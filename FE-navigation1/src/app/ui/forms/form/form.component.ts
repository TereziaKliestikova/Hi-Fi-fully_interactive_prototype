import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { FormConfig } from 'src/types/FormConfig.type';
import { Observable } from 'rxjs';
import { FormNotificationComponent } from '../form-notification/form-notification.component';
import { translate } from '@ngneat/transloco';
import { ResponseError } from 'src/types/ResponseError.type';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent<T> implements OnInit {
  @Input({ required: true }) config!: FormConfig;
  @Input({ required: true }) doSubmit!: (data: T) => Observable<void>;
  @Input({ required: false }) isNested: boolean = false;

  @ViewChild(FormNotificationComponent)
  notificationComponent!: FormNotificationComponent;

  state: 'loading' | 'error' | 'success' | null = null;
  formGroup: FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.config.inputs.forEach(i => {
      this.formGroup.addControl(i.id, i.formControl);
    });
    this.config.groupValidators &&
      this.formGroup.addValidators(this.config.groupValidators);
    this.config.groupAsyncValidator &&
      this.formGroup.addAsyncValidators(this.config.groupAsyncValidator);
  }

  // TODO move this to a service when will use this in more places
  private translateError = (error: ResponseError): string => {
    if (error.status === 400 && error.title) {
      return translate(error.title);
    } else if (error.status === 400 && error.status < 500) {
      return translate('api.error.unknownError');
    } else if (error.status >= 500) {
      return translate('api.error.serverError');
    } else if (error.status === 401) {
      return translate('api.error.login.invalidUserCredentials');
    }
    // Maybe separate translation would be better,
    // but we do not wand users to know about this
    console.error('Unknown error', error);
    return translate('api.error.unknownError');
  };

  onSubmit(): void {
    this.state = 'loading';
    this.doSubmit(this.formGroup.value).subscribe({
      complete: () => {
        this.state = 'success';
        this.formGroup.reset();
      },
      error: e => {
        this.state = 'error';
        const error = JSON.parse(e.error) as ResponseError;
        this.notificationComponent.showError(this.translateError(error));
      },
    });
  }

  errorMsg = (): string | null => {
    const errors = this.formGroup.errors;
    if (!errors) return null;

    const key = Object.keys(errors)[0];
    const options = errors[key];
    return translate(
      `basic.ui.forms.input.errors.${Object.keys(errors)[0]}`,
      options instanceof Object ? options : undefined
    );
  };

  getButtonState = (): 'loading' | 'disabled' | 'enabled' => {
    // for now, we only handle loading state
    if (this.state === 'loading') return 'loading';
    return this.formGroup.valid ? 'enabled' : 'disabled';
  };
}
