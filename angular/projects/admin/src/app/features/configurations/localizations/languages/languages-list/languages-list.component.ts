import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { availablePageSizes, baseDataTableOptions } from 'projects/admin/src/app/helpers/datatable-helper';
import { environment } from 'projects/admin/src/environments/environment';
import { Language } from 'projects/shared/src/lib/data/localization/language';
import { LanguageSearch } from 'projects/shared/src/lib/data/localization/language-search';
import { LanguagesService } from 'projects/shared/src/lib/services/api/languages/languages.service';
import { Subject } from 'rxjs';
import { LanguagesAddComponent } from '../languages-add/languages-add.component';
import { LanguagesDeleteComponent } from '../languages-delete/languages-delete.component';
import { LanguagesUpdateComponent } from '../languages-update/languages-update.component';

@Component({
  selector: 'app-languages-list',
  templateUrl: './languages-list.component.html',
  styleUrls: ['./languages-list.component.scss']
})
export class LanguagesListComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  languages: Language[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  baseApiUrl = environment.baseApiUrl;
  
  constructor(private readonly languagesService: LanguagesService, private modalService: NgbModal) { }

  ngOnInit(): void {
    const ajaxOptions = {  
      ajax: (dataTablesParameters: LanguageSearch, callback: any) => {  
        const model : LanguageSearch = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.languagesService.search(model).subscribe((result) => {
          this.languages = result.data.data;
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

  addLanguage(){
    const modal = this.openModal(LanguagesAddComponent);

    modal.result.then((result) => {
      if(result && result == "added") this.refresh();
    });
  }

  edit(language: Language): void {
    const modal = this.openModal(LanguagesUpdateComponent);
    modal.componentInstance.language = language;

    modal.result.then((result) => {
      if(result && result == "updated") this.refresh();
    });
  }

  delete(language: Language): void {
    const modal = this.openModal(LanguagesDeleteComponent);
    modal.componentInstance.language = language;

    modal.result.then((result) => {
      this.refresh();
    });

  }

  openModal(component: any) : NgbModalRef {
    const modal = this.modalService.open(component, { size: 'lg' });
    modal.componentInstance.modalRef = modal;
    return modal;
  }

  refresh(): void {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.ajax.reload();
    })
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
