/* eslint-disable @typescript-eslint/no-explicit-any */

/*
NOT REVIEWED !!! READ CAREFULLY BEFORE PROCEEDING !!!
If you want to use this component, you should review it first. Some changes may be needed.

  DESCRIPTION:
  - User should be able to upload whole sample image with annotation and assign it organ and body system
  \_ If organ/ system is not in the list, user can add new ones
  - In the current state the compoenent is able to upload .vsi file with annotation and organ/ system
  \_ Creation of new organ/ system is also implemented
  \_ The upload has been tested only with one .vsi file we did not test uploading all the levels of .vsi file
  - The created sample image can be selected in the catalog but upon opening the sample image the app
  won't be able to load the image

  HOW TO UPLOAD:
  1. Load .vsi file
  2. The name automatically transfers to the input field for the sample image name
  3. Load annotation file
  4. Choose organ and body system from the dropdowns or create new ones by typing in the input fields
  4.1. When creating new organ/ system leave the dropdowns empty
  5. Submit the form

  TODO: The component should utilize generic form component from  src\app\ui\forms
  TODO: All inputs should be validated, FormControls should have their own validators
  TODO: Code refactor, obtaining the file in upload() method is hacky
  TODO: User should be notified whether the upload was successful or not
 */

import { Component, OnInit, signal, OnDestroy } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';
import { BehaviorSubject } from 'rxjs';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AssetsService, AdminService, SampleImagesService } from 'src/app/api/services';
import { UploadService } from 'src/app/services/sample-image_upload/sample-image-upload';
import { OrganDiagnosesDto } from 'src/app/api/models';
import { RbacRoles } from 'src/types/RbacRoles.enum';
import { AdminUploadSampleImageDataPost$Plain$Params } from 'src/app/api/fn/admin/admin-upload-sample-image-data-post-plain';
import { Router, NavigationStart } from '@angular/router';
import { filter, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-sample-upload',
  templateUrl: './sample-upload.component.html',
  styleUrls: ['./sample-upload.component.scss'],
})
export class SampleUploadComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  constructor(
    private assetService: AssetsService,
    private adminService: AdminService,
    private sampleImageService: SampleImagesService,
    private uploadService: UploadService,
    private router: Router,
    private modalService: ModalService
  ) {
    document.body.style.overflow = 'hidden';

    this.router.events
      .pipe(
        filter((event): event is NavigationStart => event instanceof NavigationStart),
        filter((event: NavigationStart) => {
          const currentPath = this.router.url;
          const targetPath = event.url;

          return !targetPath.includes('/admin/upload') && currentPath.includes('/admin/upload');
        }),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.uploadService.clearTasks();
      });
  }

  organs: OrganDiagnosesDto[] | null = null;
  annotationFile: File | null = null;
  sampleImage: File | null = null;
  organId: string | null = null;
  currentKeyWord = new BehaviorSubject<string>('');
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  readonly keyWords = signal<string[]>([]);
  allKeyWords: string[] = [];
  filteredkeyWords: string[] = this.allKeyWords;
  adminForm: FormGroup = new FormGroup({
    SampleImage: new FormControl(null, Validators.required),
    KeyWords: new FormControl('', []),
    SampleImageFileName: new FormControl('', Validators.required),
    AnnotationFile: new FormControl(null),
    OrganId: new FormControl(null, Validators.required), // -1 is the default value due to BE requirements
  });

  ngOnInit(): void {
    // this.fetchSystems();
    this.fetchOrgans();
    this.currentKeyWord.subscribe(value => {
      this.filteredkeyWords = value
        ? this.allKeyWords.filter(fruit => fruit.toLowerCase().includes(value.toLowerCase()))
        : [...this.allKeyWords];
    });
  }

  ngOnDestroy(): void {
    document.body.style.overflow = 'auto';
    this.uploadService.clearTasks();

    this.destroy$.next();
    this.destroy$.complete();
  }

  fetchOrgans = () => {
    this.assetService.assetsOrganTileListGet$Json().subscribe(data => {
      this.organs = data.organs;
      console.log(this.organs);
    });
  };

  fetchOrganKeyWords = (id: number) => {
    this.sampleImageService.adminSampleImagesKeywordsIdGet$Json({ id }).subscribe(data => {
      this.filteredkeyWords = [...data];
      this.allKeyWords = data;
      console.log(data);
    });
  };

  onAnnotFileSelected(event: any): void {
    const file = (event.target as HTMLInputElement).files?.[0] || null;
    this.annotationFile = file;
  }

  onSampleImageSelected(event: any): void {
    const file = (event.target as HTMLInputElement).files?.[0] || null;
    this.sampleImage = file;
    this.adminForm.controls['SampleImageFileName'].setValue(file?.name || '');
  }

  onSubmit(): void {
    if (this.sampleImage && this.organId) {
      console.log('Starting upload...');

      // const formData = new FormData();
      // formData.append('KeyWords', this.keyWords().join(','));
      // formData.append('OrganId', this.organId);
      // formData.append('SampleImageFileName', this.sampleImage.name);
      // formData.append('AnnotationFile', this.annotationFile, this.annotationFile.name);

      const formData: AdminUploadSampleImageDataPost$Plain$Params = {
        body: {
          AnnotationFile: this.annotationFile || undefined,
          SampleImageFileName: this.sampleImage.name.split('.').slice(0, -1).join('.'),
          OrganId: Number(this.adminForm.value.OrganId),
          KeyWords: this.keyWords().join(','),
        },
      };

      console.log(formData);
      this.uploadService.uploadSample(formData, this.sampleImage);
      console.log('Files are being uploaded');
      this.closeModal();
    } else {
      console.log('Upload not started');
      const sampleImageValidation = this.adminForm.get('SampleImage');
      const annotationFileValidation = this.adminForm.get('AnnotationFile');
      const organSelection = this.adminForm.get('OrganId');
      sampleImageValidation?.markAsTouched();
      annotationFileValidation?.markAsTouched();
      sampleImageValidation?.updateValueAndValidity();
      annotationFileValidation?.updateValueAndValidity();
      organSelection?.updateValueAndValidity();
    }
  }

  onOrganChange(event: any): void {
    const organId = event.value;
    const organSelection = this.adminForm.get('OrganId');
    organSelection?.setValue(organId);
    this.organId = organId;
    if (organId) {
      this.fetchOrganKeyWords(organId);
    } else {
      this.allKeyWords = [];
      this.filteredkeyWords = [];
    }
    this.currentKeyWord.next('');
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      const currentkeyWords = this.keyWords();
      if (!currentkeyWords.includes(value)) {
        this.keyWords.set([...currentkeyWords, value]);
      }
    }
    event.chipInput!.clear();
    this.currentKeyWord.next('');
  }

  remove(fruit: string): void {
    const currentkeyWords = this.keyWords();
    const index = currentkeyWords.indexOf(fruit);
    if (index >= 0) {
      currentkeyWords.splice(index, 1);
      this.keyWords.set([...currentkeyWords]);
    }
  }

  selected(event: MatAutocompleteSelectedEvent, input: HTMLInputElement): void {
    const currentkeyWords = this.keyWords();
    const value = event.option.viewValue;
    if (!currentkeyWords.includes(value)) {
      this.keyWords.set([...currentkeyWords, value]);
    }
    input.value = '';
    this.currentKeyWord.next('');
  }

  onInputChange(value: string): void {
    this.currentKeyWord.next(value);
  }
  protected readonly RbacRoles = RbacRoles;

  closeModal() {
    this.modalService.close();
  }
}
