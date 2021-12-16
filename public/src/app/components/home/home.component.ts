import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  scrollToAboutMe(): void {
    const home = document.getElementById("home");
    if(!home ) return;

    var homeEndsAt = home.clientHeight;

    window.scrollTo({
      top: homeEndsAt + 45,
      left: 0,
      behavior : "smooth"
    });
  }

}
