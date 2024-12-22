import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Url } from '../../../models/url.model';
import { UrlService } from './../../../core/services/url.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-url-dashboard',
  standalone: false,
  templateUrl: './url-dashboard.component.html',
  styleUrls: ['./url-dashboard.component.css'],
})
export class UrlDashboardComponent implements OnInit {
  isLoggedIn: boolean = false;
  urls: Url[] = [];

  constructor(
    private urlService: UrlService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.loggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
      if (!this.isLoggedIn) {
        this.router.navigate(['/']);
      } else {
        this.authService.getUserData().subscribe((d) => {
          console.log(d)
          this.urlService.getByUserId(d.id).subscribe((d) => {
            console.log(d)
            this.urls = d.map((u) => {
              return u;
            });
          });
        });
      }
    });
  }
}
