import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextWithLinkComponent } from './text-with-link.component';

describe('TextWithLinkComponent', () => {
  let component: TextWithLinkComponent;
  let fixture: ComponentFixture<TextWithLinkComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TextWithLinkComponent],
    });
    fixture = TestBed.createComponent(TextWithLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
