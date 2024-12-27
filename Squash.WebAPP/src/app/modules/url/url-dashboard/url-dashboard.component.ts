import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Url } from '../../../models/url.model';
import { AuthService } from '../../../core/services/auth.service';
import { UrlService } from '../../../core/services/url.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-url-dashboard',
  templateUrl: './url-dashboard.component.html',
  styleUrls: ['./url-dashboard.component.css'],
  standalone: false,
})
export class UrlDashboardComponent implements OnInit, OnDestroy {
  isLoggedIn$!: Observable<boolean>;
  urls$!: Observable<Url[]>;
  loading: boolean = true;
  private destroy$ = new Subject<void>();
  aliasDeleteInput: string = '';

  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('updateModal') updateModal: any;

  modalData: {
    aliasToDelete?: string;
    urlIdToDelete?: number;
    aliasToUpdate?: string;
    urlIdToUpdate?: number;
    baseUrlToUpdate?: string;
    descToUpdate?: string;
    aliasToCreate?: string;
    baseUrlToCreate?: string;
    descToCreate?: string;
  } = {};

  aliasUpdateInput: string = '';
  baseUrlUpdateInput: string = '';

  constructor(
    private authService: AuthService,
    private urlService: UrlService,
    private router: Router,
    private toastr: ToastrService,
    private modalService: NgbModal
  ) {}

  closeModal(): void {
    this.modalService.dismissAll();
  }

  ngOnInit(): void {
    this.authService.loadUserData();

    this.isLoggedIn$ = this.authService.loggedIn$;
    this.isLoggedIn$.pipe(takeUntil(this.destroy$)).subscribe((isLoggedIn) => {
      if (!isLoggedIn) {
        this.router.navigate(['/']);
      }
    });

    this.urls$ = this.urlService.urls$;

    this.authService
      .getUserId()
      .pipe(takeUntil(this.destroy$))
      .subscribe((userId) => {
        if (userId) {
          this.urlService.loadUrlsByUserId(userId).subscribe(
            (urls) => {
              this.loading = false;
            },
            (error) => {
              console.error('Error loading URLs:', error);
              this.loading = false;
            }
          );
        } else {
          console.error('User ID is null or undefined');
        }
      });

    this.urlService.$createdSubject
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.refreshUrls();
      });

    this.urlService.$deletedSubject
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.refreshUrls();
      });

    this.urlService.$updatedSubject
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.refreshUrls();
      });
  }

  refreshUrls(): void {
    this.authService
      .getUserId()
      .pipe(takeUntil(this.destroy$))
      .subscribe((userId) => {
        if (userId) {
          this.urlService.loadUrlsByUserId(userId).subscribe(
            (urls) => {
              this.loading = false;
            },
            (error) => {
              console.error('Error al cargar las URLs:', error);
              this.loading = false;
            }
          );
        }
      });
  }

  openDeleteModal(content: any, alias: string, urlId: any): void {
    this.aliasDeleteInput = '';
    this.modalData = { aliasToDelete: alias, urlIdToDelete: urlId };
    this.modalService.open(content);
  }

  openUpdateModal(
    content: any,
    id: any,
    baseUrl: string,
    alias: string,
    description: any
  ): void {
    this.modalData = {
      urlIdToUpdate: id,
      baseUrlToUpdate: baseUrl,
      aliasToUpdate: alias,
      descToUpdate: description,
    };
    this.aliasUpdateInput = this.modalData.aliasToUpdate;
    this.baseUrlUpdateInput = this.modalData.baseUrlToUpdate;
    this.modalService.open(content);
  }

  openCreateModal(content: any): void {
    this.modalService.open(content);
  }

  deleteUrl(urlId: any): void {
    if (
      urlId !== undefined &&
      this.modalData.aliasToDelete == this.aliasDeleteInput
    ) {
      this.urlService.deleteUrl(urlId).subscribe(() => {
        this.toastr.success('URL deleted succesfully');
        this.closeModal();
        this.modalData = {};
      });
    }
  }

  updateUrl(id: any, alias: string, baseUrl: string, desc: string): void {
    this.authService
      .getUserId()
      .pipe(takeUntil(this.destroy$))
      .subscribe((userId) => {
        const updatedUrl = {
          id: id,
          alias: alias,
          baseUrl: baseUrl,
          description: desc,
          userId: userId,
        };

        this.urlService.updateUrl(updatedUrl).subscribe({
          next: () => {
            this.toastr.success('URL updated succesfully');
            this.closeModal();
            this.modalData = {};
          },
          error: (err) => {
            this.toastr.error('Error updating URL');
            console.error(err);
          },
        });
      });
  }

  createUrl(alias: string, baseUrl: string, desc: string) {
    this.authService
      .getUserId()
      .pipe(takeUntil(this.destroy$))
      .subscribe((userId) => {
        const createdUrl = {
          alias: alias,
          baseUrl: baseUrl,
          description: desc ? desc : '',
          userId: userId,
        };

        console.log(createdUrl);

        this.urlService.createUrl(createdUrl).subscribe({
          next: () => {
            this.toastr.success('URL created succesfully');
            this.closeModal();
            this.modalData = {};
          },
          error: (err) => {
            this.toastr.error('Error creating URL');
            console.error(err);
          },
        });
      });
  }

  trackById(index: number, url: any): number {
    return url.id;
  }

  copyToClipboard(alias: string): void {
    navigator.clipboard
      .writeText(`https://squash-xi.vercel.app/${alias}`)
      .then(() => this.toastr.success(`/${alias}`, 'Copied to clipoard'))
      .catch((err) => console.error('Error coping to clipboard:', err));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
