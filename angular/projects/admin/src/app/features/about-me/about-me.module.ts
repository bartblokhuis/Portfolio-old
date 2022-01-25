import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutMeComponent } from './about-me/about-me.component';
import { RouterModule } from '@angular/router';
import { AboutMeRoutes } from './about-me.routes';
import { FormsModule } from '@angular/forms';
import { RichTextEditorModule } from '../../components/rich-text-editor/rich-text-editor.module';



@NgModule({
  declarations: [
    AboutMeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RichTextEditorModule,
    RouterModule.forChild(AboutMeRoutes),
  ]
})
export class AboutMeModule { }
