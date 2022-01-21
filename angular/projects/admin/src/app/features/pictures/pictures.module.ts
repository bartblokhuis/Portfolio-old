import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListPictureComponent } from './list-picture/list-picture.component';
import { PicturesRoutes } from './pictures.routes';
import { RouterModule } from '@angular/router';
import { AddPictureComponent } from './add-picture/add-picture.component';
import { EditPictureComponent } from './edit-picture/edit-picture.component';
import { DeletePictureComponent } from './delete-picture/delete-picture.component';
import { GridPictureComponent } from './grid-picture/grid-picture.component';
import { SharedComponentsModule } from '../../components/shared/shared-components.module';
import { PictureModule } from '../../components/picture/picture.module';

@NgModule({
  declarations: [
    ListPictureComponent,
    AddPictureComponent,
    EditPictureComponent,
    DeletePictureComponent,
    GridPictureComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    PictureModule,
    RouterModule.forChild(PicturesRoutes),
  ]
})
export class PicturesModule { }
