import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { ListBlog } from 'projects/shared/src/lib/data/blog/list-blog';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { BlogPostsService } from 'projects/shared/src/lib/services/api/blog-posts/blog-posts.service';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { DeleteBlogPostComponent } from '../delete-blog-post/delete-blog-post.component';

@Component({
  selector: 'app-list-blog',
  templateUrl: './list-blog-post.component.html',
  styleUrls: ['./list-blog-post.component.scss']
})
export class ListBlogPostComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  blogPosts: ListBlog[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private blogPostsService: BlogPostsService, private router: Router, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {
    
    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.blogPostsService.list(model).subscribe((result) => {
          this.blogPosts = result.data.data;
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
      { name: `Posts`, active: true, routePath: 'blog/posts' },
    ]);
  }

  ngAfterViewInit(): void {
      this.dtTrigger.next(this.dtOptions);
  }

  editBlogPost(id: number) {
    this.router.navigate(['/blog/edit', id]);
  }

  deleteBlogPost(blog: ListBlog) {
    const modalRef = this.modalService.open(DeleteBlogPostComponent, { size: 'lg' });

    const editBlogPost: ListBlog = { createdAtUTC: blog.createdAtUTC, description: blog.description, displayNumber: blog.displayNumber, id: blog.id, isPublished: blog.isPublished, title: blog.title, updatedAtUtc: blog.updatedAtUtc, thumbnail: blog.thumbnail };
    modalRef.componentInstance.blogPost = editBlogPost
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.refresh();
    });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  refresh(): void {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();

      this.dtTrigger.next(this.dtOptions);
    })
  }
}
