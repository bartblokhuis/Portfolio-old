import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Picture } from 'src/app/data/common/picture';
import { Result } from 'src/app/data/common/Result';
import { PicturesService } from 'src/app/services/api/pictures/pictures.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { environment } from 'src/environments/environment';
import { AddPictureComponent } from '../add-picture/add-picture.component';
import { DeletePictureComponent } from '../delete-picture/delete-picture.component';
import { EditPictureComponent } from '../edit-picture/edit-picture.component';

@Component({
  selector: 'app-list-picture',
  templateUrl: './list-picture.component.html',
  styleUrls: ['./list-picture.component.scss']
})
export class ListPictureComponent implements OnInit {

  baseUrl: string = environment.baseApiUrl;
  pictures: Picture[] | null = null;
  
  constructor(private picturesService: PicturesService, private modalService: NgbModal, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadPictures();
  }

  loadPictures(): void {
    this.picturesService.getAll().subscribe(result => this.pictures = result.data);
  }


  uploadPicture() {
    this.openModal(AddPictureComponent).then(() => {
      this.loadPictures();
    });
  }

  editPicture(picture: Picture) {
    this.openModal(EditPictureComponent, picture).then(() => {
      this.loadPictures();
    });;
  }

  deletePicture(picture: Picture) {
    this.openModal(DeletePictureComponent, picture).then((result: Result) => {
      if(!result.succeeded){
        this.notificationService.error(result.messages[0])
      }
      else{
        this.loadPictures();
      }
    });
  }

  openModal(component: any, picture: Picture | null = null ) : Promise<any> {

    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(picture) {
      const modalPicture: Picture = { altAttribute: picture.altAttribute, id: picture.id, mimeType: picture.mimeType, path: picture.path, titleAttribute: picture.titleAttribute  };
      modalRef.componentInstance.picture = modalPicture
    }
    modalRef.componentInstance.modal = modalRef;
    
    return modalRef.result;
  }



}
