import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Picture } from 'projects/shared/src/lib/data/common/picture';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { PicturesService } from 'projects/shared/src/lib/services/api/pictures/pictures.service';
import { NotificationService } from '../../services/notification/notification.service';


declare var $: any;
@Component({
  selector: 'app-picture',
  templateUrl: './picture.component.html',
  styleUrls: ['./picture.component.scss']
})
export class PictureComponent implements OnInit, AfterViewInit {

  @Input() label: string = "Picture"
  @Input() picture: Picture = { altAttribute: '', id: null, mimeType: '', path: '', titleAttribute: '' }
  @Output() updatedPicture = new EventEmitter<Picture>();
  imageRequiredError: string | null = null;

  formData: FormData | null = null;
  form: any;

  constructor(private picturesService: PicturesService, private notificationService: NotificationService) { }
  
  ngOnInit(): void {
    
  }

  ngAfterViewInit(): void {
    const formId = "#picture-" + this.label;
    this.form = $(formId);
    if(this.form) this.validatePictureForm(this.form);
  }

  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.picture.path = file.name;
      this.formData = new FormData();
      this.formData.append('file', file);
    }
  }

  savePicture(): void {
    this.imageRequiredError = null;
    if(!this.form.valid()) return;

    const isNewPicture = this.picture.id == null;

    if(isNewPicture && this.formData == null){
      this.imageRequiredError = "Please enter an image";
      return;
    }

    const observable = isNewPicture ? this.createPicture() : this.updatePicture();
    observable.subscribe((result) => {
      if(!result.succeeded){
        return;
      }

      this.formData = null;
      this.picture = result.data;

      this.updatedPicture.emit(this.picture);
    })
  }

  createPicture(): Observable<Result<Picture>> {

    if(!this.formData) throw "Expected to have form data";

    let url = "Picture";

    if(this.picture.altAttribute && this.picture.altAttribute.length > 0 && this.picture.titleAttribute && this.picture.titleAttribute.length > 0) {
      url += `?titleAttribute=${this.picture.titleAttribute}&altAttribute=${this.picture.altAttribute}`
    }
    else if(this.picture.titleAttribute && this.picture.titleAttribute.length > 0){
      url += `&titleAttribute=${this.picture.titleAttribute}`;
    }
    else if(this.picture.altAttribute && this.picture.altAttribute.length > 0){
      url += `&altAttribute=${this.picture.altAttribute}`;
    }

    return this.picturesService.create(url, this.formData).pipe(map((result) => {
      if(result.succeeded) {
        this.notificationService.success("Craated the picture");
      }
      return result;
    }));
  }

  updatePicture(): Observable<Result<Picture>> {
    let url = `Picture?pictureId=${this.picture.id}`;
    if(this.picture.titleAttribute.length > 0) {
      url += `&titleAttribute=${this.picture.titleAttribute}`;
    }
    if(this.picture.altAttribute.length > 0) {
      url += `&altAttribute=${this.picture.altAttribute}`;
    }

    if(!this.formData) this.formData = new FormData();

    return this.picturesService.edit(url, this.formData).pipe(map((result) => {
      if(result.succeeded) {
        this.notificationService.success("Updated the picture");
      }
      return result;
    }));
  }

  validatePictureForm(form: any): void {

    let validation = {
      rules: {
        titleAttribute: {
          maxlength: 256
        },
        altAttribute: {
          maxlength: 512
        }
      },
      messages: {
      },
      errorElement: 'span',
      errorPlacement: function (error: any, element: any) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
      },
      highlight: function (element: any, errorClass: any, validClass: any) {
        $(element).addClass('is-invalid');
      },
      unhighlight: function (element: any, errorClass: any, validClass: any) {
        $(element).removeClass('is-invalid');
      }
    }

    Object.defineProperty(validation.rules, this.label, {
      value: { accept: "image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp" },
      writable: true,
      enumerable: true,
      configurable: true
    });

    Object.defineProperty(validation.messages, this.label, {
      value: { accept: "Please enter an image" },
      writable: true,
      enumerable: true,
      configurable: true
    });

    form.validate(validation);
  }

}
