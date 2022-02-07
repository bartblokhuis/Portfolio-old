import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { ListBlogSubscriber } from 'projects/shared/src/lib/data/blog-subscribers/list-blog-subscriber';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { DeleteBlogSubscriberComponent } from '../delete-blog-subscriber/delete-blog-subscriber.component';

@Component({
  selector: 'app-list-blog-subscriber',
  templateUrl: './list-blog-subscriber.component.html',
  styleUrls: ['./list-blog-subscriber.component.scss']
})
export class ListBlogSubscriberComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  subscribers: ListBlogSubscriber[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private readonly blogSubscribersService: BlogSubscribersService, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {
    
    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.blogSubscribersService.list(model).subscribe((result) => {
          this.subscribers = result.data.data;
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
      { name: "Blog", active: false },
      { name: `Subscribers`, active: true, routePath: 'blog/subscribers'},
    ])
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(this.dtOptions);
  }

  unsubscribe(subscriber: ListBlogSubscriber) {
    const modalRef = this.modalService.open(DeleteBlogSubscriberComponent, { size: 'lg' });

    modalRef.componentInstance.subscriber = subscriber
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
