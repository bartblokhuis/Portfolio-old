import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { QueuedEmail } from 'projects/shared/src/lib/data/queued-email/queued-email';
import { UpdateQueuedEmail } from 'projects/shared/src/lib/data/queued-email/update-queued-email';
import { QueuedEmailsService } from 'projects/shared/src/lib/services/api/queued-emails/queued-emails.service';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { DeleteAllEmailQueueComponent } from '../delete-all-email-queue/delete-all-email-queue.component';
import { DeleteEmailQueueComponent } from '../delete-email-queue/delete-email-queue.component';
import { EditEmailQueueComponent } from '../edit-email-queue/edit-email-queue.component';

@Component({
  selector: 'app-list-email-queue',
  templateUrl: './list-email-queue.component.html',
  styleUrls: ['./list-email-queue.component.scss']
})
export class ListEmailQueueComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;
  
  queuedEmails: QueuedEmail[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private readonly queuedEmailsService: QueuedEmailsService, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {

    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.queuedEmailsService.list(model).subscribe((result) => {
          this.queuedEmails = result.data.data;
          callback({
            recordsTotal: result.data.recordsTotal,
            recordsFiltered: result.data.recordsFiltered,
            draw: result.data.draw
          });
        });
      }
    }

    this.dtOptions = {...baseDataTableOptions, ...ajaxOptions}

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'System',
        url: undefined,
        active: true
      },
      {
        name: 'Email queue',
        url: undefined,
        active: true
      }
    ]);
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(this.dtOptions);
  }

  edit(queuedEmail: QueuedEmail) {
    this.openModal(EditEmailQueueComponent, queuedEmail);
  }

  delete(queuedEmail: QueuedEmail) {
    this.openModal(DeleteEmailQueueComponent, queuedEmail);
  }

  deleteAll() {
    this.openModal(DeleteAllEmailQueueComponent)
  }

  openModal(component: any, queuedEmail?: QueuedEmail) {
    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(queuedEmail){
      var email: UpdateQueuedEmail = { from: queuedEmail.from, fromName: queuedEmail.fromName, id: queuedEmail.id, sentTries: queuedEmail.sentTries, subject: queuedEmail.subject, to: queuedEmail.to, toName: queuedEmail.toName, body: '' };
      modalRef.componentInstance.queuedEmail = email;
    }
    
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.refresh();
    });
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
