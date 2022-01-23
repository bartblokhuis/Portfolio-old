import { Injectable } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { MessageStatus } from 'projects/shared/src/lib/data/messages/message-status';
import { UpdateMessage } from 'projects/shared/src/lib/data/messages/update-message';
import { ApiService } from 'projects/shared/src/lib/services/api/api.service';
@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  messages: BehaviorSubject<Message[]> = new BehaviorSubject<Message[]>([]);

  constructor(private apiService: ApiService) {
    
  }

  getMessages() : Observable<Result<Message[]>> {

    return this.apiService.get<Message[]>("Messages").pipe(
      map(res => {
        this.messages.next(res.data);
        return res;
      })
    );
  }

  update(id: number, status: MessageStatus): Observable<any> {
    const updateMessage: UpdateMessage = { id : id, messageStatus: parseInt(status.toString())}

    return this.apiService.put('Messages', updateMessage).pipe(
      map(() => {
        const cachedMessages = this.messages.value;
        const index = cachedMessages.findIndex(x => x.id == updateMessage.id);

        //Update the message status in the behaviour subject messages.
        if(index === -1) return;
        cachedMessages[index].messageStatus = updateMessage.messageStatus;

        this.messages.next(cachedMessages);
      })
    )
  }

  deleteMessage(message: Message): Observable<any> {
    return this.apiService.delete(`Messages?id=${message?.id}`).pipe(
      map(() => {
        //remove the message from the cached messages
        this.messages.next(this.messages.value.filter(x => x.id !== message.id));
      })
    )
  }
}