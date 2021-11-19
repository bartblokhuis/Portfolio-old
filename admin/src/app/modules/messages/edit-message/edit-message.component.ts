import { Component, OnInit, Input, Inject } from '@angular/core';
import { Message, MessageStatusToLabelMapping, MessageStatus } from 'src/app/data/Messages/Message';
import { UpdateMessage } from 'src/app/data/Messages/UpdateMessage';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup } from '@angular/forms';
import { MessageService } from 'src/app/services/messages/message.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-message',
  templateUrl: './edit-message.component.html',
  styleUrls: ['./edit-message.component.scss']
})
export class EditMessageComponent implements OnInit {

  @Input() message: Message;
  @Input() afterInit: Function;
  @Input() modalRef: NgbModalRef;

  editMessageForm = new FormGroup({
    messageStatus: new FormControl('')
  });

  constructor(private messageService: MessageService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.editMessageForm.controls.messageStatus.setValue(this.message.messageStatus);
  }

  mapping = MessageStatusToLabelMapping;
  messageTypes = [MessageStatus.Closed, MessageStatus.Read, MessageStatus.Unread, MessageStatus.WaitingResponse];

  close(){
    this.modalRef.close();
  }

  save() {
    var values = this.editMessageForm.value;

    var updateMessage: UpdateMessage = {
      id: this.message.id,
      messageStatus: parseInt(values.messageStatus)
    };

    this.messageService.editMessage(updateMessage).then(() => {
      this.modalRef.close();
      this.toastr.success("Updated message status");
    });
  }


}
