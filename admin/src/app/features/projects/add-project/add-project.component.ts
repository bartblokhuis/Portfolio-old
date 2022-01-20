import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {

  model: AddUpdateProject = { description: '', displayNumber: 0, isPublished: false, title: '', demoUrl: '',githubUrl: '' }
  skillModel: UpdateProjectSkills = {projectId: 0, skillIds: undefined }
  currentFileName: string = ''
  skillGroups: SkillGroup[] = [];
  formData: FormData | undefined;

  addProjectForm: any;

  constructor(private projectsService: ProjectsService, private skillGroupsService: SkillGroupsService, private notificationService: NotificationService, 
    private readonly router: Router, private readonly contentTitleService: ContentTitleService) { }

  ngOnInit(): void {
    //Initialize Select2 Elements
    $('.select2').select2({closeOnSelect: false, templateResult: formatProjectSkillsSelect});
    $('.select2').on('change', (e: any) => this.skillModel.skillIds = $('.select2').val().map((x: string) => parseInt(x)));

    this.addProjectForm = $("#addProjectForm");
    validateProjectForm(this.addProjectForm);

    this.skillGroupsService.getAll().subscribe((result: Result<SkillGroup[]>) => {
      if(result.succeeded) this.skillGroups = result.data;
    })

    this.contentTitleService.title.next('Add new project')
  }

  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.currentFileName = file.name;
      this.formData = new FormData();
      this.formData.append('icon', file);
    }
  }

  submit(): void {
    if(!this.addProjectForm.valid()) return;

    this.projectsService.createProject(this.model).subscribe((result: Result<Project>) => {

      if(!result.succeeded) {
        return;
      }

      const project = result.data;
      let observables: Observable<any>[] = [];

      if(this.skillModel.skillIds && this.skillModel.skillIds.length !== 0){
        this.skillModel.projectId = project.id;
        observables.push(this.projectsService.updateProjectSkills(this.skillModel));
      }

      if(this.formData) {
        observables.push(this.projectsService.updateDemoImage(project.id, this.formData));
      }

      if(observables.length !== 0){
        combineLatest(observables).subscribe(() => {
          this.notify();
        });
      }
      else {
        this.notify();
      }

      this.router.navigate([`projects/edit/${result.data.id}`]);
    });
  }

  notify(): void {
    this.notificationService.success("Created the new project");
  }
}
