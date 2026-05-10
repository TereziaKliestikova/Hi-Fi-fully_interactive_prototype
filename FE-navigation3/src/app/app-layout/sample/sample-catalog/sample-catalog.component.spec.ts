import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleCatalogComponent } from './sample-catalog.component';

describe('SampleCatalogComponent', () => {
  let component: SampleCatalogComponent;
  let fixture: ComponentFixture<SampleCatalogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleCatalogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SampleCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
