import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganDetailComponent } from 'src/app/dashboard/organ-detail/organ-detail.component';

describe('OrganDetailComponent', () => {
  let component: OrganDetailComponent;
  let fixture: ComponentFixture<OrganDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrganDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(OrganDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
