import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EditBlog } from 'src/app/data/blog/edit-blog';
import { Result } from 'src/app/data/common/Result';
import { Picture } from 'src/app/data/common/picture';
import { ApiService } from 'src/app/services/api/api.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { validateBlogForm } from '../helpers/blog-helper';
import { UpdateBlogPicture } from 'src/app/data/blog/update-blog-picture';
import { NotificationService } from 'src/app/services/notification/notification.service';
import QuillType, { Delta } from 'quill'

declare var $: any;

@Component({
  selector: 'app-edit-blog',
  templateUrl: './edit-blog.component.html',
  styleUrls: ['./edit-blog.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EditBlogComponent implements OnInit{

  model: EditBlog = { content: '', description: '', displayNumber: 0, id: 0, isPublished: true, title: '', metaDescription: '', metaTitle: '', thumbnail: null, thumbnailId: null, bannerPicture: null, bannerPictureId: null };
  bannerPicture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' };
  thumbnailPicture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' };
  form: any;
  titleError: string | null = null;
  blogContent: string | null = "";

  constructor(private route: ActivatedRoute, private apiService: ApiService, private contentTitleService: ContentTitleService, private router: Router, private notificationService: NotificationService) { }

  ngOnInit(): void {

    this.contentTitleService.title.next('Edit blog post')

    const idParam = this.route.snapshot.paramMap.get('id');
    if(!idParam) return;

    const id = parseInt(idParam);
    this.model.id = id;

    this.apiService.get<EditBlog>(`Blog/GetById/?id=${id}&includeUnPublished=true`).subscribe((result) => {

      if(!result.succeeded) this.router.navigate(['blog']);

      this.blogContent = result.data.content;
      console.log(this.blogContent);
     // result.data.content = "";
      this.model = result.data;

      if(this.model.bannerPicture) this.bannerPicture = this.model.bannerPicture;
      if(this.model.thumbnail) this.thumbnailPicture = this.model.thumbnail;

    }, () => this.router.navigate(['blog']));

    this.form = $("#editBlogForm");
    if(this.form) validateBlogForm(this.form);
  }

  blogContentQuillLoadad(quill: QuillType): void {
    //if(this.blogContent) quill.clipboard.dangerouslyPasteHTML(this.blogContent);
  }

  editBlogPost() {
    this.titleError = null;

    if(!this.form.valid()) return;

    this.apiService.put<EditBlog>("Blog", this.model).subscribe((result: Result<EditBlog>) => {
      if(result.succeeded) this.router.navigate(['blog']);

      this.titleError = result.messages[0];
    });
  }

  updateThumbnailPicture(picture: Picture): void {
    this.thumbnailPicture = picture;
    const model: UpdateBlogPicture = { blogPostId: this.model.id, pictureId: picture.id ?? 0 };

    this.apiService.put("Blog/UpdateThumbnailPicture", model).subscribe((result) => {
      this.notificationService.success("Updated the banner picture")
    })
  }

  updateBannerPicture(picture: Picture): void {
    this.bannerPicture = picture;
    const model: UpdateBlogPicture = { blogPostId: this.model.id, pictureId: picture.id ?? 0 };

    this.apiService.put("Blog/UpdateBannerPicture", model).subscribe((result) => {
      this.notificationService.success("Updated the banner picture")
    })
  }
}
