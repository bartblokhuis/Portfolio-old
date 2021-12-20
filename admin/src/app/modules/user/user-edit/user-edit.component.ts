import { Component, OnInit } from '@angular/core';
import { Result } from 'src/app/data/common/Result';
import { ChangeUserPassword } from 'src/app/data/user/change-password';
import { UserDetails } from 'src/app/data/user/user-details';
import { ApiService } from 'src/app/services/api/api.service';
import { BreadcrumbsService } from 'src/app/services/breadcrumbs/breadcrumbs.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

declare var $:any;

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {

  userDetails: UserDetails = { email: '', password: '', username: '' }
  userDetailsForm: any;
  userDetailsError: string | undefined;

  changePassword: ChangeUserPassword = { oldPassword: '', password: '' }
  changePasswordForm: any;
  changePasswordErrors: string[] | undefined;

  private detailsUrl = "user/details";

  constructor(private readonly BreadcrumbService: BreadcrumbsService, private readonly contentTitleService: ContentTitleService, private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {

    this.userDetailsForm = $("#user-details-from");
    this.validateUserDetails();

    this.changePasswordForm = $("#change-password-form");
    this.validateChangePassword();

    this.BreadcrumbService.setBreadcrumb([
      {
        name: 'User',
        path: undefined,
        active: true
      },
      {
        name: 'Details',
        path: undefined,
        active: true
      }
    ]);

    this.contentTitleService.title.next("User Details");

    this.apiService.get<UserDetails>(this.detailsUrl).subscribe((result: Result<UserDetails>) => {
      if(result.succeeded) this.userDetails = result.data;
    })
  }

  validateUserDetails(): void {
    this.userDetailsForm.validate({
      rules: {
        username: {
          required: true,
        },
        email: {
          required: true,
          email: true
        },
        password: {
          required: true,
        }
      },
      messages: {
        username: {
          required: "Please enter a username",
        },
        email: {
          required: 'Please enter an email address',
          email: "Please enter a valid email address"
        },
        password: {
          required: "Please enter your password"
        }
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

  validateChangePassword(): void {
    this.changePasswordForm.validate({
      rules: {
        oldPassword: {
          required: true,
        },
        password: {
          required: true,
        },
        confirmPassword: {
          equalTo: '#new-password'
        }
      },
      messages: {
        oldPassword: {
          required: "Please enter your current password",
        },
        password: {
          required: 'Please enter a new password',
        }
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

  submit(): void {
    if(!this.userDetailsForm.valid()) return;
    this.apiService.put(this.detailsUrl, this.userDetails).subscribe((result: Result<any>) => {
      if(!result.succeeded){
        this.userDetailsError = result.messages[0];
        this.notificationService.warning("Couldn't save the settings")
        return;
      }
      this.notificationService.success('Saved the changes');
    });
  }

  submitChangePassword(): void {
    if(!this.changePasswordForm.valid()) return;
    this.apiService.put('user/updatePassword', this.changePassword).subscribe((result: Result<any>) => {
      if(!result.succeeded){
        this.changePasswordErrors = result.messages;
        this.notificationService.warning("Couldn't save the settings")
        return;
      }
      this.notificationService.success('Saved the changes');
    });
  }
}
