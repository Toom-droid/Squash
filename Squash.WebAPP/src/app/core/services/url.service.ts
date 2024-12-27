import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base/base.service';
import { Url } from '../../models/url.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UrlService extends BaseService<Url> {
  private urlApi = "https://squash-7b6x.onrender.com/api/Url"
  private cachedUrls: Url[] = [];

  getUrlsByUserIdAsync(id: number): Observable<Url[]> {
    return this.http.get<Url[]>(`${this.urlApi}/userId/${id}`);
  }

  getUrlByAliasAsync(alias: string, userId: number): Observable<Url>{
    return this.http.get<Url>(`${this.urlApi}/${alias}/${userId}`);
  }

  urlAliasExistsByIdAsync(alias: string, urlId: number): Observable<boolean>{
    return this.http.get<boolean>(`${this.urlApi}/exist/${alias}/${urlId}`)
  }
  urlAliasExistsAsync(alias: string): Observable<boolean>{
    return this.http.get<boolean>(`${this.urlApi}/exist/${alias}`)
  }

  updateUrlVisitCountAsync(userId:number, urlId: number,  visitCount: number): Observable<boolean>{
    return this.http.put<boolean>(`${this.urlApi}/${userId}/${urlId}/${visitCount}`, {})
  }

  constructor(http: HttpClient) {
    super(http, 'https://squash-7b6x.onrender.com/api/Url');
  }
}
