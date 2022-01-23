import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';

@Component({
  selector: 'app-blog-unsubscribe',
  templateUrl: './blog-unsubscribe.component.html',
  styleUrls: ['./blog-unsubscribe.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class BlogUnsubscribeComponent implements OnInit {

  isUnsubscribed: boolean = false;
  error: string | null = null;

  constructor(private route: ActivatedRoute, private readonly blogSubscribersService: BlogSubscribersService) { }

  ngOnInit(): void {

    const idParam = this.route.snapshot.paramMap.get('id');
    if(!idParam) return;

    this.blogSubscribersService.unsubscribe(idParam).subscribe((result) => {
      this.isUnsubscribed = true;
      if(result.succeeded) {
        this.error = null;
      }
      else {
        this.error = result.messages[0];
      }
    }, () => {
      this.isUnsubscribed = true;
      this.error = "Unkown error, please try again."
    })

  }

}
