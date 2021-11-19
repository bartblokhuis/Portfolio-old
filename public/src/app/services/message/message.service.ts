import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from 'src/app/data/Message';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private apiService: ApiService) { }

  sendMessage(message: Message): Observable<Message> {
    return this.apiService.post<Message>(`Messages`, message);
  }
}
