import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { AnchorComponent } from './anchor.component';

describe('AppAnchorComponent', () => {
  let component: AnchorComponent;
  let fixture: ComponentFixture<AnchorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AnchorComponent],
      imports: [RouterTestingModule],
    });
    fixture = TestBed.createComponent(AnchorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
