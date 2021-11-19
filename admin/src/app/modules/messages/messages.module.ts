import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ListMessagesComponent } from './list-messages/list-messages.component';
import { DeleteMessageComponent } from './delete-message/delete-message.component';
import { EditMessageComponent } from './edit-message/edit-message.component';
import { MessageRoutes } from './messages.routes';
import { ComponentsModule } from 'src/app/components/components.module';

@NgModule({
  declarations: [ListMessagesComponent, DeleteMessageComponent, EditMessageComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(MessageRoutes),
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule
  ]
})
export class MessagesModule { }
