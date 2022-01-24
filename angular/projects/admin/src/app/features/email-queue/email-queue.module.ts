import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { RichTextEditorModule } from '../../components/rich-text-editor/rich-text-editor.module';
import { PipesModule } from '../../pipes/pipes.module';
import { DeleteAllEmailQueueComponent } from './delete-all-email-queue/delete-all-email-queue.component';
import { DeleteEmailQueueComponent } from './delete-email-queue/delete-email-queue.component';
import { EditEmailQueueComponent } from './edit-email-queue/edit-email-queue.component';
import { EmailQueueRoutes } from './email-queue.routes';
import { ListEmailQueueComponent } from './list-email-queue/list-email-queue.component';


@NgModule({
  declarations: [
    ListEmailQueueComponent,
    DeleteEmailQueueComponent,
    DeleteAllEmailQueueComponent,
    EditEmailQueueComponent
  ],
  imports: [
    CommonModule,
    PipesModule,
    FormsModule,
    RichTextEditorModule,
    RouterModule.forChild(EmailQueueRoutes)
  ]
})
export class EmailQueueModule { }
