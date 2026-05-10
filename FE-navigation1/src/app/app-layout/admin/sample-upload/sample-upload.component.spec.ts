import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SampleUploadComponent } from './sample-upload.component';

describe('SampleUploadComponent', () => {
  let component: SampleUploadComponent;
  let fixture: ComponentFixture<SampleUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleUploadComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SampleUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
