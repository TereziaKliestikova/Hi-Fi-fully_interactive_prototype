import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogToolBarComponent } from './catalog-tool-bar.component';

describe('CatalogToolBarComponent', () => {
  let component: CatalogToolBarComponent;
  let fixture: ComponentFixture<CatalogToolBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CatalogToolBarComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CatalogToolBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
