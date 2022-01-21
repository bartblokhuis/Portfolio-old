import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreateBlog } from 'projects/shared/src/lib/data/blog/create-blog';
import { ListBlog } from 'projects/shared/src/lib/data/blog/list-blog';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { BlogPostsService } from 'projects/shared/src/lib/services/api/blog-posts/blog-posts.service';
import { ContentTitleService } from '../../../services/content-title/content-title.service';
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

  constructor(private contentTitleService: ContentTitleService, private blogPostsService: BlogPostsService, private router: Router) { }

  ngOnInit(): void {
    this.contentTitleService.title.next("Add blog post");

    this.form = $("#addBlogForm");
    if(this.form) validateBlogForm(this.form);
  }

  createBlog() {

    if(!this.form.valid()) return;

    this.blogPostsService.createBlogPost(this.model).subscribe((result: Result<ListBlog>) => {
      this.router.navigate([`blog/edit/${result.data.id}`]);
    });

  }
}
