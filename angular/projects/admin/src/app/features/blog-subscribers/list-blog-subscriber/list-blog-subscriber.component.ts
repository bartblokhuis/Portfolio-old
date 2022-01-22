import { Component, OnInit } from '@angular/core';
import { ListBlogSubscriber } from 'projects/shared/src/lib/data/blog-subscribers/list-blog-subscriber';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';

@Component({
  selector: 'app-list-blog-subscriber',
  templateUrl: './list-blog-subscriber.component.html',
  styleUrls: ['./list-blog-subscriber.component.scss']
})
export class ListBlogSubscriberComponent implements OnInit {

  subscribers: ListBlogSubscriber[] = [];

  constructor(private readonly blogSubscribersService: BlogSubscribersService) { }

  ngOnInit(): void {
    this.loadBlogSubscribers();
  }

  loadBlogSubscribers() {
    this.blogSubscribersService.getAll().subscribe((result) => {
      if(result.succeeded) this.subscribers = result.data;
    })
  }

unsubscribe(subscriber: ListBlogSubscriber) {

}

}
