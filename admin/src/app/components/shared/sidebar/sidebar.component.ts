import { Component } from '@angular/core';
import { Message } from 'src/app/data/messages/message';
import { MessageStatus } from "src/app/data/messages/message-status";
import { MessagesService } from 'src/app/services/messages/messages.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  hasNewMessages: boolean = false;

  menuItems : MenuItem[] = [ 
    { name: "About me", path: "/about-me", icon: "nav-icon fas fa-male" }, 
    { name: "Skills", path: "/skills", icon: "nav-icon fas fa-trophy" }, 
    { name: "Projects", path: "/projects", icon: "nav-icon fas fa-tasks" },
    { name: "Blog", path: "/blog", icon: "nav-icon fas fa-tasks" },
    { name: "Messages", path: "/messages", icon: "nav-icon fas fa-inbox" },
    { name: 'Settings', path: '', icon: 'nav-icon fas fa-cog', menuItems: [
      { name: 'General settings', icon: "far fa-circle nav-icon", path: "/settings/general-settings" },
      { name: 'Seo settings', icon: "far fa-circle nav-icon", path: "/settings/seo-settings" },
      { name: 'Email settings', icon: "far fa-circle nav-icon", path: "/settings/email-settings" },
    ]}];

  constructor(private messagesService: MessagesService) { 

    const indexOfMessages = this.menuItems.findIndex(x => x.name === 'Messages');

    //Applies a 'new' badge to the messages menu item.
    this.messagesService.messages.subscribe((messages: Message[]) => {
      
      if(messages.filter(x => x.messageStatus == MessageStatus.Unread).length > 0){
        if(!this.menuItems) return;
        this.menuItems[indexOfMessages].badge = { value: "New", style: "right badge badge-danger" };
      }
      else {
        if(!this.menuItems) return;
        this.menuItems[indexOfMessages].badge = undefined;
      }
    });
  }
}

interface MenuItem {
  name: string
  path?: string
  icon: string
  menuItems?: MenuItem[]
  badge?: Badge
}

interface Badge {
  value: any
  style: string
}

