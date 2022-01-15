import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Picture } from 'src/app/data/common/picture';
import { PicturesService } from 'src/app/services/api/pictures/pictures.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-grid-picture',
  templateUrl: './grid-picture.component.html',
  styleUrls: ['./grid-picture.component.scss']
})
export class GridPictureComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  
  baseUrl: string = environment.baseApiUrl;
  pictures: Picture[] | null = null;

  constructor(private picturesService: PicturesService) { }

  ngOnInit(): void {
    this.picturesService.getAll().subscribe(result => this.pictures = result.data);
  }

  selectPicture(picture: Picture): void {
    if(this.modal){
      this.modal.close(picture)
    }
  }

}
