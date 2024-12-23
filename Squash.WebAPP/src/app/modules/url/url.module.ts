import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UrlDashboardComponent } from './url-dashboard/url-dashboard.component';
import { UrlRoutingModule } from './url-routing.module';
import { UrlCreateComponent } from './url-create/url-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UrlDeleteComponent } from './url-delete/url-delete.component';
import { UrlUpdateComponent } from './url-update/url-update.component';

@NgModule({
  declarations: [UrlDashboardComponent, UrlCreateComponent, UrlDeleteComponent, UrlUpdateComponent],
  imports: [CommonModule, UrlRoutingModule, ReactiveFormsModule, FormsModule],
})
export class UrlModule {}
