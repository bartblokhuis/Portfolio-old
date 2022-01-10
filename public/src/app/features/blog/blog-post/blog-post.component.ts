import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/data/blog/blog';
import { Result } from 'src/app/data/common/result';
import { ApiService } from 'src/app/services/common/api.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.scss']
})
export class BlogPostComponent implements OnInit {

  baseUrl = environment.baseApiUrl;
  blogPost: Blog | null = null;
  id: number | null = null;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private apiService: ApiService, private metaService: Meta, private titleService: Title) { }

  ngOnInit(): void {
    const idParam = this.activatedRoute.snapshot.paramMap.get("id");
    if(!idParam){
      this.router.navigate([`/`]);
      return;
    }

    const id = parseInt(idParam);
    if(Number.isNaN(id)){
      this.router.navigate([`blog`]);
      return;
    }

    this.apiService.get<Blog>(`Blog/GetById?id=${id}&includeUnPublished=false`).subscribe((result: Result<Blog>) => {

      if(!result.succeeded) this.router.navigate([`blog`]);

      this.blogPost = result.data;

      if(this.blogPost.metaTitle){
        this.titleService.setTitle(this.blogPost.metaTitle);
        this.metaService.addTag({ name: 'twitter:title', content: this.blogPost.metaTitle});
      }
      if(this.blogPost.metaDescription){
        this.metaService.addTag({ name:'description', content: this.blogPost.metaDescription});
        this.metaService.addTag({ name: 'og:title', content: this.blogPost.metaDescription});
        this.metaService.addTag({ name: 'twitter:description', content: this.blogPost.metaDescription});
      }

    }, error => this.router.navigate([`blog`]));
  }

}
