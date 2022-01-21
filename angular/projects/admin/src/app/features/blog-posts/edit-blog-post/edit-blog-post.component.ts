import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlogComment } from '../../../data/blog/comment';
import { EditBlog } from '../../../data/blog/edit-blog';
import { UpdateBlogPicture } from '../../../data/blog/update-blog-picture';
import { Picture } from '../../../data/common/picture';
import { Result } from '../../../data/common/Result';
import { BlogPostsService } from '../../../services/api/blog-posts/blog-posts.service';
import { CommentsService } from '../../../services/api/comments/comments.service';
import { ContentTitleService } from '../../../services/content-title/content-title.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { DeleteCommentComponent } from '../comments/delete-comment/delete-comment.component';
import { ReplyCommentComponent } from '../comments/reply-comment/reply-comment.component';
import { validateBlogForm } from '../helpers/blog-helper';

declare var $: any;

@Component({
  selector: 'app-edit-blog',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EditBlogPostComponent implements OnInit{

  
  model: EditBlog = { content: '', description: '', displayNumber: 0, id: 0, isPublished: true, title: '', metaDescription: '', metaTitle: '', thumbnail: null, thumbnailId: null, bannerPicture: null, bannerPictureId: null, comments: null };
  bannerPicture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' };
  thumbnailPicture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' };
  form: any;
  titleError: string | null = null;

  constructor(private route: ActivatedRoute, private blogPostsService: BlogPostsService, private contentTitleService: ContentTitleService, private router: Router, 
    private notificationService: NotificationService, private modalService: NgbModal, private readonly commentsService: CommentsService) { }

  ngOnInit(): void {

    this.contentTitleService.title.next('Edit blog post')

    const idParam = this.route.snapshot.paramMap.get('id');
    if(!idParam) return;

    const id = parseInt(idParam);
    this.model.id = id;

    this.blogPostsService.getById(id).subscribe((result) => {

      if(!result.succeeded) this.router.navigate(['blog']);
      this.model = result.data;

      if(this.model.bannerPicture) this.bannerPicture = this.model.bannerPicture;
      if(this.model.thumbnail) this.thumbnailPicture = this.model.thumbnail;

    }, () => this.router.navigate(['blog']));

    this.form = $("#editBlogForm");
    if(this.form) validateBlogForm(this.form);
  }

  editBlogPost() {
    this.titleError = null;

    if(!this.form.valid()) return;

    this.blogPostsService.editBlogPost(this.model).subscribe((result: Result<EditBlog>) => {
      if(result.succeeded) this.router.navigate(['blog']);

      this.titleError = result.messages[0];
    });
  }

  updateThumbnailPicture(picture: Picture): void {
    this.thumbnailPicture = picture;
    const model: UpdateBlogPicture = { blogPostId: this.model.id, pictureId: picture.id ?? 0 };

    this.blogPostsService.updateThumbnail(model).subscribe((result) => {
      this.notificationService.success("Updated the banner picture")
    })
  }

  updateBannerPicture(picture: Picture): void {
    this.bannerPicture = picture;
    const model: UpdateBlogPicture = { blogPostId: this.model.id, pictureId: picture.id ?? 0 };

    this.blogPostsService.updateBanner(model).subscribe((result) => {
      this.notificationService.success("Updated the banner picture")
    })
  }

  deleteComment(comment: BlogComment) {
    const modalRef = this.modalService.open(DeleteCommentComponent, { size: 'lg' });

    //const editBlogPost: ListBlog = { createdAtUTC: blog.createdAtUTC, description: blog.description, displayNumber: blog.displayNumber, id: blog.id, isPublished: blog.isPublished, title: blog.title, updatedAtUtc: blog.updatedAtUtc };
    modalRef.componentInstance.comment = comment
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.refreshComments();
    });
  }

  replyToComment(comment: BlogComment) {
    const modalRef = this.modalService.open(ReplyCommentComponent, { size: 'lg' });

    //const editBlogPost: ListBlog = { createdAtUTC: blog.createdAtUTC, description: blog.description, displayNumber: blog.displayNumber, id: blog.id, isPublished: blog.isPublished, title: blog.title, updatedAtUtc: blog.updatedAtUtc };
    modalRef.componentInstance.comment = comment;
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.refreshComments();
    });
  }

  refreshComments(){
    if(!this.model) return;
    this.commentsService.getCommentsByBlogPostId(this.model.id).subscribe((result) => {
      if(result.succeeded) this.model.comments = result.data;
    })
  }
}
