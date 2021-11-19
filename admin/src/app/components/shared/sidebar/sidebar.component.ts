import { Component, OnInit } from '@angular/core';
import { MessageStatus } from 'src/app/data/Messages/Message';
import { MessageService } from 'src/app/services/messages/message.service';

interface MenuItems {
  name: string
  path?: string
  icon: string
  menuItems?: MenuItems[]
  badge?: Badge
}

interface Badge {
  value: any
  style: string
}


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  menuItems : MenuItems[] = [ 
    { name: "Dashboard", path: "/dashboard", icon: "nav-icon fas fa-home" }, 
    { name: "About me", path: "/about-me", icon: "nav-icon fas fa-male" }, 
    { name: "Skills", path: "/skills", icon: "nav-icon fas fa-trophy" }, 
    { name: "Projects", path: "/projects", icon: "nav-icon fas fa-tasks" },
    { name: "Messages", path: "/messages", icon: "nav-icon fas fa-inbox" },
    { name: 'Settings', path: '', icon: 'nav-icon fas fa-cog', menuItems: [
      { name: 'General settings', icon: "far fa-circle nav-icon", path: "/general-settings" },
      { name: 'Seo settings', icon: "far fa-circle nav-icon", path: "/seo-settings" },
      { name: 'Email settings', icon: "far fa-circle nav-icon", path: "/email-settings" },
    ]}];
  
  constructor(private messagesService : MessageService) {
    this.messagesService.getMessages().then((res) => {
      if(!res || res.length === 0) return;

      const newMessages =  res.filter(x => x.messageStatus === MessageStatus.Unread).length !== 0;

      if(newMessages){
        var index = this.menuItems.findIndex((menuItem) => menuItem.path === '/messages');
        this.menuItems[index].badge = { value: "New", style: "right badge badge-danger" };
      }
    });

    this.messagesService.updatedMessagesEventListener((messages) => {
      const newMessages =  messages.filter(x => x.messageStatus === MessageStatus.Unread).length !== 0;

      var index = this.menuItems.findIndex((menuItem) => menuItem.path === '/messages');
      if(newMessages){
        this.menuItems[index].badge = { value: "New", style: "right badge badge-danger" };
      }
      else{
        this.menuItems[index].badge = null
      }
    });
   }

  ngOnInit(): void {
  }

}
