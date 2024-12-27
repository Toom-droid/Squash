import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UrlDashboardComponent } from './url-dashboard/url-dashboard.component';
import { UrlRoutingModule } from './url-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [UrlDashboardComponent],
  imports: [CommonModule, UrlRoutingModule, ReactiveFormsModule, FormsModule],
})
export class UrlModule {}
