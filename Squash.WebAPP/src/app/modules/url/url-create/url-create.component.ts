import { AuthService } from './../../../core/services/auth.service';
import { Url } from './../../../models/url.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UrlService } from '../../../core/services/url.service';
declare var bootstrap: any;

@Component({
  selector: 'app-url-create',
  templateUrl: './url-create.component.html',
  styleUrls: ['./url-create.component.css'],
  standalone: false,
})
export class UrlCreateComponent implements OnInit {
  createForm!: FormGroup;
  url: Url = { baseUrl: '', alias: '', description: '', flag: '', userId: 0 };

  constructor(
    private fb: FormBuilder,
    private urlService: UrlService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.createForm = this.fb.group({
      baseUrl: ['', [Validators.required, Validators.pattern('https?://.+')]],
      alias: ['', Validators.required],
      description: [''],
      flag: [''],
    });

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
      this.url.flag = formData.flag

      this.urlService.create(this.url).subscribe((data) => {
        console.log(data);
      });
    }
  }

  closeModal(): void {
    const modalElement = document.getElementById('createModal');
    if (modalElement) {
      const modalInstance = bootstrap.Modal.getInstance(modalElement);
      modalInstance?.hide();
    }
  }
}
