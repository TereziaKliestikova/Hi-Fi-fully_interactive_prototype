import { Component, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';

import {
  emailValidator,
  nameValidator,
  passwordMatchValidator,
  passwordValidator,
} from '../validators/authValidators';

import { FormConfig } from '../../../types/FormConfig.type';
import { Observable, share, Subscription } from 'rxjs';
import { AccountService } from '../../api/services/account.service';
import { TranslocoService } from '@ngneat/transloco';
import { RegistrationRequest } from '../../api/models/registration-request';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnDestroy {
  registrationSuccess = false;
  private subs: Subscription[] = [];

  constructor(
    private transloco: TranslocoService,
    private accountService: AccountService
  ) {}

  ngOnDestroy(): void {
    this.subs.forEach(o => o.unsubscribe());
  }

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`auth.registration.${key}`);

  formCfg: FormConfig = {
    inputs: [
      {
        id: 'firstName',
        type: 'text',
        label: this.translate('form.firstName.label'),
        autocomplete: 'given-name',
        placeholder: this.translate('form.firstName.placeholder'),
        formControl: new FormControl<string | null>(null, [nameValidator()]),
      },
      {
        id: 'lastName',
        type: 'text',
        label: this.translate('form.lastName.label'),
        autocomplete: 'family-name',
        placeholder: this.translate('form.lastName.placeholder'),
        formControl: new FormControl<string | null>(null, [nameValidator()]),
      },
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
        formControl: new FormControl<string | null>(null, []),
      },
    ],
    groupValidators: passwordMatchValidator(),
    submitText: this.translate('form.submit'),
  };

  doRegister = (data: RegistrationRequest): Observable<void> => {
    const obs = this.accountService
      .accountRegisterPost({
        body: data,
      })
      .pipe(share());

    this.subs.push(
      // We need it because we want to know when the request is complete to user
      obs.subscribe({
        complete: () => (this.registrationSuccess = true),
      })
    );
    return obs;
  };
}
