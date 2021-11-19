import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { GeneralSettings } from 'src/app/data/GeneralSettings';
import { Message } from 'src/app/data/Message';
import { MessageService } from 'src/app/services/message/message.service';

@Component({
  selector: 'app-contact-me',
  templateUrl: './contact-me.component.html',
  styleUrls: ['./contact-me.component.scss']
})
export class ContactMeComponent implements OnInit {

  @Input() generalSettings: GeneralSettings | null = null;
  isContactMeOpen: boolean = false;

  sendMessageForm: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    messageContent: new FormControl('')
  });

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
  }
  openContactMe(): void {
    var body = document.getElementById("contactMeBody");

    if(!this.isContactMeOpen){
      body?.classList.add("open");
    }
    else{
      body?.classList.remove("open");
    }

    this.isContactMeOpen = !this.isContactMeOpen;
  }

  sendMessage(): void {
    const button = document.getElementById("submitContactMe");

    if(button?.classList.contains("successfull")){
      button.classList.remove("successfull")
    }

    button?.classList.add("loading");
    button?.classList.remove("default")
    const message: Message = { 
      firstName: this.sendMessageForm.controls['firstName'].value,
      lastName: this.sendMessageForm.controls['lastName'].value,
      email: this.sendMessageForm.controls['email'].value,
      messageContent: this.sendMessageForm.controls['messageContent'].value,
    };

    this.messageService.sendMessage(message).subscribe(() => {
      button?.classList.add("successfull");
      button?.classList.remove("loading");
    });
  }

}
