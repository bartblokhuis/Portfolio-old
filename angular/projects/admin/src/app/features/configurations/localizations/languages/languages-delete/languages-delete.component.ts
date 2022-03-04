import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { Language } from 'projects/shared/src/lib/data/localization/language';
import { LanguagesService } from 'projects/shared/src/lib/services/api/languages/languages.service';

@Component({
  selector: 'app-languages-delete',
  templateUrl: './languages-delete.component.html',
  styleUrls: ['./languages-delete.component.scss']
})
export class LanguagesDeleteComponent implements OnInit {

  @Input() modalRef: NgbModalRef | null = null;
  @Input() language: Language | null = null;


  constructor(private readonly languagesService: LanguagesService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove(): void {
    if(!this.language) return;

    this.languagesService.delete(this.language.id).subscribe((result) => {
      if(!result.succeeded) this.notificationService.error(`Failed to remove the language`)
      else this.notificationService.success(`Removed the language`)
      this.modalRef?.close("removed");
    });
  }

}
