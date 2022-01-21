import { Injectable } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Observable } from 'rxjs';
import { CreateMessage } from '../../../data/messages/create-message';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private apiService: ApiService) { }

  sendMessage(message: CreateMessage): Observable<Result<any>> {
    return this.apiService.post<any>(`Messages`, message);
  }
}
