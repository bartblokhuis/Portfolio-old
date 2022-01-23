import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListMessagesComponent } from './list-messages/list-messages.component';
import { RouterModule } from '@angular/router';
import { MessageRoutes } from './messages.routes';
import { EditMessageComponent } from './edit-message/edit-message.component';
import { DeleteMessageComponent } from './delete-message/delete-message.component';
import { SharedComponentsModule } from '../../components/shared/shared-components.module';
import { PipesModule } from '../../pipes/pipes.module';



@NgModule({
  declarations: [
    ListMessagesComponent,
    EditMessageComponent,
    DeleteMessageComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    PipesModule,
    RouterModule.forChild(MessageRoutes),
  ]
})
export class MessagesModule { }
