import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Url } from '../../../models/url.model';
import { UrlService } from './../../../core/services/url.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-url-dashboard',
  standalone: false,
  templateUrl: './url-dashboard.component.html',
  styleUrls: ['./url-dashboard.component.css'],
})
export class UrlDashboardComponent implements OnInit, OnDestroy {
  isLoggedIn: boolean = false;
  urls: Url[] = [];

  aliasToDelete: string = '';
  urlIdToDelete: number | null = null;

  aliasToUpdate: string | null = null;
  urlIdToUpdate: number | null = null;
  baseUrlToUpdate: string | null = null;

  private urlCreatedSubscription: Subscription | null = null;
  private urlDeletedSubscription: Subscription | null = null;
  private urlUpdatedSubscription: Subscription | null = null;

  constructor(
    private urlService: UrlService,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadUrls();

    this.urlCreatedSubscription = this.urlService.$createdSubject.subscribe(
      (created) => {
        if (created) this.loadUrls();
      }
    );

    this.urlDeletedSubscription = this.urlService.$deletedSubject.subscribe(
      (deleted) => {
        if (deleted) this.loadUrls();
      }
    );

    this.urlUpdatedSubscription = this.urlService.$updatedSubject.subscribe(
      (updated) => {
        if (updated) this.loadUrls();
      }
    );
  }

  loadUrls(): void {
    this.authService.loggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
      if (!this.isLoggedIn) {
        this.router.navigate(['/']);
      } else {
        this.authService.getUserData().subscribe((d) => {
          this.urlService.getUrlsByUserIdAsync(d.id).subscribe((d) => {
            this.urls = d.map((u) => {
              return u;
            });
          });
        });
      }
    });
  }

  ngOnDestroy(): void {
    if (this.urlCreatedSubscription) {
      this.urlCreatedSubscription.unsubscribe();
    }
    if (this.urlDeletedSubscription) {
      this.urlDeletedSubscription.unsubscribe();
    }
    if (this.urlUpdatedSubscription) {
      this.urlUpdatedSubscription.unsubscribe();
    }
  }

  openDeleteModal(alias: string, urlId: any): void {
    this.aliasToDelete = alias;
    this.urlIdToDelete = urlId;
  }

  openUpdateModal(id: any, baseUrl: string, alias: string) {
    this.urlIdToUpdate = id;
    this.baseUrlToUpdate = baseUrl;
    this.aliasToUpdate = alias;
  }

  deleteUrl(urlId: any): void {
    this.urlService.delete(urlId).subscribe((d) => {
      return d;
    });
    this.aliasToDelete = '';
    this.urlIdToDelete = null;
  }

  copyToClipboard(alias: string): void {
    navigator.clipboard
      .writeText('https://localhost:4200/' + alias)
      .then(() => {
        this.toastr.success(
          "/"+ alias,
          'Copied to clipboard',{
            toastClass: "toast-success"
          }
        );
      })
      .catch((err) => {
        console.error('Error: ', err);
      });
  }
}
