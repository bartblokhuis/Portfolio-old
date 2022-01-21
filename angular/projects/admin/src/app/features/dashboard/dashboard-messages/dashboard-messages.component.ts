import { Component, OnInit } from '@angular/core';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { MessagesService } from '../../../services/messages/messages.service';

@Component({
  selector: 'app-dashboard-messages',
  templateUrl: './dashboard-messages.component.html',
  styleUrls: ['./dashboard-messages.component.scss']
})
export class DashboardMessagesComponent implements OnInit {

  messages: Message[] = [];
  constructor(private messagesService: MessagesService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages(){
    this.messagesService.getMessages().subscribe((result) => {
      if(result.data.length > 5){
        this.messages = result.data.slice(0, 5)
      }
      else {
        this.messages = result.data;
      }
    })
  }

}
