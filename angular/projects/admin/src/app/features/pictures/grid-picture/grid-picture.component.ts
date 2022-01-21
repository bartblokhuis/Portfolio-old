import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'projects/admin/src/environments/environment';
import { Picture } from '../../../data/common/picture';
import { PicturesService } from '../../../services/api/pictures/pictures.service';
import { AddPictureComponent } from '../add-picture/add-picture.component';

@Component({
  selector: 'app-grid-picture',
  templateUrl: './grid-picture.component.html',
  styleUrls: ['./grid-picture.component.scss']
})
export class GridPictureComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  
  baseUrl: string = environment.baseApiUrl;
  pictures: Picture[] | null = null;

  constructor(private picturesService: PicturesService, private readonly modalService: NgbModal) { }

  ngOnInit(): void {
    this.picturesService.getAll().subscribe(result => this.pictures = result.data);
  }

  selectPicture(picture: Picture): void {
    if(this.modal){
      this.modal.close(picture)
    }
  }

  uploadPicture(): void {
    const modalRef = this.modalService.open(AddPictureComponent, { size: 'lg' });
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then((result: Picture) => {
      this.pictures?.push(result);
    })
  }

}
