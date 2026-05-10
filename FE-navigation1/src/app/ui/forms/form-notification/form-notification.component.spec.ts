import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormNotificationComponent } from './form-notification.component';

describe('NotificationComponent', () => {
  let component: FormNotificationComponent;
  let fixture: ComponentFixture<FormNotificationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormNotificationComponent],
    });
    fixture = TestBed.createComponent(FormNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
