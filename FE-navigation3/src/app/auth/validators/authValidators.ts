import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/*
    Custom validator functions used in ./registration.component.ts
    based on example: https://blog.angular-university.io/angular-custom-validators/
*/

// TODO Somehow enforce same error messages for same validation functions
//  e.g containsNumber -> weakPassword

// TODO break this into smaller files

export function nameValidator(minLength = 4): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value; // value variable stores string from specific input field

    if (!value) return { required: true };

    if (!hasMinLength(value, minLength))
      return {
        minlength: {
          requiredLength: minLength,
          actualLength: value.length,
        },
      };

    if (!startsWithUpperCase(value)) return { name: true };

    return null;
  };
}

// TODO weak, this way you can have `11111111` password
export function passwordValidator(minLength = 8): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    const errors: ValidationErrors = {};
    if (!value) errors['required'] = true;
    else {
      if (!containsNumber(value)) errors['weakPassword'] = true;
      if (!containsUpperCase(value)) errors['upperCaseLetter'] = true;
      if (!containsSpecialCharacter(value)) errors['specialSym'] = true;
      if (!hasMinLength(value, minLength)) {
        errors['minlength'] = {
          requiredLength: minLength,
          actualLength: value.length,
        };
      }
    }
    if (Object.keys(errors) && Object.keys(errors).length > 0) {
      return errors;
    }
    return null;
  };
}

export function passwordMatchValidator(
  password = 'password',
  passwordConfirm = 'passwordConfirm'
): ValidatorFn {
  return (c: AbstractControl): ValidationErrors | null => {
    const passControl = c.get(password);
    const confPassControl = c.get(passwordConfirm);

    if (!passControl || !confPassControl)
      throw new Error(
        `Did not find ${password} or ${passwordConfirm} control in form`
      );

    if (passControl.value !== confPassControl.value)
      return { passwordMismatch: true };

    return null;
  };
}

export function emailValidator(): ValidatorFn {
  return (c: AbstractControl): ValidationErrors | null => {
    const value = c.value as string;

    if (!value) {
      return { required: true };
    }

    const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;

    if (!emailPattern.test(value)) {
      return { email: true };
    }

    return null;
  };
}

const startsWithUpperCase = (value: string): boolean => /^[A-Z].+/.test(value);

const containsNumber = (value: string): boolean => /.*[0-9].*/.test(value);
const containsUpperCase = (value: string): boolean => /.*[A-Z].*/.test(value);
const containsSpecialCharacter = (value: string): boolean =>
  /[^a-zA-Z0-9]/.test(value);
const hasMinLength = (value: string, minLength: number): boolean =>
  value.length >= minLength;
