import { Component } from '@angular/core';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { MessageStatus } from 'projects/shared/src/lib/data/messages/message-status';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { MessagesService } from '../../../services/messages/messages.service';


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  hasNewMessages: boolean = false;

  menuItems : MenuItem[] = [ 
    { name: "Dashboard", path: "/dashboard", icon: "nav-icon fas fa-home" }, 
    { name: "About me", path: "/about-me", icon: "nav-icon fas fa-male" }, 
    { name: "Skills", path: "/skills", icon: "nav-icon fas fa-trophy" }, 
    { name: "Projects", path: "/projects", icon: "nav-icon fas fa-tasks" },
    { name: "Blog", path: "", icon: "nav-icon fas fa-book", menuItems: [
      { name: "Blog posts", path: "/blog/posts", icon: "far fa-circle nav-icon" },
      { name: "Blog subscribers", path: "/blog/subscribers", icon: "far fa-circle nav-icon" },
    ] },
    { name: "Messages", path: "/messages", icon: "nav-icon fas fa-inbox" },
    { name: "Pictures", path: "/pictures", icon: "nav-icon fas fa-image" },
    { name: 'Settings', path: '', icon: 'nav-icon fas fa-cog', menuItems: [
      { name: 'General settings', icon: "far fa-circle nav-icon", path: "/settings/general-settings" },
      { name: 'Seo settings', icon: "far fa-circle nav-icon", path: "/settings/seo-settings" },
      { name: 'Email settings', icon: "far fa-circle nav-icon", path: "/settings/email-settings" },
      { name: 'Blog settings', icon: "far fa-circle nav-icon", path: "/settings/blog-settings" },
      { name: 'Public site settings', icon: "far fa-circle nav-icon", path: "/settings/public-site-settings" },
      { name: 'API settings', icon: "far fa-circle nav-icon", path: "/settings/api-settings" },
    ]},
    { name: 'System', icon: 'fas fa-server nav-icon', menuItems: [
      { name: 'Email Queue', icon: "far fa-circle nav-icon", path: "/system/email-queue" },
      { name: 'Schedule tasks', icon: "far fa-circle nav-icon", path: "/system/schedule-tasks" },
    ]}
  ];

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

