import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CreateBlogSubscriber } from 'projects/shared/src/lib/data/blog-subscribers/create-blog-subscriber';
import { isValidEmail } from 'projects/shared/src/lib/helpers/common-helpers';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';

@Component({
  selector: 'app-blog-subscribe',
  templateUrl: './blog-subscribe.component.html',
  styleUrls: ['./blog-subscribe.component.scss']
})
export class BlogSubscribeComponent implements OnInit {

  @ViewChild('emailAddress') emailAddress!: ElementRef;

  error: string | null = null;
  success: string | null = null;

  constructor(private readonly blogSubscribersService: BlogSubscribersService) { }

  ngOnInit(): void {
  }

  subscribe(emailAddres: string) {

    this.error = null;
    this.success = null;

    if(!emailAddres || !isValidEmail(emailAddres)){
      this.error = "Please enter a real email";
      return;
    }

    const model: CreateBlogSubscriber = { emailAddress: emailAddres, name: '' }

    this.blogSubscribersService.subscribe(model).subscribe((result) => {
      if(result.succeeded) {
        if(this.emailAddress) this.emailAddress.nativeElement.value = '';
        this.success = "Thanks for subscribing!";
        return;
      }

      this.error = result.messages[0];
      
    })
    
  }

}
