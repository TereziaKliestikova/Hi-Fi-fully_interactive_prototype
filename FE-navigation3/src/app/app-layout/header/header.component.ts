import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { TranslocoService } from '@ngneat/transloco';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { AuthService } from 'src/app/oauth/auth.service';
import { AccountService } from 'src/app/api/services/account.service';

import { NavConfig } from 'src/types/NavConfig.type';
import { RefreshAccessTokenRequest } from 'src/app/api/models';
import { AppRoutes } from 'src/app/app-routing.module';
import { RbacRoles } from 'src/types/RbacRoles.enum';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  currentLanguage: string;

  constructor(
    private transloco: TranslocoService,
    private authService: AuthService,
    private accountService: AccountService,
    private router: Router
  ) {
    this.currentLanguage = this.transloco.getActiveLang();
    this.transloco.langChanges$.subscribe(newLang => {
      this.currentLanguage = newLang;
    });
  }

  isDropdownOpen = false;
  mobileDropdownOpen = false;

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`basic.ui.header.${key}`);

  userNav: NavConfig[] = [
    {
      text: this.translate('nav.sampleImage'),
      to: AppRoutes.app.sampleImages,
      roles: [RbacRoles.Admin],
    },
    // {
    //   text: this.translate('nav.organ'),
    //   to: AppRoutes.app.organCatalog,
    //   roles: [RbacRoles.Student],
    // },
    // {
    //   text: this.translate('nav.system'),
    //   to: AppRoutes.app.systemCatalog,
    //   roles: [RbacRoles.Student],
    // },
    {
      text: this.translate('nav.sampleImage'),
      to: AppRoutes.app.sampleImagesCatalog,
      roles: [RbacRoles.Student],
    },
    {
      text: this.translate('nav.classwork'),
      to: AppRoutes.app.adminClassworkStudyCategories,
      roles: [RbacRoles.Admin],
    },
    {
      text: this.translate('nav.classwork'),
      to: AppRoutes.app.classworkStudyCategories,
      roles: [RbacRoles.Student],
    },
    {
      text: this.translate('nav.tasks'),
      to: '/PLACEHOLDER-ROUTE',
      roles: [RbacRoles.Student],
    }, //hidden tab since the logic is not yet implemented
  ];

  changeLanguage(newLang: string): void {
    switch (newLang) {
      case 'en':
        this.transloco.setActiveLang('en');
        localStorage.setItem('lang', 'en');
        break;
      case 'sk':
        this.transloco.setActiveLang('sk');
        localStorage.setItem('lang', 'sk');
        break;
      default:
        console.error('Unknown language');
    }
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  toggleMobileDropdown(value?: boolean) {
    this.mobileDropdownOpen = value ?? !this.mobileDropdownOpen;
  }

  logout(): void {
    const data: RefreshAccessTokenRequest = {
      refreshToken: this.authService.getRefreshToken(),
    };

    const obs = this.accountService.accountLogoutPost({
      body: data,
    });

    obs
      .pipe(
        finalize(() => {
          // since we flush the local storage and navigate to login regardless of the response
          this.authService.logout();
          this.router.navigate([AppRoutes.auth.login]);
        })
      )
      .subscribe({
        error: err => {
          console.error(err);
        },
      });
  }

  protected readonly AppRoutes = AppRoutes;
  protected readonly RbacRoles = RbacRoles;
}
