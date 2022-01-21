import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Result } from '../../../data/common/Result';
import { Project } from '../../../data/projects/project';
import { ProjectsService } from '../../../services/api/projects/projects.service';
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

  constructor(private projectsService: ProjectsService, private modalService: NgbModal) { }

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
      const editProject: Project = { description: project.description, displayNumber: project.displayNumber, id: project.id, isPublished: project.isPublished, title: project.title, urls: [], skills: project.skills, pictures: project.pictures  };
      modalRef.componentInstance.project = editProject
    }
    modalRef.componentInstance.modalRef = modalRef;
    
    modalRef.result.then(() => {
      this.loadProjects();
    });
  }

  loadProjects() {
    this.projectsService.getAll().subscribe((result: Result<Project[]>) => {
      if(result.succeeded) this.projects = result.data;
      console.log(result)
    })
  }
}
