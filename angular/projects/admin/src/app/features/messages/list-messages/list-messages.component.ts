import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { MessagesService } from '../../../services/messages/messages.service';
import { DeleteMessageComponent } from '../delete-message/delete-message.component';
import { EditMessageComponent } from '../edit-message/edit-message.component';

@Component({
  selector: 'app-list-messages',
  templateUrl: './list-messages.component.html',
  styleUrls: ['./list-messages.component.scss']
})
export class ListMessagesComponent implements OnInit {

  constructor(private messagesService: MessagesService, private modalService: NgbModal) { }

  messages: Message[] = [];

  ngOnInit(): void {
    this.messagesService.getMessages().subscribe(result => {
      if(result.succeeded) this.messages = result.data;
    });
  }

  edit(message: Message): void {
    const modal = this.openModal(EditMessageComponent);
    modal.componentInstance.message = message;

    modal.result.then(() => {

    });
  }

  delete(message: Message): void {
    const modal = this.openModal(DeleteMessageComponent);
    modal.componentInstance.message = message;

    modal.result.then((result) => {

      if(result === 'canceled') return;

      //Remove the message from the list without refreshing the entire list.
      this.messages = this.messages.filter(x => x.id !== message.id);
    });

  }

  openModal(component: any) : NgbModalRef {

    const modal = this.modalService.open(component, { size: 'lg' });
    modal.componentInstance.modal = modal;

    return modal;
  }

}
