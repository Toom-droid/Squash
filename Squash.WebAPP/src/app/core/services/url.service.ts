import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Url } from '../../models/url.model';

@Injectable({
  providedIn: 'root',
})
export class UrlService {
  private urlApi = 'https://squash-7b6x.onrender.com/api/Url';

  // BehaviorSubjects para el estado local
  private urlsSubject = new BehaviorSubject<Url[]>([]);
  urls$ = this.urlsSubject.asObservable();

  private createdSubject = new BehaviorSubject<boolean>(false);
  $createdSubject = this.createdSubject.asObservable();

  private deletedSubject = new BehaviorSubject<boolean>(false);
  $deletedSubject = this.deletedSubject.asObservable();

  private updatedSubject = new BehaviorSubject<boolean>(false);
  $updatedSubject = this.updatedSubject.asObservable();

  constructor(private http: HttpClient) {}

  // Cargar URLs por usuario
  loadUrlsByUserId(userId: number): Observable<Url[]> {
    return this.http.get<Url[]>(`${this.urlApi}/userId/${userId}`).pipe(
      tap((urls) => {this.urlsSubject.next(urls);})
    );
  }

  // Obtener una URL por alias
  getUrlByAlias(alias: string): Observable<Url> {
    return this.http.get<Url>(`${this.urlApi}/alias/${alias}`);
  }

  // Validar existencia de alias por ID
  urlAliasExistsById(alias: string, urlId: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.urlApi}/exist/${alias}/${urlId}`);
  }

  // Validar existencia de alias
  urlAliasExists(alias: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.urlApi}/exist/${alias}`);
  }

  // Actualizar contador de visitas
  updateUrlVisitCount(urlId: number, visitCount: number): Observable<boolean> {
    return this.http.put<boolean>(`${this.urlApi}/visit/${urlId}/${visitCount}`, {});
  }

  // Crear una nueva URL
  createUrl(newUrl: Url): Observable<Url> {
    return this.http.post<Url>(this.urlApi, newUrl).pipe(
      tap((url) => {
        const currentUrls = this.urlsSubject.value;
        this.urlsSubject.next([...currentUrls, url]);
        this.createdSubject.next(true);
      })
    );
  }

  // Actualizar una URL
  updateUrl(updatedUrl: Url): Observable<Url> {
    return this.http.put<Url>(`${this.urlApi}`, updatedUrl).pipe(
      tap(() => {
        const updatedUrls = this.urlsSubject.value.map((url) =>
          url.id === updatedUrl.id ? updatedUrl : url
        );
        this.urlsSubject.next(updatedUrls);
        this.updatedSubject.next(true);
      })
    );
  }
  

  // Eliminar una URL
  deleteUrl(urlId: number): Observable<void> {
    return this.http.delete<void>(`${this.urlApi}/${urlId}`).pipe(
      tap(() => {
        const remainingUrls = this.urlsSubject.value.filter((url) => url.id !== urlId);
        this.urlsSubject.next(remainingUrls);
        this.deletedSubject.next(true);
      })
    );
  }
}
