import { Component, OnInit } from '@angular/core';
import { BodyService } from './services/theming/body.service';
import { ThemingService } from './services/theming/theming.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'admin';
  private currentTheme: string = 'dark-mode';

  constructor(private bodyService: BodyService) {}

  ngOnInit(): void {
    this.bodyService.OnInit();
  }
}
