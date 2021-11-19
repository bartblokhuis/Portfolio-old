import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Project, UpdateProjectSkills } from 'src/app/data/project';
import { ProjectService } from 'src/app/services/projects/project.service';
import { SkillService } from 'src/app/services/skills/skill.service';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.scss']
})
export class CreateProjectComponent implements OnInit {

  @Input() modalRef: NgbModalRef;

  selectedItems = [];
  dropdownList = null;
  dropDown = new FormControl('');
  dropdownSettings: IDropdownSettings = {};

  createForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    demoUrl: new FormControl(''),
    githubUrl: new FormControl(''),
    isPublished: new FormControl(),
    file: new FormControl(''),
    fileSource: new FormControl('')
  });

  constructor(private skillService: SkillService, private projectService: ProjectService, private toastr: ToastrService) { }

  currentFileName: string = 'File';

  ngOnInit(): void {
    this.dropdownSettings.enableCheckAll = false;
    this.skillService.getSkills().subscribe((skills) => {
      this.dropdownList = skills.map((skill) => ({id: skill.id, text: skill.name}));
    });
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.currentFileName = file.name;
      this.createForm.patchValue({
        fileSource: file
      });
    }
  }

  close(){
    this.modalRef.close();
  }

  submit(): void {
    let project: Project = {
      title: this.createForm.controls.title.value,
      description: this.createForm.controls.description.value,
      isPublished: this.createForm.controls.isPublished.value ?? false,
      demoUrl: this.createForm.controls.demoUrl.value,
      githubUrl: this.createForm.controls.githubUrl.value,
      imagePath: '',
      displayNumber: 0,
      id: 0
    };

    this.projectService.createProject(project).subscribe((newProject: Project) => {
      project = newProject;

      var uploadSkillsRequest = this.uploadProjectSkills(project.id);
      if(uploadSkillsRequest){
        uploadSkillsRequest.subscribe(() => {
          var uploadImageRequest = this.uploadProjectImage(project.id);
          if(!uploadImageRequest){
            this.modalRef.close();
            this.showCreatedNotification();
            return;
          }

          uploadImageRequest.subscribe(() => {
            this.modalRef.close();
            this.showCreatedNotification();
            return;
          })
        });
      }
      else{
        var uploadImageRequest = this.uploadProjectImage(project.id);
        if(!uploadImageRequest){
          this.modalRef.close();
          this.showCreatedNotification();
          return;
        }

        uploadImageRequest.subscribe(() => {
          this.modalRef.close();
          this.showCreatedNotification();
          return;
        })
      }
    })
  };

  uploadProjectSkills(projectId: number): Observable<object> {
    
    if(!this.dropDown || this.dropDown.value.length == 0){
      return;
    }

    const updateModel: UpdateProjectSkills = {
      projectId: projectId,
      skillIds: this.dropDown.value.map(x => x.id)
    };
    
    return this.projectService.updateProjectSkills(updateModel);
  }

  uploadProjectImage(projectId: number): Observable<object> {
      const fileSource = this.createForm.get('fileSource').value;
      
      if(!fileSource) {
        return;
      }

      const formData = new FormData();
      formData.append('icon', fileSource);

      return this.projectService.updateProjectImage(projectId, formData);
  }

  showCreatedNotification(){
    this.toastr.success("Saved the new project");
  }

}
