import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSampleImagesComponent } from './admin-sample-images.component';

describe('AdminSampleImagesComponent', () => {
  let component: AdminSampleImagesComponent;
  let fixture: ComponentFixture<AdminSampleImagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminSampleImagesComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminSampleImagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
