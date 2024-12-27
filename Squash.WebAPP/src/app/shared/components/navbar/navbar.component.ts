import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: false,
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  user: User;

  submenuVisible = false; 

  toggleSubmenu() {
    this.submenuVisible = !this.submenuVisible;
  }

  @HostListener('document:click', ['$event'])
  handleClickOutside(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.submenu') && !target.closest('.nav-link')) {
      this.submenuVisible = false;
    }
  }


  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.loggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
    });

    this.authService.userData$.subscribe(u => {this.user = u; console.log(this.user);})
  }
}
