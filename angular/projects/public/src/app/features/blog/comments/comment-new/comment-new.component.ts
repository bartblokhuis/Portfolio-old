import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { CreateBlogComent } from 'projects/shared/src/lib/data/blog/create-blog-comment';
import { BlogPostsService } from 'projects/public/src/app/services/blogPosts/blog-posts.service';
import { BlogPost } from 'projects/shared/src/lib/data/blog/blog-post';

@Component({
  selector: 'app-comment-new',
  templateUrl: './comment-new.component.html',
  styleUrls: ['./comment-new.component.scss']
})
export class CommentNewComponent implements OnInit {

  @Input() blogPost: BlogPost | null = null;
  @Output() onCommentCreated: EventEmitter<BlogComment> = new EventEmitter<BlogComment>();

  model : CreateBlogComent = { blogPostId: null, content: '', name: '', parentCommentId: null, email: '' };
  error: string | null = null;
  nameError: string | null = null;
  emailError: string | null = null;
  commentError: string | null = null;

  constructor(private readonly blogPostsService: BlogPostsService) { }

  ngOnInit(): void {
    if(this.blogPost != null){
      this.model.blogPostId = this.blogPost?.id;
    }
  }

  createComment() {

    let hasError: boolean = false;
    if(!this.model.name || this.model.name.length < 1){
      this.nameError = "Please enter a name";
      hasError = true;
    }

    if(this.model.name.length > 16){
      this.nameError = "Please enter a name that has less than 16 charachters";
      hasError = true;
    }

   if(this.model.email && this.model.email.length > 0 && !isEmail(this.model.email)){
      this.emailError = "Please enter a valid email address";
      hasError = true;
    }

    if(!this.model.content || this.model.content.length < 1){
      this.commentError = "Please enter your comment";
      hasError = true;
    }

    if(hasError) return;

    this.blogPostsService.postComment(this.model).subscribe((result) => {
      if(!result.succeeded){
        this.error = result.messages[0];
      }
      if(result.succeeded) {
        this.onCommentCreated.emit(result.data);

        //Clear the form
        this.model.content = '';
        this.model.email = '';
        this.model.name = '';
      }
    });

  }
}

export function isEmail(search:string): boolean {
  const regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
  return regexp.test(search);
}
