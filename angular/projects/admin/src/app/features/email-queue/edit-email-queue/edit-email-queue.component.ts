import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { UpdateQueuedEmail } from 'projects/shared/src/lib/data/queued-email/update-queued-email';
import { QueuedEmailsService } from 'projects/shared/src/lib/services/api/queued-emails/queued-emails.service';
import { NotificationService } from '../../../services/notification/notification.service';

declare var $: any;

@Component({
  selector: 'app-edit-email-queue',
  templateUrl: './edit-email-queue.component.html',
  styleUrls: ['./edit-email-queue.component.scss']
})
export class EditEmailQueueComponent implements OnInit {

  @Input() modal!: NgbModalRef;
  @Input() queuedEmail!: UpdateQueuedEmail;

  form: any;
  error!: string;

  constructor(private readonly queuedEmailsService: QueuedEmailsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    if(!this.queuedEmail){
      this.modal?.close();
      return;
    }
    this.queuedEmailsService.getById(this.queuedEmail.id).subscribe((result) => {
      if(!result.succeeded) this.modal?.close();

      this.queuedEmail = result.data;
    });

    this.form = $("#editQueuedEmailForm");
    this.validateForm();
  }

  update() {

    if(!this.form.valid()) return;

    this.queuedEmailsService.edit(this.queuedEmail).subscribe((result) => {
      if(!result.succeeded){
        this.error = result.messages[0];
        return;
      }

      this.notificationService.success("Updated the queued email")
      this.modal?.close();
    })

  }

  validateForm() {
    this.form.validate({
      rules: {
        from: {
          required: true,
          email: true,
        },
        to: {
          required: true,
          email: true,
        },
        subject: {
          required: true
        },
        body: {
          required: true
        },
        sentTries: {
          required: true,
          number: true,
          min: 0,
          max: 3,
        }
      },
      messages: {
        from: {
          required: "Please enter a from email address",
          email:  "Please enter a valid email address",
        },
        to: {
          required: "Please enter a to email address",
          email: "Please enter a valid email address",
        },
        subject: {
          required: "Please enter the email's subject"
        },
        body: {
          required: "Please enter the email's content"
        },
        sentTries: {
          required: "Please enter the amount of attempts",
          number: "Please enter a number",
          min: "Please don't enter sub zero numbers",
          max: "Please don't enter a number higher than 3"
        }
      },
      errorElement: 'span',
      errorPlacement: function (error: any, element: any) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
      },
      highlight: function (element: any, errorClass: any, validClass: any) {
        $(element).addClass('is-invalid');
      },
      unhighlight: function (element: any, errorClass: any, validClass: any) {
        $(element).removeClass('is-invalid');
      }
  });
  }
}
