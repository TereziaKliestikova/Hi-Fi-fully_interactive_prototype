import { AsyncValidatorFn, ValidatorFn } from '@angular/forms';
import FormInput from './FormInput.type';
import { Observable } from 'rxjs';

// TODO when this gets more complex types make it generic with FormInput
export type FormConfig = {
  inputs: FormInput[];
  submitText: Observable<string>;
  groupValidators?: ValidatorFn | ValidatorFn[];
  groupAsyncValidator?: AsyncValidatorFn | AsyncValidatorFn[];
};
