import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseBlog } from 'src/app/data/blog/base-blog';
import { CreateBlog } from 'src/app/data/blog/create-blog';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from 'src/app/services/api/api.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { validateBlogForm } from '../helpers/blog-helper';

declare var $:any;

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.scss']
})
export class AddBlogComponent implements OnInit {

  model: CreateBlog = { title: '', content: '', description: '', displayNumber: 0, isPublished: false };
  form: any;

  constructor(private contentTitleService: ContentTitleService, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.contentTitleService.title.next("Add blog post");

    this.form = $("#addBlogForm");
    if(this.form) validateBlogForm(this.form);
  }

  createBlog() {

    if(!this.form.valid()) return;

    this.apiService.post<CreateBlog>("Blog", this.model).subscribe((result: Result<CreateBlog>) => {
      this.router.navigate(['blog']);
    });

  }
}
