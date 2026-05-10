import { Component, OnDestroy } from '@angular/core';
import { FormConfig } from '../../../types/FormConfig.type';
import { FormControl } from '@angular/forms';
import { Observable, share, Subscription } from 'rxjs';
import { TranslocoService } from '@ngneat/transloco';
import { ForgotPasswordRequest } from '../../api/models/forgot-password-request';
import { AccountService } from '../../api/services/account.service';
import { emailValidator } from '../validators/authValidators';
import { AppRoutes } from 'src/app/app-routing.module';

@Component({
  selector: 'app-request-change-password',
  templateUrl: './request-password-change.component.html',
  styleUrls: ['./request-password-change.component.scss'],
})
export class RequestPasswordChangeComponent implements OnDestroy {
  requestSuccessful = false;
  private subs: Subscription[] = [];

  constructor(
    private transloco: TranslocoService,
    private accountService: AccountService
  ) {}

  ngOnDestroy(): void {
    this.subs.forEach(o => o.unsubscribe());
  }

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`auth.requestChangePassword.${key}`);

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
    ],
    submitText: this.translate('form.submit'),
  };

  doVerificationEmail = (data: ForgotPasswordRequest): Observable<void> => {
    const obs = this.accountService
      .accountForgotPasswordPost({
        body: data,
      })
      .pipe(share());

    this.subs.push(
      obs.subscribe({
        complete: () => (this.requestSuccessful = true),
      })
    );
    return obs;
  };
  protected readonly AppRoutes = AppRoutes;
}
