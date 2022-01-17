import { Component, OnInit } from '@angular/core';
import { BodyService } from './services/theming/body.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'admin';

  constructor(private bodyService: BodyService) {}

  ngOnInit(): void {
    this.bodyService.OnInit();
  }
}
