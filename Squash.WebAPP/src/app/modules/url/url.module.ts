import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UrlDashboardComponent } from './url-dashboard/url-dashboard.component';
import { UrlRoutingModule } from './url-routing.module';

@NgModule({
  declarations: [UrlDashboardComponent],
  imports: [CommonModule, UrlRoutingModule],
})
export class UrlModule {}
