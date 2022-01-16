import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Project } from 'src/app/data/Project';
import { Skill } from 'src/app/data/Skill';
import { ProjectsService } from 'src/app/services/projects/projects.service';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

  projects: Project[] | null = null;
  baseUrl: string = environment.baseApiUrl;

  constructor(private projectsService: ProjectsService, @Inject(DOCUMENT) private document: Document) { }

  ngOnInit(): void {
    this.projectsService.get().subscribe((result) => {
      if(result.succeeded) this.projects = result.data;
    })
  }

  readMore(project: Project) {
    let demoButton = "", githubButton = ""

    if(project.githubUrl){
      githubButton = `<a class="btn btn-secondary" target="_blank" href="${project.githubUrl}">VIEW ON GITHUB</a>`
    }

    if(project.demoUrl){
      demoButton = `<a class="btn btn-secondary" target="_blank" href="${project.demoUrl}">VIEW SITE</a>`
    }

    const modalContent = `<div class="modal-image"><img src="${this.baseUrl}${project.imagePath}"/></div>
    <div class="modal-title">${project.title}</div> <div class="project-skills">${this.printSkill(project.skills)}</div> <div class="modal-content">${project.description}</div><div class="modal-footer">${demoButton}${githubButton}</div>`;

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
