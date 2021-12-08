import { Component, ComponentFactoryResolver, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Project } from 'src/app/data/projects/project';
import { ApiService } from 'src/app/services/api/api.service';
import { AddProjectComponent } from '../add-project/add-project.component';
import { DeleteProjectComponent } from '../delete-project/delete-project.component';
import { EditProjectComponent } from '../edit-project/edit-project.component';

@Component({
  selector: 'app-list-projects',
  templateUrl: './list-projects.component.html',
  styleUrls: ['./list-projects.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ListProjectsComponent implements OnInit {

  projects: Project[] = [];

  constructor(private apiService: ApiService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadProjects();
  }

  addProject() {
    this.openModal(AddProjectComponent)
  }

  editProject(project: Project) {
    this.openModal(EditProjectComponent, project);
  }

  deleteProject(project: Project) {
    this.openModal(DeleteProjectComponent, project);
  }

  openModal(component: any, project: Project | undefined = undefined){

    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(project) {
      const editProject: Project = { description: project.description, displayNumber: project.displayNumber, id: project.id, imagePath: project.imagePath, isPublished: project.isPublished, title: project.title, demoUrl: project.demoUrl, githubUrl: project.githubUrl, skills: project.skills  };
      modalRef.componentInstance.project = editProject
    }
    modalRef.componentInstance.modalRef = modalRef;
    
    modalRef.result.then(() => {
      this.loadProjects();
    });
  }

  loadProjects() {
    this.apiService.get<Project[]>('Project').subscribe((result: Project[]) => {
      this.projects = result;
    })
  }


}
