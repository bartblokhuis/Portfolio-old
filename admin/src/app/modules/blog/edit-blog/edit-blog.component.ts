import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EditBlog } from 'src/app/data/blog/edit-blog';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from 'src/app/services/api/api.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { validateBlogForm } from '../helpers/blog-helper';

declare var $: any;

@Component({
  selector: 'app-edit-blog',
  templateUrl: './edit-blog.component.html',
  styleUrls: ['./edit-blog.component.scss']
})
export class EditBlogComponent implements OnInit{

  model: EditBlog = { content: '', description: '', displayNumber: 0, id: 0, isPublished: true, title: '' };
  form: any;
  titleError: string | null = null;

  constructor(private route: ActivatedRoute, private apiService: ApiService, private contentTitleService: ContentTitleService, private router: Router) { }

  ngOnInit(): void {

    this.contentTitleService.title.next('Edit blog post')

    const idParam = this.route.snapshot.paramMap.get('id');
    if(!idParam) return;

    const id = parseInt(idParam);
    this.model.id = id;

    this.apiService.get<EditBlog>(`Blog/GetById/?id=${id}&includeUnPublished=true`).subscribe((result) => {

      if(!result.succeeded) this.router.navigate(['blog']);

      this.model = result.data;
    }, () => this.router.navigate(['blog']));

    this.form = $("#editBlogForm");
    if(this.form) validateBlogForm(this.form);
  }

  editBlogPost() {
    this.titleError = null;

    if(!this.form.valid()) return;

    this.apiService.put<EditBlog>("Blog", this.model).subscribe((result: Result<EditBlog>) => {
      if(result.succeeded) this.router.navigate(['blog']);

      this.titleError = result.messages[0];
    });
  }

}
