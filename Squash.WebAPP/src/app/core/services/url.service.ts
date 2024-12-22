import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base/base.service';
import { Url } from '../../models/url.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UrlService extends BaseService<Url> {
  getByUserId(id: number): Observable<Url[]> {
    return this.http.get<Url[]>(`http://localhost:5163/api/Url/userId/${id}`);
  }

  constructor(http: HttpClient) {
    super(http, 'http://localhost:5163/api/Url');
  }
}
