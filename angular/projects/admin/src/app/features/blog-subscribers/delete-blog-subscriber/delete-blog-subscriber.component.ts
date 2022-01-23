import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ListBlogSubscriber } from 'projects/shared/src/lib/data/blog-subscribers/list-blog-subscriber';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-blog-subscriber',
  templateUrl: './delete-blog-subscriber.component.html',
  styleUrls: ['./delete-blog-subscriber.component.scss']
})
export class DeleteBlogSubscriberComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() subscriber: ListBlogSubscriber = { createdAtUTC: new Date(), emailAddress: '', id: '', isDeleted: false, name: '', updatedAtUtc: new Date() };

  constructor(private readonly blogSubscribersService: BlogSubscribersService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove() {
    if(!this.subscriber) return;
    this.blogSubscribersService.unsubscribe(this.subscriber.id).subscribe((result) => {
      if(result.succeeded){
        this.notificationService.success(this.subscriber.name + ' is no longer subscribed');
        this.modal?.close();
      }
    });
  }

}
