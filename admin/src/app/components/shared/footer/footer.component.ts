import { Component, OnInit } from '@angular/core';

interface FooterDetails {
  year: number
  name: string
  version: string
  url: string
}

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  footerDetails: FooterDetails = { name: "Bart Blokhuis", url: "https://bartblokhuis.com", version: "1.0.0", year: new Date().getFullYear() }

  constructor() { }

  ngOnInit(): void {
  }

}
