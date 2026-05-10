import { AfterViewInit, Component, OnDestroy, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { FormConfig } from 'src/types/FormConfig.type';
import { TranslocoService } from '@ngneat/transloco';
import { Observable, share, Subscription } from 'rxjs';
import { LoginRequest } from '../../api/models/login-request';
import { AccountService } from '../../api/services/account.service';
import { EmailVerificationStatus } from 'src/types/EmailVerificationStatus';
import { FormComponent } from '../../ui/forms/form/form.component';
import { emailValidator } from '../validators/authValidators';
import { AuthService } from 'src/app/oauth/auth.service';
import { AppRoutes } from 'src/app/app-routing.module';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements AfterViewInit, OnDestroy {
  @ViewChild(FormComponent)
  formComponent!: FormComponent<LoginRequest>;

  private subs: Subscription[] = [];

  constructor(
    private transloco: TranslocoService,
    private accountService: AccountService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {}

  ngAfterViewInit(): void {
    this.route.queryParams.subscribe(params => {
      const emailVerificationStatus: EmailVerificationStatus =
        params['emailVerificationStatus'];

      if (emailVerificationStatus) {
        // this timeout must be because of undefined formComponent
        // TODO: Try to fix access to formComponent without timeout
        setTimeout(() => {
          this.handleEmailVerificationStatus(emailVerificationStatus);
        }, 100);
      }
    });
  }

  handleEmailVerificationStatus(status: EmailVerificationStatus): void {
    // this access for notificationComponent is specific for this usecase - DO NOT REPRODUCE IT
    switch (status) {
      case EmailVerificationStatus.Verified:
        this.formComponent.notificationComponent.showMessage(
          this.translate('emailVerification.verified')
        );
        break;
      case EmailVerificationStatus.AlreadyVerified:
        this.formComponent.notificationComponent.showMessage(
          this.translate('emailVerification.alreadyVerified')
        );
        break;
      case EmailVerificationStatus.InvalidToken:
        this.formComponent.notificationComponent.showMessage(
          this.translate('emailVerification.invalidToken')
        );
        break;
      default:
        this.formComponent.notificationComponent.showMessage(
          this.translate('emailVerification.unknownStatus')
        );
        break;
    }
  }

  ngOnDestroy(): void {
    this.subs.forEach(o => o.unsubscribe());
  }

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`auth.login.${key}`);

  formCfg: FormConfig = {
    inputs: [
      {
        id: 'email',
        type: 'text',
        label: this.translate('form.email.label'),
        autocomplete: 'email',
        placeholder: this.translate('form.email.placeholder'),
        formControl: new FormControl<string | null>(null, [emailValidator()]),
      },
      {
        id: 'password',
        type: 'password',
        label: this.translate('form.password.label'),
        autocomplete: 'current-password',
        placeholder: this.translate('form.password.placeholder'),
        formControl: new FormControl<string | null>(null, [
          Validators.required,
        ]),
      },
    ],
    submitText: this.translate('form.submit'),
  };

  doLogin = (data: LoginRequest): Observable<void> => {
    const obs = this.accountService
      .accountValidateUserPost({
        body: data,
      })
      .pipe(share());

    this.subs.push(
      obs.subscribe({
        complete: () => {
          this.authService
            .fetchToken(data.email!, data.password!)
            .then(() => this.router.navigate([AppRoutes.app.home]));
        },
      })
    );
    return obs;
  };
  protected readonly AppRoutes = AppRoutes;
}
