import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private router: Router) {
    const titleStart = "Portfolio | ";
    router.events.forEach((event) => {
      if(event instanceof NavigationEnd) {

        var url = router.url.replace("/", "");
        if(url.indexOf("-") !== -1){
          url = url.replace("-", " ")
        }
        if(url.indexOf("/") !== -1){
          url = url.replace("/", " ")
        }

        url = url.charAt(0).toUpperCase() + url.slice(1);

        document.title = titleStart + url;
      }
    });
  }
}
