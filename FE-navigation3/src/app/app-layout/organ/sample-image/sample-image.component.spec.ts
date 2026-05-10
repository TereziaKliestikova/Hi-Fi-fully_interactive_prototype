import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleImageComponent } from './sample-image.component';

describe('SampleImageComponent', () => {
  let component: SampleImageComponent;
  let fixture: ComponentFixture<SampleImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleImageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SampleImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
