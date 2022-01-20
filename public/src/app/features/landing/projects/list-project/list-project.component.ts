import { DOCUMENT } from '@angular/common';
import { Component, ComponentFactoryResolver, Inject, OnInit } from '@angular/core';
import { Project } from 'src/app/data/project/Project';
import { Skill } from 'src/app/data/Skill';
import { ProjectsService } from 'src/app/services/projects/projects.service';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-list-project',
  templateUrl: './list-project.component.html',
  styleUrls: ['./list-project.component.scss']
})
export class ListProjectComponent implements OnInit {

  projects: Project[] | null = null;
  baseUrl: string = environment.baseApiUrl;

  constructor(private projectsService: ProjectsService, @Inject(DOCUMENT) private document: Document, private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit(): void {
    this.projectsService.get().subscribe((result) => {
      if(result.succeeded) this.projects = result.data;
    })

    //let childComponent = this.componentFactoryResolver.resolveComponentFactory(ChildComponent);
    //this.componentRef = this.target.createComponent(childComponent);
  }

  readMore(project: Project) {

    var buttons = "";
    if(project.urls) {
      project.urls.forEach(url => {
        buttons += `<a class="btn btn-secondary" target="_blank" href="${url.fullUrl}">${url.friendlyName}</a>`
      })
    }

    const modalContent = `<div class="modal-image"></div>
    <div class="modal-title">${project.title}</div> <div class="project-skills">${this.printSkill(project.skills)}</div> <div class="modal-content">${project.description}</div><div class="modal-footer">${buttons}</div>`;

    Swal.fire({
      title: "",
      showCancelButton: false,
      showConfirmButton: false,
      html: modalContent,
      heightAuto: false,
    });
  }

  printSkill(skills: Skill[] | undefined): string {
    return skills?.map(x => x.name).join(" / ") ?? ""
  }

}
