import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://squash-7b6x.onrender.com/api/user'; // API base URL

  private loggedInSubject = new BehaviorSubject<boolean>(this.checkLoginStatus());
  loggedIn$ = this.loggedInSubject.asObservable();

  // Comportamiento reactivo para los datos del usuario
  private userDataSubject = new BehaviorSubject<any>(this.getStoredUserData());
  userData$ = this.userDataSubject.asObservable();

  private userData: any = null; // Variable local para almacenar los datos del usuario

  constructor(private http: HttpClient, private router: Router) {}

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
      this.loadUserData(); // Cargar datos del usuario después del login
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
      this.loggedInSubject.next(true);
      this.loadUserData(); // Cargar datos del usuario después del login
      this.router.navigate(['/dashboard']);
    } else {
      console.error('Token not Provided');
    }
  }

  // Cargar los datos del usuario solo si no están en memoria
  loadUserData(): void {
    if (this.userData) {
      this.userDataSubject.next(this.userData); // Si los datos ya están disponibles, no hacer la llamada
      return;
    }


    const token = localStorage.getItem('authToken');
    if (token) {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      this.http.get(`${this.apiUrl}/jwt`, { headers }).subscribe({
        next: (userData) => {
          this.userData = userData; // Almacenar los datos en la variable local
          localStorage.setItem('userData', JSON.stringify(userData)); // Guardamos los datos en localStorage
          this.userDataSubject.next(userData); // Actualizar el observable
        },
        error: (error) => {
          console.error('Error al cargar los datos del usuario:', error);
          this.logout(); // Si ocurre un error, cerrar sesión
        },
      });
    } else {
      console.error('No token found');
      this.logout(); // Si no hay token, cerrar sesión
    }
  }

  getUserData(): Observable<any> {
    return this.userData$; // Retorna un observable del comportamiento reactivo del usuario
  }

  // Obtener solo el ID del usuario
  getUserId(): Observable<number> {
    return this.userData$.pipe(
      map(userData => userData ? userData.id : 0)
    );
  }

  // Obtener los datos del usuario almacenados localmente
  private getStoredUserData(): any {
    const storedData = localStorage.getItem('userData');
    return storedData ? JSON.parse(storedData) : null; // Retorna los datos si están en localStorage
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userData'); // Limpiar los datos del usuario de localStorage
    this.loggedInSubject.next(false);
    this.userData = null; // Limpiar los datos del usuario
    this.userDataSubject.next(null);
    this.router.navigate(['/']);
  }
}
