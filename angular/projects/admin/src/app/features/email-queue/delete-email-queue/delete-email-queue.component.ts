import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { UpdateQueuedEmail } from 'projects/shared/src/lib/data/queued-email/update-queued-email';
import { QueuedEmailsService } from 'projects/shared/src/lib/services/api/queued-emails/queued-emails.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-email-queue',
  templateUrl: './delete-email-queue.component.html',
  styleUrls: ['./delete-email-queue.component.scss']
})
export class DeleteEmailQueueComponent implements OnInit {

  @Input() modal!: NgbModalRef;
  @Input() queuedEmail!: UpdateQueuedEmail;

  constructor(private readonly queuedEmailsService: QueuedEmailsService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove() {
    this.queuedEmailsService.delete(this.queuedEmail.id).subscribe((result) => {
      if(result.succeeded) {
        this.notificationService.success("Removed the queued email");
        this.modal?.close();
      }
    })
  }
}
