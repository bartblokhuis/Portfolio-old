import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseSearchModel } from '../../../data/common/base-search-model';
import { Result } from '../../../data/common/Result';
import { QueuedEmail } from '../../../data/queued-email/queued-email';
import { QueuedEmailList } from '../../../data/queued-email/queued-email-list';
import { UpdateQueuedEmail } from '../../../data/queued-email/update-queued-email';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class QueuedEmailsService {

  constructor(private readonly apiService: ApiService) { }

  getAll(): Observable<Result<QueuedEmail[]>> {
    return this.apiService.get<QueuedEmail[]>('QueuedEmail')
  }

  getById(id: number): Observable<Result<UpdateQueuedEmail>> {
    return this.apiService.get<UpdateQueuedEmail>(`QueuedEmail/GetById?id=${id}`)
  }

  list(searchModel: BaseSearchModel): Observable<Result<QueuedEmailList>> {
    return this.apiService.post("QueuedEmail/List", searchModel);
  }

  edit(queuedEmail: UpdateQueuedEmail): Observable<Result<QueuedEmail>> {
    return this.apiService.put<QueuedEmail>('QueuedEmail', queuedEmail);
  }

  delete(id: number) {
    return this.apiService.delete(`QueuedEmail?id=${id}`)
  }

  deleteAll(): Observable<Result> {
    return this.apiService.delete(`QueuedEmail/DeleteAll`)
  }
}
