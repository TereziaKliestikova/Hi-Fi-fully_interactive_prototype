import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthPageLayoutComponent } from './auth-page-layout.component';

describe('AuthPageLayoutComponent', () => {
  let component: AuthPageLayoutComponent;
  let fixture: ComponentFixture<AuthPageLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthPageLayoutComponent],
    });
    fixture = TestBed.createComponent(AuthPageLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
