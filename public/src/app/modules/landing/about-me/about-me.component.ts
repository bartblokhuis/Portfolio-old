import { Component, Input, OnInit } from '@angular/core';
import { AboutMe } from 'src/app/data/AboutMe';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.scss']
})
export class AboutMeComponent implements OnInit {

  @Input() aboutMe: AboutMe | null = { title: '', content: ''};
  constructor() { }

  ngOnInit(): void {
  }

}
