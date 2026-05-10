import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCaustryComponent } from './add-file-modal.component';

describe('AddCaustryComponent', () => {
  let component: AddCaustryComponent;
  let fixture: ComponentFixture<AddCaustryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddCaustryComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AddCaustryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
