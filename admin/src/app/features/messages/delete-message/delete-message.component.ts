import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Message } from 'src/app/data/messages/message';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-delete-message',
  templateUrl: './delete-message.component.html',
  styleUrls: ['./delete-message.component.scss']
})
export class DeleteMessageComponent implements OnInit {

  @Input() modal: NgbModalRef | undefined;
  @Input() message: Message | undefined;
  
  constructor(private messagesService: MessagesService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove(){

    if(!this.message) return;

    this.messagesService.deleteMessage(this.message).subscribe(() => {
      this.notificationService.success(`rRemoved ${this.message?.firstName} ${this.message?.lastName}'s message.'`);
      this.modal?.close();
    })
  }

}
