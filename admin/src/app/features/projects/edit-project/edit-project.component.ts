import { AfterViewInit, Component, Input, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { AddUpdateProject } from 'src/app/data/projects/add-update-project';
import { Project } from 'src/app/data/projects/project';
import { UpdateProjectSkills } from 'src/app/data/projects/update-project-skills';
import { SkillGroup } from 'src/app/data/skill-groups/skill-group';
import { ProjectsService } from 'src/app/services/api/projects/projects.service';
import { SkillGroupsService } from 'src/app/services/api/skill-groups/skill-groups.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { formatProjectSkillsSelect, validateProjectForm } from '../helpers/project-helpers';

declare var $:any;

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.scss']
})
export class EditProjectComponent implements OnInit, AfterViewInit {

  @ViewChildren('skillSelect') skills: QueryList<any> | undefined;

  project: Project | null = null;
  skillIds: number[] = [];
  model: AddUpdateProject = { description: '', displayNumber: 0, imagePath: '', isPublished: false, title: '', demoUrl: '',githubUrl: '' }
  skillModel: UpdateProjectSkills = {projectId: 0, skillIds: undefined }
  currentFileName: string = '';
  skillGroups: SkillGroup[] | undefined = undefined;
  formData: FormData | undefined;
  editProjectForm: any;
  projectTitle: string = '';
  
  constructor(private readonly route: ActivatedRoute, private projectsService: ProjectsService, private skillGroupsService: SkillGroupsService, private notificationService: NotificationService, 
    private readonly router: Router, private readonly contentTitleService: ContentTitleService) { }
  

  ngOnInit(): void {

    const idParam = this.route.snapshot.paramMap.get('id');
    if(!idParam) return;

    const id = parseInt(idParam);

    this.projectsService.getById(id).subscribe((result) => {
      if(!result.succeeded){
        this.router.navigate(['Projects']);
        return;
      }

      this.project = result.data;
      this.model = result.data;

      if(this.project.skills){
        this.skillIds = this.project.skills?.map(x => x.id);
      }

      this.projectTitle = this.model.title;
      this.contentTitleService.title.next(`Edit project: ${this.project.title}`)
      
      this.currentFileName = this.project.imagePath;
      this.model.id = this.project.id;
  
      $('.select2').select2({closeOnSelect: false, templateResult: formatProjectSkillsSelect, tags: true});
      $('.select2').on('change', (e: any) => this.skillModel.skillIds = $('.select2').val().map((x: string) => parseInt(x)));
  
      this.model = this.project;
      this.editProjectForm = $("#editProjectForm");
      validateProjectForm(this.editProjectForm);
  
      this.skillGroupsService.getAll().subscribe((result: Result<SkillGroup[]>) => {
        if(result.succeeded) this.skillGroups = result.data;

        this.skills?.changes.subscribe(t => {
          $('.select2').select2({closeOnSelect: false, templateResult: formatProjectSkillsSelect, tags: true});
        });
      });

      console.log('done 1')

      

    }, () => this.router.navigate(['Projects']))
  }

  ngAfterViewInit(): void {
    console.log('done 2')
    
  }

  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.currentFileName = file.name;
      this.formData = new FormData();
      this.formData.append('icon', file);
    }
  }

  submit() {
    if(!this.editProjectForm.valid() || !this.project) return;

    let observables: Observable<any>[] = [];
    observables.push(this.projectsService.updateProject(this.model));

    if(this.skillModel.skillIds){
      this.skillModel.projectId = this.project.id;
      observables.push(this.projectsService.updateProjectSkills(this.skillModel));
    }

    if(this.formData) {
      observables.push(this.projectsService.updateDemoImage(this.project.id, this.formData));
    }

    combineLatest(observables).subscribe(() => {
      this.notify()
      return;
    })
  }

  notify(): void {
    this.notificationService.success("Updated the project");
  }

}


