import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogGridComponent } from './catalog-grid.component';

describe('CatalogGridComponent', () => {
  let component: CatalogGridComponent<never>;
  let fixture: ComponentFixture<CatalogGridComponent<never>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CatalogGridComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CatalogGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
