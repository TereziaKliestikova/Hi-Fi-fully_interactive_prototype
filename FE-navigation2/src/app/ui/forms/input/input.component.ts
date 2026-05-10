import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import FormInput from '../../../../types/FormInput.type';
import { translate } from '@ngneat/transloco';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
})
export class InputComponent {
  @Input() input!: FormInput;
  @Input() formGroup!: FormGroup;

  // This is for password input type to show/hide password
  hide = true;

  getType(): string {
    if (!(this.input.type === 'password')) return this.input.type;
    return this.hide ? this.input.type : 'text';
  }

  errorMsg = (): string | null => {
    const errors = this.input.formControl.errors;
    if (!errors) return null;
    let finalError = '';
    Object.keys(errors).forEach(err => {
      const options = errors[err];
      finalError += translate(
        `basic.ui.forms.input.errors.${err}`,
        options instanceof Object ? options : undefined
      );
      finalError += '<br>';
    });
    return finalError;
  };
}
