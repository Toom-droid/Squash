import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { UrlService } from '../../../core/services/url.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-redirect',
  template: '',
  styleUrls: ['./redirect.component.css'],
  standalone: false,
})
export class RedirectComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private urlService: UrlService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    try {
      const alias = this.route.snapshot.paramMap.get('alias');

      if (!alias) {
        throw new Error('Alias not provided');
      }

      const userData = await firstValueFrom(this.authService.getUserData());
      const userId = userData.id;
      
      if (!userId) {
        throw new Error('User not authenticated');
      }
      
      const url = await firstValueFrom(
        this.urlService.getUrlByAliasAsync(alias, userId)
      );
      let visitCount = url.visitCount ? url.visitCount + 1 : null;


      if (visitCount && url.id) {
        this.urlService.updateUrlVisitCountAsync(userId, url.id, visitCount).subscribe();
      }

      if (!url) throw new Error('Alias not found');
      window.location.href = url.baseUrl;

    } catch (error) {
      this.router.navigate(['/not-found']);
      console.error('Redirection error:', error);
    }
  }
}
