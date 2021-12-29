import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBlogComponent } from './list-blog/list-blog.component';
import { SharedComponentsModule } from 'src/app/components/shared/shared-components.module';
import { BlogRoutes } from './blog.routes';
import { RouterModule } from '@angular/router';
import { AddBlogComponent } from './add-blog/add-blog.component';
import { QuillModule } from 'ngx-quill';
import { FormsModule } from '@angular/forms';
import { EditBlogComponent } from './edit-blog/edit-blog.component';



@NgModule({
  declarations: [
    ListBlogComponent,
    AddBlogComponent,
    EditBlogComponent
  ],
  imports: [
    CommonModule,
    QuillModule,
    FormsModule,
    SharedComponentsModule,
    RouterModule.forChild(BlogRoutes),
  ]
})
export class BlogModule { }
