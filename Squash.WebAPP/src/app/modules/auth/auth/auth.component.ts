import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
  standalone: false,
})
export class AuthComponent implements OnInit {
  isLoggedIn = false;
  userName: string | null = null;
  userData: any = null;

  constructor(
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  LoginWithGoogle(): void {
    this.authService.loginWithGoogle();
  }

  LoginWithGithub(): void{
    this.authService.loginWithGithub();
  }

  ngOnInit(): void {
    this.authService.loggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
    });
    if (this.isLoggedIn) this.loadUserData();
  }

  loadUserData(): void {
    this.authService.getUserData().subscribe({
      next: (data) => {
        this.userData = data;
      },
      error: (err) => {
        console.error('Error al obtener los datos del usuario:', err);
      },
    });
  }

  logout(): void {
    this.authService.logout();
    this.cdr.detectChanges();
  }
}
