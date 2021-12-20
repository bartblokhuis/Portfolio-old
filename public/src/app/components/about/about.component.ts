import { Component, OnInit } from '@angular/core';
import { AboutMe } from 'src/app/data/AboutMe';
import { AboutMeService } from 'src/app/services/about-me/about-me.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {

  aboutMe: AboutMe | null = null;

  constructor(private aboutMeService: AboutMeService) { }

  ngOnInit(): void {
    this.aboutMeService.get().subscribe((result) => {
      if(result.succeeded) this.aboutMe = result.data;
    })
  }

}
