import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/data/messages/message';
import { MessageStatus } from "src/app/data/messages/message-status";
import { AuthenticationService } from 'src/app/services/api/authentication/authentication.service';
import { SystemService } from 'src/app/services/api/system/system.service';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { ThemingService } from 'src/app/services/theming/theming.service';

@Component({
  selector: 'app-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.scss']
})
export class MainHeaderComponent implements OnInit {

  public currentTheme = 'navbar-dark';
  amountOfnewMessages: number = 0;

  constructor(private themingService: ThemingService, private authenticationService: AuthenticationService, private messagesService: MessagesService,
    private readonly systemService: SystemService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    this.themingService.theme.subscribe((theme: string) => {

      if(theme === 'dark-mode'){
        this.currentTheme = 'navbar-dark';
      }
      else {
        this.currentTheme = 'navbar-light';
      }
    });

    this.messagesService.messages.subscribe((messages: Message[]) => {
      this.amountOfnewMessages = messages.filter(x => x.messageStatus == MessageStatus.Unread).length
    })
  }

  logout(): void {
    this.authenticationService.logout();
  }

  toggleTheme(): void {
    
    if(this.themingService.theme.value === 'dark-mode') {
      this.themingService.theme.next('light-mode');
    }
    else {
      this.themingService.theme.next('dark-mode');
    }
  }

  clearApiCache(): void {
    this.systemService.clearCache().subscribe((result) => {
      if(result.succeeded) {
        this.notificationService.success("Cleared the API's cache");
      }
    })
  }

}
