import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormComponent } from './form.component';
import { ButtonComponent } from '../../basic/button/button.component';

describe('FormComponent', () => {
  let component: FormComponent<never>;
  let fixture: ComponentFixture<FormComponent<never>>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormComponent, ButtonComponent],
    });
    fixture = TestBed.createComponent(FormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
