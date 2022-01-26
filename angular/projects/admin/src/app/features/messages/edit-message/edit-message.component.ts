import { AfterViewInit, Component, Input, OnInit, QueryList, ViewChildren } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { MessageStatus, MessageStatusToLabelMapping } from 'projects/shared/src/lib/data/messages/message-status';
import { MessagesService } from '../../../services/messages/messages.service';
import { NotificationService } from '../../../services/notification/notification.service';


declare var $:any;
@Component({
  selector: 'app-edit-message',
  templateUrl: './edit-message.component.html',
  styleUrls: ['./edit-message.component.scss']
})
export class EditMessageComponent implements OnInit, AfterViewInit{

  @Input() modal: NgbModalRef | undefined;
  @Input() message: Message | undefined;
  @ViewChildren('select2') skills: QueryList<any> | undefined;

  messageStatus: MessageStatus | undefined = undefined;

  constructor(private messagesService: MessagesService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.messageStatus = this.message?.messageStatus;
  }

  ngAfterViewInit(): void {
    const select2 = $('.select2');
    select2.select2();
    select2.on('change', () => this.messageStatus = select2.val());
  }

  edit(){
    if(!this.message || !this.messageStatus) return;

    this.messagesService.update(this.message.id, this.messageStatus).subscribe((result) => {
      if (result.succeeded){
        this.notificationService.success("Updated the message")
      }
      this.modal?.close('success');
    });
  }


  messageTypes = [MessageStatus.Closed, MessageStatus.Read, MessageStatus.Unread, MessageStatus.WaitingResponse];
  mapping = MessageStatusToLabelMapping;
}
