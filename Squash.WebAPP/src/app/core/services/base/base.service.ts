import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';

export class BaseService<T> {
  private createdSubject = new BehaviorSubject<boolean>(false);
  $createdSubject = this.createdSubject.asObservable();

  private deletedSubject = new BehaviorSubject<boolean>(false);
  $deletedSubject = this.deletedSubject.asObservable();
  
  private updatedSubject = new BehaviorSubject<boolean>(false);
  $updatedSubject = this.updatedSubject.asObservable();

  constructor(protected http: HttpClient, private apiUrl: string) {}

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.apiUrl);
  }

  getById(id: number): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${id}`);
  }

  create(item: T): Observable<T> {
    return this.http.post<T>(this.apiUrl, item).pipe(
      tap(() => {
        this.createdSubject.next(true);
      })
    );
  }

  update(item: T): Observable<T> {
    return this.http.put<T>(this.apiUrl, item).pipe(
      tap(()=>{
        this.updatedSubject.next(true)
      })
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        this.deletedSubject.next(true);
      })
    );
  }
}
