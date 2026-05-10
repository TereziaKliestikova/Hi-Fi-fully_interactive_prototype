import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassworkStudyCategoriesComponent } from './classwork-study-categories.component';

describe('ClassworkStudyCategoriesComponent', () => {
  let component: ClassworkStudyCategoriesComponent;
  let fixture: ComponentFixture<ClassworkStudyCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassworkStudyCategoriesComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ClassworkStudyCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
