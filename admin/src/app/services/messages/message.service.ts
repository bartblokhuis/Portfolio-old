import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Message } from 'src/app/data/Messages/Message';
import { UpdateMessage } from 'src/app/data/Messages/UpdateMessage';
import { environment } from 'src/environments/environment';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient, private apiService: ApiService) {
   }

   cachedMessages: Message[] | null = null;

  async getMessages(reloadCache: boolean = false): Promise<Message[]> {
    if(!reloadCache && this.cachedMessages) return this.cachedMessages;

    const messages = await this.apiService.get<Message[]>(`Messages`).toPromise();
    this.cachedMessages = messages;

    return messages;
  }

  createMessage(message: Message): Observable<Message> {
    return this.http.post<Message>(`${environment.baseApiUrl}Messages`, message).pipe((res) => {
      this.updatedMessagesEventPublisher();
      return res;
    });
  }

  async editMessage(updateMessage: UpdateMessage): Promise<Message> {
    return this.http.put<Message>(`${environment.baseApiUrl}Messages`, updateMessage).toPromise()
      .then((res) => {
        this.updatedMessagesEventPublisher();
        return res;
      });
  }

  deleteMessage(messageId: number): Promise<Object> {
    return this.http.delete<Message>(`${environment.baseApiUrl}Messages?messageId=${messageId}`).toPromise()
      .then((res) => {
        this.updatedMessagesEventPublisher();
        return res;
      });
  }

  updatedMessagesEventPublisher() {

    this.getMessages(true).then((messages) => {
      this.eventListeners.forEach(func => {
        func(messages);
      });
    })
  }

  eventListeners: { (messages: Message[]): void; }[] = [];

  updatedMessagesEventListener(callback: { (messages: Message[]): void }) {
    this.eventListeners.push(callback);
  }
}
