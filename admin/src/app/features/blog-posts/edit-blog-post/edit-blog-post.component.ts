import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EditBlog } from 'src/app/data/blog/edit-blog';
import { UpdateBlogPicture } from 'src/app/data/blog/update-blog-picture';
import { Picture } from 'src/app/data/common/picture';
import { Result } from 'src/app/data/common/Result';
import { BlogPostsService } from 'src/app/services/api/blog-posts/blog-posts.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { validateBlogForm } from '../helpers/blog-helper';

declare var $: any;

@Component({
  selector: 'app-edit-blog',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EditBlogPostComponent implements OnInit{

  model: EditBlog = { content: '', description: '', displayNumber: 0, id: 0, isPublished: true, title: '', metaDescription: '', metaTitle: '', thumbnail: null, thumbnailId: null, bannerPicture: null, bannerPictureId: null };
  bannerPicture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' };
  thumbnailPicture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' };
  form: any;
  titleError: string | null = null;

  constructor(private route: ActivatedRoute, private blogPostsService: BlogPostsService, private contentTitleService: ContentTitleService, private router: Router, private notificationService: NotificationService) { }

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
}
