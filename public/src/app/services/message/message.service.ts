import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from 'src/app/data/Message';
import { Result } from 'src/app/data/Result';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private apiService: ApiService) { }

  sendMessage(message: Message): Observable<Result> {
    return this.apiService.post<Result>(`Messages`, message);
  }
}
