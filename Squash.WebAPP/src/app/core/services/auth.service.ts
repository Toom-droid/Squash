import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5163/api/user';

  private loggedInSubject = new BehaviorSubject<boolean>(this.checkLoginStatus());
  loggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  private checkLoginStatus(): boolean {
    return !!localStorage.getItem('authToken');
  }

  loginWithGoogle(): void {
    window.location.href = 'http://localhost:5163/api/User/auth/google';
  }

  handleGoogleCallback(): void {
    const urlParams = new URLSearchParams(window.location.search);
    const token = urlParams.get('token');
    if (token) {
      localStorage.setItem('authToken', token);
      this.loggedInSubject.next(true); 
      this.router.navigate(['/dashboard']);
    } else {
      console.error('No se recibió el token');
    }
  }

  getUserData(): Observable<any> {
    const token = localStorage.getItem('authToken');
    if (!token) {
      throw new Error('Token no encontrado');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.apiUrl}/jwt`, { headers });
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.loggedInSubject.next(false);
    this.router.navigate(['/']);
  }
}
