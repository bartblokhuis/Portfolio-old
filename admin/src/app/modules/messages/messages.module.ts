import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListMessagesComponent } from './list-messages/list-messages.component';
import { RouterModule } from '@angular/router';
import { MessageRoutes } from './messages.routes';
import { SharedComponentsModule } from 'src/app/components/shared/shared-components.module';
import { EditMessageComponent } from './edit-message/edit-message.component';
import { DeleteMessageComponent } from './delete-message/delete-message.component';



@NgModule({
  declarations: [
    ListMessagesComponent,
    EditMessageComponent,
    DeleteMessageComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    RouterModule.forChild(MessageRoutes),
  ]
})
export class MessagesModule { }
