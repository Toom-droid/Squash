import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Url } from '../../../models/url.model';
import { UrlService } from '../../../core/services/url.service';
import { AuthService } from '../../../core/services/auth.service';
declare var bootstrap: any;

@Component({
  selector: 'app-url-update',
  templateUrl: './url-update.component.html',
  styleUrls: ['./url-update.component.css'],
  standalone: false,
})
export class UrlUpdateComponent implements OnInit {
  updateForm!: FormGroup;
  @Input() id: any | null = null;
  @Input() baseUrl: any | null = null;
  @Input() alias: any | null = null;
  @Input() description: any | null = null;
  url: Url = {
    baseUrl: '',
    alias: '',
    description: '',
    id: 0,
    userId: 0,
  };

  constructor(
    private urlService: UrlService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.updateForm = this.fb.group({
      baseUrl: ['', [Validators.required, Validators.pattern('https?://.+')]],
      alias: ['', Validators.required],
      description: [''],
    });

    this.url.alias = this.alias;
    this.url.baseUrl = this.baseUrl;
    this.url.id = this.id;
    this.url.description = this.description;

    this.authService.getUserData().subscribe((d) => {
      this.url.userId = d.id;
    });
  }

  onSubmit(): void {
    if (this.updateForm && this.updateForm.valid) {
      const formData = this.updateForm.value;
      this.url.baseUrl = formData.baseUrl;
      this.url.alias = formData.alias;
      this.url.description = formData.description;

      this.urlService.update(this.url).subscribe();
    }
  }

  closeModal(): void {
    const modalElement = document.getElementById('updateModal');
    if (modalElement) {
      const modalInstance = bootstrap.Modal.getInstance(modalElement);
      modalInstance?.hide();
    }
  }
}
