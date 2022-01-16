import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Picture } from 'src/app/data/common/picture';

@Component({
  selector: 'app-edit-picture',
  templateUrl: './edit-picture.component.html',
  styleUrls: ['./edit-picture.component.scss']
})
export class EditPictureComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() picture: Picture = { altAttribute: '', id: 0, mimeType: '', path: '', titleAttribute: ''};
  
  constructor() { }

  ngOnInit(): void {
  }

}
