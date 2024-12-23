import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base/base.service';
import { Url } from '../../models/url.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UrlService extends BaseService<Url> {
  private urlApi = "http://localhost:5163/api/Url"

  getUrlsByUserIdAsync(id: number): Observable<Url[]> {
    return this.http.get<Url[]>(`${this.urlApi}/userId/${id}`);
  }

  getUrlByAliasAsync(alias: string, userId: number): Observable<Url>{
    return this.http.get<Url>(`${this.urlApi}/${alias}/${userId}`);
  }

  updateUrlVisitCountAsync(userId:number, urlId: number,  visitCount: number): Observable<boolean>{
    return this.http.put<boolean>(`${this.urlApi}/${userId}/${urlId}/${visitCount}`, {})
  }

  constructor(http: HttpClient) {
    super(http, 'http://localhost:5163/api/Url');
  }
}
