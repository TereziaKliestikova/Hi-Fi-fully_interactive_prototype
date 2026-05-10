/// <reference types="jasmine" />
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ToggleButtonComponent } from './toggle.component';

describe('ToggleButtonComponent', () => {
  let component: ToggleButtonComponent;
  let fixture: ComponentFixture<ToggleButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ToggleButtonComponent],
    });

    fixture = TestBed.createComponent(ToggleButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should be unchecked by default', () => {
    expect(component.checked).toBeFalse();
    expect(component.label).toBe('Skryť relevantné');
  });

  it('should toggle checked state on click', () => {
    component.onToggle();

    expect(component.checked).toBeTrue();
    expect(component.label).toBe('Zobraziť relevantné');
  });
});
