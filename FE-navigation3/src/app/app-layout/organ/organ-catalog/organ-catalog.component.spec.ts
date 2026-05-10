import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganCatalogComponent } from './organ-catalog.component';

describe('OrganCatalogComponent', () => {
  let component: OrganCatalogComponent;
  let fixture: ComponentFixture<OrganCatalogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrganCatalogComponent],
    });
    fixture = TestBed.createComponent(OrganCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
