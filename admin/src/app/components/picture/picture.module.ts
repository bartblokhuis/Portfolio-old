import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PictureComponent } from './picture.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [PictureComponent],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    PictureComponent
  ]
})
export class PictureModule { }
