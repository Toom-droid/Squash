import { AuthService } from './../../../core/services/auth.service';
import { Url } from './../../../models/url.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UrlService } from '../../../core/services/url.service';
import { ToastrService } from 'ngx-toastr';
declare var bootstrap: any;

@Component({
  selector: 'app-url-create',
  templateUrl: './url-create.component.html',
  styleUrls: ['./url-create.component.css'],
  standalone: false,
})
export class UrlCreateComponent implements OnInit {
  createForm!: FormGroup;
  url: Url = { baseUrl: '', alias: '', description: '', userId: 0 };

  constructor(
    private fb: FormBuilder,
    private urlService: UrlService,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.createForm = this.fb.group({
      baseUrl: [
        '',
        [
          Validators.required,
          Validators.pattern('https?://.+'),
          Validators.maxLength(500),
        ],
      ],
      alias: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.maxLength(100)]],
    });

    const modalElement = document.getElementById('createModal');
    if (modalElement) {
      modalElement.addEventListener('hidden.bs.modal', () => {
        this.resetForm();
      });
    }

    this.authService.getUserData().subscribe((data) => {
      this.url.userId = data.id;
    });
  }

  onSubmit(): void {
    if (this.createForm && this.createForm.valid) {
      const formData = this.createForm.value;
      this.url.baseUrl = formData.baseUrl;
      this.url.alias = formData.alias;
      this.url.description = formData.description;
      this.urlService.urlAliasExistsAsync(this.url.alias).subscribe((d) => {
        if (d) {
          this.toastr.error(
            `Alias: ${this.url.alias} already exists. Try writing another one or generating a random one `,"", {
              toastClass: 'toast-success',
            }
          );
        } else {
          this.urlService.create(this.url).subscribe();
        }
      });
    }
  }

  resetForm(): void {
    this.createForm.reset({
      baseUrl: '',
      alias: '',
      description: '',
    });

    this.url = { baseUrl: '', alias: '', description: '', userId: 0 };
  }

  closeModal(): void {
    const modalElement = document.getElementById('createModal');
    this.resetForm();
    if (modalElement) {
      const modalInstance = bootstrap.Modal.getInstance(modalElement);
      modalInstance?.hide();
    }
  }
}
