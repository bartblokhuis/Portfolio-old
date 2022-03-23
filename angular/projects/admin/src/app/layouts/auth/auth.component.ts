import { Component, OnInit } from '@angular/core';
import { ThemingService } from '../../services/theming/theming.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {

  constructor(private readonly themingService: ThemingService) { }

  ngOnInit(): void {
    this.themingService.initialize();
  }

}
