import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../../data/common/result';
import { Message } from '../../data/Message';
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
