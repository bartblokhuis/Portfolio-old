import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { LanguageCreate } from 'projects/shared/src/lib/data/localization/language-create';
import { LanguagesService } from 'projects/shared/src/lib/services/api/languages/languages.service';
import { validateLanguageForm } from '../language-helpers/language-helper';

declare var $: any;

@Component({
  selector: 'app-languages-add',
  templateUrl: './languages-add.component.html',
  styleUrls: ['./languages-add.component.scss']
})
export class LanguagesAddComponent implements OnInit {

  @Input() modalRef: NgbModalRef | undefined;
  
  model: LanguageCreate = { name: '', languageCulture: '', isPublished: false, displayNumber: 0};
  error: string | null = null;
  formData: FormData | null = null;
  imagePath: string = '';
  form: any;

  constructor(private languagesService: LanguagesService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    this.form = $("#addLanguageForm");
    if(this.form) validateLanguageForm(this.form);
  }

  submit() {
    this.error = null;
    if(!this.form.valid()) return;

    this.languagesService.create(this.model).subscribe((result) => {
      if(result.succeeded && this.formData && result.data && result.data.id) {
        this.languagesService.uploadLanguageIcon(result.data.id, this.formData).subscribe((iconResult) => {
          if(iconResult.succeeded) {
            this.notificationService.success("Created the new language");
            this.modalRef?.close("added");
            return;
          }

          this.error = iconResult.messages[0];
        });
      }
      else if(!result.succeeded){
        this.error = result.messages[0];
      }
      else{
        this.modalRef?.close();
      }
    })
  }


  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.imagePath = file.name;
      this.formData = new FormData();
      this.formData.append('file', file);
    }
  }


}
