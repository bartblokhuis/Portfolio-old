import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { MessagesService } from '../../../services/messages/messages.service';
import { DeleteMessageComponent } from '../delete-message/delete-message.component';
import { EditMessageComponent } from '../edit-message/edit-message.component';

@Component({
  selector: 'app-list-messages',
  templateUrl: './list-messages.component.html',
  styleUrls: ['./list-messages.component.scss']
})
export class ListMessagesComponent implements OnInit {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  messages: Message[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private messagesService: MessagesService, private modalService: NgbModal) { }

  ngOnInit(): void {
    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.messagesService.list(model).subscribe((result) => {
          this.messages = result.data.data;
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

  edit(message: Message): void {
    const modal = this.openModal(EditMessageComponent);
    modal.componentInstance.message = message;

    modal.result.then(() => {
      this.refresh();
    });
  }

  delete(message: Message): void {
    const modal = this.openModal(DeleteMessageComponent);
    modal.componentInstance.message = message;

    modal.result.then((result) => {
      this.refresh();
    });

  }

  openModal(component: any) : NgbModalRef {
    const modal = this.modalService.open(component, { size: 'lg' });
    modal.componentInstance.modal = modal;
    return modal;
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
