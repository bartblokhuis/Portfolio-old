import { Component, Input, OnInit } from '@angular/core';
import { GeneralSettings } from 'src/app/data/GeneralSettings';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  @Input() generalSettings: GeneralSettings | null = null;

  //Get the current year
  year: number = new Date().getFullYear();

  constructor() { }

  ngOnInit(): void {
  }

}
