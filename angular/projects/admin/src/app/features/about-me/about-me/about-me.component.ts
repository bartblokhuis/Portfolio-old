import { Component, OnInit } from '@angular/core';
import { AboutMe } from 'projects/shared/src/lib/data/about-me';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { AboutMeService } from 'projects/shared/src/lib/services/api/about-me/about-me.service';
import { NotificationService } from '../../../services/notification/notification.service';

declare var $:any;
@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.scss']
})
export class AboutMeComponent implements OnInit {

  aboutMe : AboutMe = { title: '', content: '' }
  aboutMeForm: any;
  apiError: string | null = null;

  constructor(private aboutMeService: AboutMeService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.aboutMeForm = $('#aboutMeForm');
    this.validateAboutMe();

    this.aboutMeService.get().subscribe((result: Result<AboutMe>) => {
      if(result.succeeded) {
        this.aboutMe = result.data;
      }
      else {
        this.apiError = result.messages[0];
      }
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
          maxlength: 128
        },
      },
      messages: {
        title: {
          required: "Please enter a title",
          maxlength: "Please don't use more than 128 charachter in the title"
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
