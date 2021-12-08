import { Component, OnInit } from '@angular/core';
import { AboutMe } from 'src/app/data/about-me';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

declare var $:any;
@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.scss']
})
export class AboutMeComponent implements OnInit {

  aboutMe : AboutMe = { title: '', content: '' }
  aboutMeForm: any;

  constructor(private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.aboutMeForm = $('#aboutMeForm');
    this.validateAboutMe();

    this.apiService.get<AboutMe>('AboutMe').subscribe((result: AboutMe) => {
      this.aboutMe = result;
    });
  }

  textChanged($event : any) {
    const MAX_LENGTH = 256;
    if ($event.editor.getLength() > MAX_LENGTH) {
      $event.editor.deleteText(MAX_LENGTH, $event.editor.getLength());
    }
  }

  submit(){
    if (!this.aboutMeForm.valid()) return;

    this.apiService.post('AboutMe', this.aboutMe).subscribe((result) => {
      this.notificationService.success('Saved the changes')
    })
  }

  validateAboutMe() {
    this.aboutMeForm.validate({
      rules: {
        title: {
          required: true,
        },
      },
      messages: {
        title: {
          required: "Please enter a title",
        },
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
    });
  }
}
