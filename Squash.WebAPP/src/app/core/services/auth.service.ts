import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://squash-7b6x.onrender.com/api/user';

  private loggedInSubject = new BehaviorSubject<boolean>(this.checkLoginStatus());
  loggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  private checkLoginStatus(): boolean {
    return !!localStorage.getItem('authToken');
  }

  // Google Auth
  loginWithGoogle(): void {
    window.location.href = `${this.apiUrl}/auth/google`;
  }

  handleGoogleCallback(): void {
    const urlParams = new URLSearchParams(window.location.search);
    const token = urlParams.get('token');
    if (token) {
      localStorage.setItem('authToken', token);
      this.loggedInSubject.next(true); 
      this.router.navigate(['/dashboard']);
    } else {
      console.error('Token not Provided');
    }
  }

  // Github Auth
  loginWithGithub(): void {
    window.location.href = `${this.apiUrl}/auth/github`;
  }

  handleGithubCallback(): void {
    const urlParams = new URLSearchParams(window.location.search);
    const token = urlParams.get('token');
    if (token) {
      localStorage.setItem('authToken', token);
      this.router.navigate(['/dashboard']);
    } else {
      console.error('Token not Provided');
    }
  }


  getUserData(): Observable<any> {
    const token = localStorage.getItem('authToken');
    if (!token) {
      throw new Error('Token doesnt Founded');
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
