import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreateBlog } from 'projects/shared/src/lib/data/blog/create-blog';
import { ListBlog } from 'projects/shared/src/lib/data/blog/list-blog';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { BlogPostsService } from 'projects/shared/src/lib/services/api/blog-posts/blog-posts.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { ContentTitleService } from '../../../services/content-title/content-title.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { validateBlogForm } from '../helpers/blog-helper';

declare var $:any;

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.scss']
})
export class AddBlogPostComponent implements OnInit {

  model: CreateBlog = { title: '', content: '', description: '', displayNumber: 0, isPublished: false, metaDescription: '', metaTitle: '' };
  form: any;
  apiError: string | null = null;

  constructor(private contentTitleService: ContentTitleService, private blogPostsService: BlogPostsService, private router: Router, private readonly breadcrumbsService: BreadcrumbsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.contentTitleService.title.next("Add blog post");


    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      { name: "Blog", active: false },
      { name: `Post`, active: false, routePath: 'blog/posts' },
      { name: `New`, active: true, },
    ])

    this.form = $("#addBlogForm");
    if(this.form) validateBlogForm(this.form);
  }

  createBlog() {
    this.apiError = null;
    if(!this.form.valid()) return;

    this.blogPostsService.createBlogPost(this.model).subscribe((result: Result<ListBlog>) => {
      if(result.succeeded) {
        this.notificationService.success("Created the new blog post");
        this.router.navigate([`blog/edit/${result.data.id}`]);
      }
      else {
        this.apiError = result.messages[0];
      }
      
    });

  }
}
