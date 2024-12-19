import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base/base.service';
import { Url } from '../../models/url.model';

@Injectable({
  providedIn: 'root',
})
export class UrlService extends BaseService<Url> {
  constructor(http: HttpClient) {
    super(http, 'http://localhost:5067/api/url');
  }
}
