import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { MessageStatus } from 'src/app/data/Messages/Message';
import { User } from 'src/app/data/User';
import { AuthenticationService } from 'src/app/services/Authentication/authentication.service';
import { MessageService } from 'src/app/services/messages/message.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  public amountOfMessages: number = 0;
  user: User;

  constructor(private authenticationService: AuthenticationService, private messagesService: MessageService) { 
    this.messagesService.getMessages().then((res) => {
      if(!res || res.length === 0) return;

      this.amountOfMessages =  res.filter(x => x.messageStatus === MessageStatus.Unread).length;
    });

    this.messagesService.updatedMessagesEventListener((messages) => {
      this.amountOfMessages =  messages.filter(x => x.messageStatus === MessageStatus.Unread).length;
    });
  }
  
  ngOnInit(): void {
  }

  logout(): void {
    this.authenticationService.logout();
  }
}
