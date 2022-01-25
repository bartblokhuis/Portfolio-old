import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { QueuedEmail } from 'projects/shared/src/lib/data/queued-email/queued-email';
import { UpdateQueuedEmail } from 'projects/shared/src/lib/data/queued-email/update-queued-email';
import { QueuedEmailsService } from 'projects/shared/src/lib/services/api/queued-emails/queued-emails.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { DeleteAllEmailQueueComponent } from '../delete-all-email-queue/delete-all-email-queue.component';
import { DeleteEmailQueueComponent } from '../delete-email-queue/delete-email-queue.component';
import { EditEmailQueueComponent } from '../edit-email-queue/edit-email-queue.component';

@Component({
  selector: 'app-list-email-queue',
  templateUrl: './list-email-queue.component.html',
  styleUrls: ['./list-email-queue.component.scss']
})
export class ListEmailQueueComponent implements OnInit {

  queuedEmails: QueuedEmail[] = [];

  constructor(private readonly queuedEmailsService: QueuedEmailsService, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {
    this.loadQueuedEmail();
  }

  loadQueuedEmail() {
    this.queuedEmailsService.getAll().subscribe((result) => {
      if(result.succeeded) this.queuedEmails = result.data;
    })

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'System',
        url: undefined,
        active: true
      },
      {
        name: 'Email queue',
        url: undefined,
        active: true
      }
    ]);
  }

  edit(queuedEmail: QueuedEmail) {
    this.openModal(EditEmailQueueComponent, queuedEmail);
  }

  delete(queuedEmail: QueuedEmail) {
    this.openModal(DeleteEmailQueueComponent, queuedEmail);
  }

  deleteAll() {
    this.openModal(DeleteAllEmailQueueComponent)
  }

  openModal(component: any, queuedEmail?: QueuedEmail) {
    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(queuedEmail){
      var email: UpdateQueuedEmail = { from: queuedEmail.from, fromName: queuedEmail.fromName, id: queuedEmail.id, sentTries: queuedEmail.sentTries, subject: queuedEmail.subject, to: queuedEmail.to, toName: queuedEmail.toName, body: '' };
      modalRef.componentInstance.queuedEmail = email;
    }
    
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.loadQueuedEmail();
    });
  }

}
