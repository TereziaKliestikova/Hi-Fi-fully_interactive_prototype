import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassworkSampleImageSelectorComponent } from './classwork-sample-images-selector.component';

describe('ClassworkStudyCategoriesComponent', () => {
  let component: ClassworkSampleImageSelectorComponent;
  let fixture: ComponentFixture<ClassworkSampleImageSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassworkSampleImageSelectorComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ClassworkSampleImageSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
