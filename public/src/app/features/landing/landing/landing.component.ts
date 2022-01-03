import { Component, HostListener, OnInit } from '@angular/core';

declare var AOS: any;

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {

  constructor() { }

  @HostListener("window:load", []) onWindowLoad() {
    if(AOS) AOS.init();
  }

  ngOnInit(): void {
  }

}
