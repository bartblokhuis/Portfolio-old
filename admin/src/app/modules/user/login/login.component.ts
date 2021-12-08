import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { BodyService } from 'src/app/services/theming/body.service';
import { first } from 'rxjs/operators';
import { ApiService } from 'src/app/services/api/api.service';
import { ActivatedRoute, Router } from '@angular/router';

declare var $: any;

interface loginDetails {
  username: string,
  password: string,
  rememberMe: boolean
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {

  model: loginDetails = { username: '', password: '', rememberMe: false };
  loginForm: any;
  error: string | undefined = undefined;

  constructor(private bodyService: BodyService, private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.loginForm = $('#loginForm');
    this.loginValidaitonRules();
    this.bodyService.setCustomClasses("hold-transition login-page");
  }

  loginValidaitonRules(): void{
    this.loginForm.validate({
      rules: {
        username: {
          required: true,
        },
        password: {
          required: true
        },
        terms: {
          required: true
        },
      },
      messages: {
        email: {
          required: "Please enter a email address"
        },
        password: {
          required: "Please provide a password"
        },
        terms: "Please accept our terms"
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

  login(): void {
    if(!this.loginForm.valid()) return;
    this.authenticationService.login(this.model.username, this.model.password, this.model.rememberMe)
      .pipe(first())
      .subscribe((result) => {
        if(result.error) {
          this.error = result.error;
          console.log(this.error)
          this.model.password = '';
      }
      else {
        location.reload();

        //Don't navigate to a return url because this breaks admin lte.
        //this.router.navigate([this.returnUrl]);
      }
      });

  }
}
