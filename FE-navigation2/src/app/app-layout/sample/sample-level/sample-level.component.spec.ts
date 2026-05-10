import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleLevelComponent } from './sample-level.component';

describe('SampleLevelComponent', () => {
  let component: SampleLevelComponent;
  let fixture: ComponentFixture<SampleLevelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleLevelComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SampleLevelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
