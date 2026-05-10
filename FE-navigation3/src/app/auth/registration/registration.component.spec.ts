import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationComponent } from './registration.component';
import { TextWithLinkComponent } from '../../ui/basic/text-with-link/text-with-link.component';
import { FormComponent } from '../../ui/forms/form/form.component';
import { ButtonComponent } from '../../ui/basic/button/button.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('RegistrationComponent', () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        RegistrationComponent,
        TextWithLinkComponent,
        FormComponent,
        ButtonComponent,
      ],
      imports: [RouterTestingModule],
    });
    fixture = TestBed.createComponent(RegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
