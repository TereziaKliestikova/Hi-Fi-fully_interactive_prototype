import { Component, OnDestroy } from '@angular/core';
import { FormConfig } from '../../../types/FormConfig.type';
import { FormControl } from '@angular/forms';
import {
  passwordMatchValidator,
  passwordValidator,
} from '../validators/authValidators';
import { Observable, share, Subscription } from 'rxjs';
import { TranslocoService } from '@ngneat/transloco';
import { ResetPasswordRequest } from '../../api/models/reset-password-request';
import { AccountService } from '../../api/services/account.service';
import { ActivatedRoute } from '@angular/router';
import { AppRoutes } from 'src/app/app-routing.module';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnDestroy {
  requestSuccessful = false;
  private subs: Subscription[] = [];

  constructor(
    private transloco: TranslocoService,
    private accountService: AccountService,
    private route: ActivatedRoute
  ) {}

  ngOnDestroy(): void {
    this.subs.forEach(o => o.unsubscribe());
  }

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`auth.changePassword.${key}`);

  formCfg: FormConfig = {
    inputs: [
      {
        id: 'password',
        type: 'password',
        label: this.translate('form.password.label'),
        autocomplete: 'new-password',
        placeholder: this.translate('form.password.placeholder'),
        formControl: new FormControl<string | null>(null, [
          passwordValidator(),
        ]),
      },
      {
        id: 'passwordConfirm',
        type: 'password',
        label: this.translate('form.passwordConfirm.label'),
        autocomplete: 'new-password',
        placeholder: this.translate('form.passwordConfirm.placeholder'),
        formControl: new FormControl<string | null>(null, [
          passwordValidator(),
        ]),
      },
    ],
    groupValidators: passwordMatchValidator(),
    submitText: this.translate('form.submit'),
  };

  doChangePassword = (data: ResetPasswordRequest): Observable<void> => {
    // get from url parameters token and email
    const token = this.route.snapshot.queryParams['token'] as string;
    const email = this.route.snapshot.queryParams['email'] as string;

    data.token = token;
    data.email = email;

    const obs = this.accountService
      .accountResetPasswordPost({
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
