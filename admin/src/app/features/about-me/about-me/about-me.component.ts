import { Component, OnInit } from '@angular/core';
import { AboutMe } from 'src/app/data/about-me';
import { Result } from 'src/app/data/common/Result';
import { AboutMeService } from 'src/app/services/api/about-me/about-me.service';
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

  constructor(private aboutMeService: AboutMeService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.aboutMeForm = $('#aboutMeForm');
    this.validateAboutMe();

    this.aboutMeService.get().subscribe((result: Result<AboutMe>) => {
      if(result.succeeded) this.aboutMe = result.data;
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

    this.aboutMeService.save(this.aboutMe).subscribe((result) => {
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
