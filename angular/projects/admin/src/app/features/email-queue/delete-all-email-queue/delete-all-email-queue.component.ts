import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { QueuedEmailsService } from 'projects/shared/src/lib/services/api/queued-emails/queued-emails.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-all-email-queue',
  templateUrl: './delete-all-email-queue.component.html',
  styleUrls: ['./delete-all-email-queue.component.scss']
})
export class DeleteAllEmailQueueComponent implements OnInit {

  @Input() modal!: NgbModalRef;

  constructor(private readonly queuedEmailsService: QueuedEmailsService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove() {
    this.queuedEmailsService.deleteAll().subscribe((result) => {
      if(result.succeeded){
        this.notificationService.success("Removed all the queued emails");
        this.modal?.close();
      }
    })
  }

}
