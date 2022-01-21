import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { UrlsService } from 'projects/admin/src/app/services/api/urls/urls.service';
import { Url } from 'projects/shared/src/lib/data/url';
import { validateUrlForm } from '../helpers/project-url-helpers';

declare var $: any;
@Component({
  selector: 'app-edit-url',
  templateUrl: './edit-url.component.html',
  styleUrls: ['./edit-url.component.scss']
})
export class EditUrlComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() url: Url = { friendlyName: '', fullUrl: '', id: 0}

  form: any;

  constructor(private readonly urlsService: UrlsService) { }

  ngOnInit(): void {
    if(this.url.id == 0) this.modal?.close();

    this.form = $("#editUrlForm");
    validateUrlForm(this.form);
  }

  update(): void {
    if(!this.form.valid()) return;

    this.urlsService.update(this.url).subscribe((result) => {
      if(result.succeeded) this.modal?.close();
    })
  }

}
