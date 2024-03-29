import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { combineLatest, Observable } from 'rxjs';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { AddUpdateProject } from 'projects/shared/src/lib/data/projects/add-update-project';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { UpdateProjectSkills } from 'projects/shared/src/lib/data/projects/update-project-skills';
import { SkillGroup } from 'projects/shared/src/lib/data/skill-groups/skill-group';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { SkillGroupsService } from 'projects/shared/src/lib/services/api/skill-groups/skill-groups.service';
import { ContentTitleService } from '../../../services/content-title/content-title.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { formatProjectSkillsSelect, validateProjectForm } from '../helpers/project-helpers';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { AddProject } from 'projects/shared/src/lib/data/projects/add-project';

declare var $:any;

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {

  model: AddProject = { description: '', displayNumber: 0, isPublished: false, title: '' }
  skillModel: UpdateProjectSkills = {projectId: 0, skillIds: undefined }
  skillGroups: SkillGroup[] = [];
  apiError: string | null = null;

  addProjectForm: any;

  constructor(private projectsService: ProjectsService, private skillGroupsService: SkillGroupsService, private notificationService: NotificationService, 
    private readonly router: Router, private readonly contentTitleService: ContentTitleService, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {
    //Initialize Select2 Elements
    $('.select2').select2({closeOnSelect: false, templateResult: formatProjectSkillsSelect});
    $('.select2').on('change', (e: any) => this.skillModel.skillIds = $('.select2').val().map((x: string) => parseInt(x)));

    this.addProjectForm = $("#addProjectForm");
    validateProjectForm(this.addProjectForm);

    this.skillGroupsService.getAll().subscribe((result: Result<SkillGroup[]>) => {
      if(result.succeeded) this.skillGroups = result.data;
    })

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      { name: "Projects", active: false, url: null, routePath: 'projects' },
      { name: `New`, active: true, },
    ])

    this.contentTitleService.title.next('Add new project')
  }
  
  submit(): void {
    this.apiError = null;
    if(!this.addProjectForm.valid()) return;

    this.projectsService.createProject(this.model).subscribe((result: Result<Project>) => {

      if(!result.succeeded) {
        this.apiError = result.messages[0];
        return;
      }

      const project = result.data;
      let observables: Observable<any>[] = [];

      if(this.skillModel.skillIds && this.skillModel.skillIds.length !== 0){
        this.skillModel.projectId = project.id;
        observables.push(this.projectsService.updateProjectSkills(this.skillModel));
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
