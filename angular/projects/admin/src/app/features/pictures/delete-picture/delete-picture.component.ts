import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Picture } from 'projects/shared/src/lib/data/common/picture';
import { PicturesService } from 'projects/shared/src/lib/services/api/pictures/pictures.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-picture',
  templateUrl: './delete-picture.component.html',
  styleUrls: ['./delete-picture.component.scss']
})
export class DeletePictureComponent implements OnInit {

  @Input() picture: Picture = { altAttribute: '', id: 0, mimeType: '', path: '', titleAttribute: ''};
  @Input() modal: NgbModalRef | undefined;

  constructor(private picturesService: PicturesService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove() {

    if(!this.picture.id) return;

    this.picturesService.delete(this.picture.id).subscribe((result) => {
      if(result.succeeded) {
        this.notificationService.success("Removed the picture");
      }
      this.modal?.close(result);
    })
  }

}
