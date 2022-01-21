import { Injectable } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Message } from 'projects/shared/src/lib/data/messages/message';
import { Observable } from 'rxjs';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private apiService: ApiService) { }

  sendMessage(message: Message): Observable<Result<any>> {
    return this.apiService.post<any>(`Messages`, message);
  }
}
