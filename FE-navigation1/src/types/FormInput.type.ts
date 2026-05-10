import {
  AsyncValidatorFn,
  FormControl,
  FormControlOptions,
  ValidatorFn,
} from '@angular/forms';
import { Observable } from 'rxjs';

type FormInput = {
  type: 'text' | 'password'; // add more types as needed
  id: string;
  label: Observable<string>;
  // https://html.spec.whatwg.org/multipage/form-control-infrastructure.html#autofilling-form-controls%3A-the-autocomplete-attribute
  autocomplete?: string;
  formControl: FormControl<string | null>;
  placeholder: Observable<string>;
  validators?: ValidatorFn | ValidatorFn[] | FormControlOptions;
  asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[];
};

export default FormInput;
