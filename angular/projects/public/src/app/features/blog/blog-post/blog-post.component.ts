import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'projects/admin/src/environments/environment';
import { BlogPost } from 'projects/shared/src/lib/data/blog/blog-post';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { ApiService } from 'projects/shared/src/lib/services/api/api.service';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class BlogPostComponent implements OnInit {

  baseUrl = environment.baseApiUrl;
  blogPost: BlogPost | null = null;
  id: number | null = null;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private apiService: ApiService, private metaService: Meta, private titleService: Title) { }

  ngOnInit(): void {
    let title = this.activatedRoute.snapshot.paramMap.get("id");
    if(!title){
      this.router.navigate([`/`]);
      return;
    }

    if(title.indexOf("+") !== -1) {
      title = title.replace("+", "%2B");
    }

    this.apiService.get<BlogPost>(`BlogPost/GetByTitle?title=${title}&includeUnPublished=false`).subscribe((result: Result<BlogPost>) => {

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

  onCommentCreated($event: BlogComment) {
    this.blogPost?.comments?.unshift($event);
  }

}
