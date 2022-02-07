import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { environment } from 'projects/admin/src/environments/environment';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { Picture } from 'projects/shared/src/lib/data/common/picture';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { PicturesService } from 'projects/shared/src/lib/services/api/pictures/pictures.service';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { NotificationService } from '../../../services/notification/notification.service';
import { AddPictureComponent } from '../add-picture/add-picture.component';
import { DeletePictureComponent } from '../delete-picture/delete-picture.component';
import { EditPictureComponent } from '../edit-picture/edit-picture.component';

@Component({
  selector: 'app-list-picture',
  templateUrl: './list-picture.component.html',
  styleUrls: ['./list-picture.component.scss']
})
export class ListPictureComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;
  
  baseUrl: string = environment.baseApiUrl;
  pictures: Picture[] | null = null;
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();
  
  constructor(private picturesService: PicturesService, private modalService: NgbModal, private notificationService: NotificationService) { }

  ngOnInit(): void {
    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.picturesService.list(model).subscribe((result) => {
          this.pictures = result.data.data;
          callback({
            recordsTotal: result.data.recordsTotal,
            recordsFiltered: result.data.recordsFiltered,
            draw: result.data.draw
          });
        });
      }
    }

    this.dtOptions = {...baseDataTableOptions, ...ajaxOptions}
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(this.dtOptions);
  }

  uploadPicture() {
    this.openModal(AddPictureComponent).then(() => {
      this.refresh();
    });
  }

  editPicture(picture: Picture) {
    this.openModal(EditPictureComponent, picture).then(() => {
      this.refresh();
    });;
  }

  deletePicture(picture: Picture) {
    this.openModal(DeletePictureComponent, picture).then((result: Result) => {
      if(!result.succeeded){
        this.notificationService.error(result.messages[0])
      }
      else{
        this.refresh();
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

  refresh(): void {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();

      this.dtTrigger.next(this.dtOptions);
    })
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
