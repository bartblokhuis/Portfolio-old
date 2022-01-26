import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { EditBlog } from 'projects/shared/src/lib/data/blog/edit-blog';
import { UpdateBlogPicture } from 'projects/shared/src/lib/data/blog/update-blog-picture';
import { Picture } from 'projects/shared/src/lib/data/common/picture';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { BlogPostsService } from 'projects/shared/src/lib/services/api/blog-posts/blog-posts.service';
import { CommentsService } from 'projects/shared/src/lib/services/api/comments/comments.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
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
  apiError: string | null = null;

  constructor(private route: ActivatedRoute, private blogPostsService: BlogPostsService, private contentTitleService: ContentTitleService, private router: Router, 
    private notificationService: NotificationService, private modalService: NgbModal, private readonly commentsService: CommentsService, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {

    this.contentTitleService.title.next('Edit blog post');

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      { name: "Blog", active: false },
      { name: `Posts`, active: false, routePath: 'blog/posts' },
      { name: 'Edit', active: true}
    ])

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
    this.apiError = null;

    if(!this.form.valid()) return;

    this.blogPostsService.editBlogPost(this.model).subscribe((result: Result<EditBlog>) => {

      if(result.succeeded) {
        this.notificationService.success("Updated the blog post");
        this.router.navigate(['blog']);
      } 
      else {
        this.apiError = result.messages[0];
      }
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

    modalRef.componentInstance.comment = comment
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.refreshComments();
    });
  }

  replyToComment(comment: BlogComment) {
    const modalRef = this.modalService.open(ReplyCommentComponent, { size: 'lg' });

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
