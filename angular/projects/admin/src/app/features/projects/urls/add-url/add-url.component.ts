import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CreateProjectUrl } from 'projects/admin/src/app/data/projects/add-project-url';
import { ProjectsService } from 'projects/admin/src/app/services/api/projects/projects.service';
import { validateUrlForm } from '../helpers/project-url-helpers';

declare var $: any;
@Component({
  selector: 'app-add-url',
  templateUrl: './add-url.component.html',
  styleUrls: ['./add-url.component.scss']
})
export class AddUrlComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() projectId: number | null = null;

  model: CreateProjectUrl = { friendlyName: '', fullUrl: '', projectId: 0 }
  form: any;

  constructor(private readonly projectsService: ProjectsService) { }

  ngOnInit(): void {
    if(!this.projectId) this.modal?.close();
    else this.model.projectId = this.projectId;

    this.form = $("#addUrlForm");
    validateUrlForm(this.form);
  }

  add() {
    if(!this.form.valid()) return;

    this.projectsService.createProjectUrl(this.model).subscribe((result) => {
      if(result.succeeded) this.modal?.close();
    })
  }

}
