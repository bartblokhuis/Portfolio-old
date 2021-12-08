import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, Observable } from 'rxjs';
import { AddUpdateProject } from 'src/app/data/projects/add-update-project';
import { Project } from 'src/app/data/projects/project';
import { UpdateProjectSkills } from 'src/app/data/projects/update-project-skills';
import { SkillGroup } from 'src/app/data/skill-groups/skill-group';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { formatProjectSkillsSelect, validateProjectForm } from '../helpers/project-helpers';

declare var $:any;

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {

  model: AddUpdateProject = { description: '', displayNumber: 0, imagePath: '', isPublished: false, title: '', demoUrl: '',githubUrl: '' }
  skillModel: UpdateProjectSkills = {projectId: 0, skillIds: undefined }
  currentFileName: string = ''
  skillGroups: SkillGroup[] = [];
  formData: FormData | undefined;

  addProjectForm: any;

  @Input() modalRef: NgbModalRef | undefined;

  constructor(private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    //Initialize Select2 Elements
    $('.select2').select2({closeOnSelect: false, templateResult: formatProjectSkillsSelect});
    $('.select2').on('change', (e: any) => this.skillModel.skillIds = $('.select2').val().map((x: string) => parseInt(x)));

    this.addProjectForm = $("#addProjectForm");
    validateProjectForm(this.addProjectForm);

    this.apiService.get<SkillGroup[]>('SkillGroup').subscribe((result: SkillGroup[]) => {
      this.skillGroups = result;
    })
  }

  close(){
    this.modalRef?.close();
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

    this.apiService.post<Project>("Project", this.model).subscribe((result: Project) => {

        let observables: Observable<any>[] = [];

        if(this.skillModel.skillIds && this.skillModel.skillIds.length !== 0){
          this.skillModel.projectId = result.id;
          observables.push(this.apiService.put("Project/UpdateSkills", this.skillModel));
        }

        if(this.formData) {
          observables.push(this.apiService.put(`Project/UpdateDemoImage/${result.id}`, this.formData));
        }

        if(observables.length !== 0){
          combineLatest(observables).subscribe(() => {
            this.notify();
            this.modalRef?.close();
            return;
          });
        }
        else {
          this.notify();
          this.modalRef?.close();
          return;
        }
      });
  }

  notify(): void {
    this.notificationService.success("Created the new project");
  }
}
