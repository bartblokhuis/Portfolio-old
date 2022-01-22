import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ListBlogSubscriber } from 'projects/shared/src/lib/data/blog-subscribers/list-blog-subscriber';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';
import { DeleteBlogSubscriberComponent } from '../delete-blog-subscriber/delete-blog-subscriber.component';

@Component({
  selector: 'app-list-blog-subscriber',
  templateUrl: './list-blog-subscriber.component.html',
  styleUrls: ['./list-blog-subscriber.component.scss']
})
export class ListBlogSubscriberComponent implements OnInit {

  subscribers: ListBlogSubscriber[] = [];

  constructor(private readonly blogSubscribersService: BlogSubscribersService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadBlogSubscribers();
  }

  loadBlogSubscribers() {
    this.blogSubscribersService.getAll().subscribe((result) => {
      if(result.succeeded) this.subscribers = result.data;
    })
  }

  unsubscribe(subscriber: ListBlogSubscriber) {
    const modalRef = this.modalService.open(DeleteBlogSubscriberComponent, { size: 'lg' });

    modalRef.componentInstance.subscriber = subscriber
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.loadBlogSubscribers();
    });
  }

}
