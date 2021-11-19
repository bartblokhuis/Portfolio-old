import { Component, Input } from '@angular/core';
import { Message } from 'src/app/data/Messages/Message';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'src/app/services/messages/message.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete-message',
  templateUrl: './delete-message.component.html',
  styleUrls: ['./delete-message.component.scss']
})
export class DeleteMessageComponent {

  @Input() message: Message;
  @Input() modalRef: NgbModalRef;

  constructor(private messageService: MessageService, private toastr: ToastrService){
  }

  close(){
    this.modalRef.close();
  }

  remove(id: number){
    this.messageService.deleteMessage(id).then(() => {
      this.modalRef.close();
      this.toastr.success("Removed message");
    });
  }

}
