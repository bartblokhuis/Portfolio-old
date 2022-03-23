import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { Language } from 'projects/shared/src/lib/data/localization/language';
import { LanguageUpdate } from 'projects/shared/src/lib/data/localization/language-update';
import { LanguagesService } from 'projects/shared/src/lib/services/api/languages/languages.service';
import { validateLanguageUpdateForm } from '../language-helpers/language-helper';

declare var $: any;
@Component({
  selector: 'app-languages-update',
  templateUrl: './languages-update.component.html',
  styleUrls: ['./languages-update.component.scss']
})
export class LanguagesUpdateComponent implements OnInit {

  @Input() modalRef: NgbModalRef | null = null;
  @Input() language: Language | null = null;
  
  model: LanguageUpdate = { name: '', languageCulture: '', isPublished: false, displayNumber: 0, id: 0 };
  error: string | null = null;
  formData: FormData | null = null;
  imagePath: string = '';
  form: any;

  constructor(private languagesService: LanguagesService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    if(this.language) this.model = { name: this.language.name, languageCulture: this.language.languageCulture, isPublished: this.language.isPublished, displayNumber: this.language.displayNumber, id: this.language.id };
    else this.modalRef?.close();

    this.imagePath = this.language?.flagImageFilePath ?? '';

    this.form = $("#updateLanguageForm");
    if(this.form) validateLanguageUpdateForm(this.form);
  }

  submit(): void {
    this.error = null;
    if(!this.form.valid()) return;

    this.languagesService.update(this.model).subscribe((result) => {
      if(result.succeeded && this.formData && result.data && result.data.id) {
        this.languagesService.uploadLanguageIcon(result.data.id, this.formData).subscribe((iconResult) => {
          if(iconResult.succeeded) {
            this.notificationService.success("Update the language");
            this.modalRef?.close("updated");
            return;
          }

          this.error = iconResult.messages[0];
        });
      }
      else if(result.succeeded) {
        this.notificationService.success("Update the language");
        this.modalRef?.close("updated");
      }
      else if(!result.succeeded){
        this.error = result.messages[0];
      }
      else{
        this.modalRef?.close();
      }
    })
  }

  onFileChange($event: any): void {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.imagePath = file.name;
      this.formData = new FormData();
      this.formData.append('file', file);
    }
  }

}
