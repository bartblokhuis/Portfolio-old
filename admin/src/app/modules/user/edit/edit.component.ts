import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { UserDetails } from 'src/app/data/User';
import { AuthenticationService } from 'src/app/services/Authentication/authentication.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  detailsForm: FormGroup;
  changePasswordForm: FormGroup;
  userDetails: UserDetails = {username: '', email: ''};

  constructor(private formBuilder: FormBuilder, private authenticationService: AuthenticationService) { 

  }

  ngOnInit(): void {

    this.detailsForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['']
    });

    this.changePasswordForm = this.formBuilder.group({
      oldPassword: [''],
      password: [''],
      confirmNewPassword: ['']
    });

    this.authenticationService.getUserDetails().subscribe((details) => {
      this.detailsForm.controls.username.setValue(details.username);
      this.detailsForm.controls.email.setValue(details.email);
    });
  }

  updateUserDetails(): void {
    this.authenticationService
    .updateUserDetails(this.detailsForm.controls.username.value, this.detailsForm.controls.email.value, this.detailsForm.controls.password.value)
    .subscribe((result) => {

    });
  }

  changePassword(): void {
    this.authenticationService.updatePassword(this.changePasswordForm.controls.password.value, this.changePasswordForm.controls.oldPassword.value)
    .subscribe(() => "");
  }

}
