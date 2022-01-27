import { Component, OnInit } from '@angular/core';
import { CreateMessage } from 'projects/shared/src/lib/data/messages/create-message';
import { isValidEmail } from 'projects/shared/src/lib/helpers/common-helpers';
import { MessageService } from 'projects/shared/src/lib/services/api/message/message.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  model: CreateMessage = { email: '', firstName: '', lastName: '', messageContent: '' };
  error: string | undefined = undefined;
  firstNameError: string = '';
  emailError: string = '';
  messageError: string = '';
  success: string = '';
  sendingMessage: boolean = false;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
  }

  sendMessage(): void {
    if(this.sendingMessage) {
      return;
    }
    this.sendingMessage = true;
    this.firstNameError = "";
    this.emailError = "";
    this.messageError = "";
    this.success = "";

    let error = false;
    if(this.model.firstName.length < 3){
      this.firstNameError = "Please enter a name with more than 3 characters";
      error = true;;
    }
    else if(this.model.firstName.length > 128) {
      this.firstNameError = "Please enter a name with less than 128 characters";
    }
    
    if(this.model.email.length === 0) {
      this.emailError = "Please enter your email address";
      error = true;
    }
    else if(!isValidEmail(this.model.email)){
      this.emailError = "Please enter a real email address";
      error = true;
    }

    if(this.model.messageContent.length === 0) {
      this.messageError = "Please enter your message";
      error = true;
    }
    else if(this.model.messageContent.length > 512){
      this.messageError = "Please don't use more than 512 characters in your message";
      error = true;
    }


    if(error) {
      this.sendingMessage = false;
      return;
    }

    this.messageService.sendMessage(this.model).subscribe((result) => {
      if(!result.succeeded) {
        this.error = result.messages[0];
        this.sendingMessage = false;
        return;
      }

      this.sendingMessage = false;
      this.success = "Thanks for contacting me! I received your message and I'll respons as soon as possible";
    }, () => this.sendingMessage = false)
  }
}
