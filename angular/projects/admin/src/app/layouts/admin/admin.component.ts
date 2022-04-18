import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'projects/shared/src/lib/services/api/authentication/authentication.service';
import { ThemingService } from '../../services/theming/theming.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  finishedLoading: boolean = false;

  constructor(private readonly authenticationService: AuthenticationService, private readonly themingService: ThemingService) { }

  ngOnInit(): void {
    //Get the current user.
    this.authenticationService.getCurrentUser().subscribe((result) => {
      this.finishedLoading = true;
      this.themingService.initialize();
    }, () => this.authenticationService.logout());
  }

}
