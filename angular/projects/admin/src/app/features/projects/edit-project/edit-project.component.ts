import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest, Observable, Subject } from 'rxjs';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { UpdateProjectSkills } from 'projects/shared/src/lib/data/projects/update-project-skills';
import { SkillGroup } from 'projects/shared/src/lib/data/skill-groups/skill-group';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { SkillGroupsService } from 'projects/shared/src/lib/services/api/skill-groups/skill-groups.service';
import { ContentTitleService } from '../../../services/content-title/content-title.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { formatProjectSkillsSelect, validateProjectForm } from '../helpers/project-helpers';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { UpdateProject } from 'projects/shared/src/lib/data/projects/update-project';

declare var $:any;

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.scss']
})
export class EditProjectComponent implements OnInit {

  @ViewChildren('skillSelect') skills: QueryList<any> | undefined;

  project: Project | null = null;
  projectSubject: Subject<Project> = new Subject();

  skillIds: number[] = [];
  model: UpdateProject = { description: '', displayNumber: 0, isPublished: false, title: '', id: 0 }
  skillModel: UpdateProjectSkills = {projectId: 0, skillIds: undefined }
  skillGroups: SkillGroup[] | undefined = undefined;
  editProjectForm: any;
  projectTitle: string = '';
  apiError: string | null = null;
  
  constructor(private readonly route: ActivatedRoute, private projectsService: ProjectsService, private skillGroupsService: SkillGroupsService, private notificationService: NotificationService, 
    private readonly router: Router, private readonly contentTitleService: ContentTitleService, private breadcrumbsService: BreadcrumbsService) { }
  

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
      this.projectSubject.next(result.data);
      this.model = result.data;

      if(this.project.skills){
        this.skillIds = this.project.skills?.map(x => x.id);
      }

      this.projectTitle = this.model.title;
      this.contentTitleService.title.next(`Edit project: ${this.project.title}`);

      this.breadcrumbsService.setBreadcrumb([
        this.breadcrumbsService.homeCrumb,
        { name: "Projects", active: false, url: null, routePath: 'projects' },
        { name: `Edit`, active: true, },
      ])
      
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

    }, () => this.router.navigate(['Projects']))
  }

  submit() {
    this.apiError = null;
    if(!this.editProjectForm.valid() || !this.project) return;

    let observables: Observable<any>[] = [];
    observables.push(this.projectsService.updateProject(this.model));

    if(this.skillModel.skillIds){
      this.skillModel.projectId = this.project.id;
      observables.push(this.projectsService.updateProjectSkills(this.skillModel));
    }

    combineLatest(observables).subscribe((result) => {
      if(!result[0].succeeded) {
        this.apiError = result[0].messages[0];
        return;
      }
      this.notify()
      return;
    })
  }

  notify(): void {
    this.notificationService.success("Updated the project");
  }

}


