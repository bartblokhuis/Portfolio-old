import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Message } from 'src/app/data/Messages/Message';
import { DeleteMessageComponent } from '.././delete-message/delete-message.component';
import { EditMessageComponent } from '.././edit-message/edit-message.component';
import { MessageService } from '../../../services/messages/message.service';

@Component({
  selector: 'app-list-messages',
  templateUrl: './list-messages.component.html',
  styleUrls: ['./list-messages.component.scss']
})
export class ListMessagesComponent implements OnInit {

  messages: Message[] = [];
  currentDate: Date = new Date();

  constructor(private messageService: MessageService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.LoadMessages();
    this.messageService.updatedMessagesEventListener((messages) => {
      this.messages = messages;
    });
  }

  LoadMessages(){
    this.messageService.getMessages(true).then((messages) => {
      this.messages = messages;
    });
  }

  editMessage(message: Message) {
    const modalRef = this.modalService.open(EditMessageComponent, { size: 'lg' })
    modalRef.componentInstance.message = message;
    modalRef.componentInstance.modalRef = modalRef;
    modalRef.result.then((result => {
      //this.LoadMessages();
    }))
  }

  deleteMessage(message: Message){
    const modalRef = this.modalService.open(DeleteMessageComponent, { size: 'lg' });
    modalRef.componentInstance.message = message;
    modalRef.componentInstance.modalRef = modalRef;

    modalRef.result.then((result => {
      //this.LoadMessages();
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

}
