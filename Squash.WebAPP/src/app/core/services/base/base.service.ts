import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export class BaseService<T> {
  constructor(protected http: HttpClient, private apiUrl: string) { }

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.apiUrl);
  }

  getById(id: number): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${id}`);
  }

  create(item: T): Observable<T> {
    return this.http.post<T>(this.apiUrl, item);
  }

  update(item: T): Observable<T> {
    return this.http.put<T>(this.apiUrl, item);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
