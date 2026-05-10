import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleImageAssistantComponent } from './sample-image-assistant.component';

describe('SampleImageHelperComponent', () => {
  let component: SampleImageAssistantComponent;
  let fixture: ComponentFixture<SampleImageAssistantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleImageAssistantComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SampleImageAssistantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
