import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ADTSettings } from 'angular-datatables/src/models/settings';
import { data } from 'jquery';
import { ListBlog } from 'projects/shared/src/lib/data/blog/list-blog';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { BlogPostsService } from 'projects/shared/src/lib/services/api/blog-posts/blog-posts.service';
import { Subject } from 'rxjs';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { DeleteBlogPostComponent } from '../delete-blog-post/delete-blog-post.component';

@Component({
  selector: 'app-list-blog',
  templateUrl: './list-blog-post.component.html',
  styleUrls: ['./list-blog-post.component.scss']
})
export class ListBlogPostComponent implements OnInit {

  //d: ADTSettings
  blogPosts: ListBlog[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();
  x: BaseSearchModel = {availablePageSizes: "10,20", draw: "", length: 10, page: 0, pageSize: 10, start: 0};

  constructor(private blogPostsService: BlogPostsService, private router: Router, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {

    const baseOptions = {
      responsive: false,
      autoWidth: false,
      searching: false,
      orderMulti: false,
      ordering: false,
      serverSide: true,
      processing: true,
      lengthMenu: new Array<string>("10", "25", "50", "100"),
    }

    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: "10,25, 50, 100", draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.blogPostsService.list(model).subscribe((result) => {
          this.blogPosts = result.data.data;
          callback({
            recordsTotal: result.data.recordsTotal,
            recordsFiltered: result.data.recordsFiltered
          });
        });
      }
    }

    this.dtOptions = {...baseOptions, ...ajaxOptions}
    console.log(this.dtOptions);

    this.dtTrigger.next(this.dtOptions);
    //this.loadBlog();

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      { name: "Blog", active: false },
      { name: `Posts`, active: true, routePath: 'blog/posts' },
    ]);
  }

  loadBlog() : void {
    this.blogPostsService.getAll().subscribe((result: Result<ListBlog[]>) => {
      this.blogPosts = result.data;

      this.dtTrigger.next(this.dtOptions);
    });

    this.blogPostsService.list(this.x).subscribe((result) => {
      console.log(result);
    })
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
      this.loadBlog();
    });
  }
}
