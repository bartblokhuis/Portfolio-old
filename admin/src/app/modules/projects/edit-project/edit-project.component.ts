import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Project, UpdateProjectSkills } from 'src/app/data/project';
import { ProjectService } from 'src/app/services/projects/project.service';
import { SkillService } from 'src/app/services/skills/skill.service';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.scss']
})
export class EditProjectComponent implements OnInit {

  @Input() project: Project;
  @Input() modalRef: NgbModalRef;

  editForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    demoUrl: new FormControl(''),
    githubUrl: new FormControl(''),
    isPublished: new FormControl(),
    file: new FormControl(''),
    fileSource: new FormControl(''),
  });

  cities: any = [];
  selectedItems: any = [];
  dropdownSettings: any = {};
  currentFileName = "";

  dropdownList = null;
  skillsDropDown = new FormControl('');
  skills: any[];

  constructor(private skillService: SkillService, private projectService: ProjectService, private toastr: ToastrService) { }

  ngOnInit(): void {

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      enableCheckAll: false,
      itemsShowLimit: 3,
      allowSearchFilter: false
  };


    this.skillService.getSkills().subscribe((skills) => {

      if(this.project.skills){
        var projectSkillIds: number[] = this.project.skills.map((skill) => skill.id);
        this.selectedItems = skills.filter((skill) => projectSkillIds.indexOf(skill.id) !== -1).map((skill) => ({item_id: skill.id, item_text: skill.name}));
        this.skillsDropDown.setValue(this.selectedItems);
      }
      
      this.skills = skills.map((skill) => ({item_id: skill.id, item_text: skill.name}));
    });

    this.editForm.controls.title.setValue(this.project.title);
    this.editForm.controls.description.setValue(this.project.description);
    this.editForm.controls.demoUrl.setValue(this.project.demoUrl);
    this.editForm.controls.githubUrl.setValue(this.project.githubUrl);
    this.editForm.controls.isPublished.setValue(this.project.isPublished);
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.currentFileName = file.name;
      this.editForm.patchValue({
        fileSource: file
      });
    }
  }

  close(): void {
    this.modalRef.close();
  }

  submit():void {
    let project: Project = {
      title: this.editForm.controls.title.value,
      description: this.editForm.controls.description.value,
      isPublished: this.editForm.controls.isPublished.value ?? false,
      demoUrl: this.editForm.controls.demoUrl.value,
      githubUrl: this.editForm.controls.githubUrl.value,
      imagePath: this.project.imagePath,
      displayNumber: this.project.displayNumber,
      id: this.project.id
    };

    this.projectService.updateProject(project).subscribe(() => {
      this.uploadProjectSkills(project.id).subscribe(() => {

        var uploadImageRequest = this.uploadProjectImage(project.id)

        if(uploadImageRequest){
          uploadImageRequest.subscribe(() => {
            this.modalRef.close();
            this.savedProjectNotification();
            return;
          });
        }else{
          this.modalRef.close();
          this.savedProjectNotification();
            return;
        }
      })
    })
  }

  uploadProjectSkills(projectId: number): Observable<object> {
    const updateModel: UpdateProjectSkills = {
      projectId: projectId,
      skillIds: this.skillsDropDown.value.map(x => x.item_id)
    };

    return this.projectService.updateProjectSkills(updateModel);
  }

  uploadProjectImage(projectId: number): Observable<object> {

      const fileSource = this.editForm.get('fileSource').value;
      if(!fileSource) {
        return;
      }

      const formData = new FormData();
      formData.append('icon', fileSource);

      return this.projectService.updateProjectImage(projectId, formData);
  }

  savedProjectNotification(){
    this.toastr.success("Saved " + this.project.title);
  }

}
