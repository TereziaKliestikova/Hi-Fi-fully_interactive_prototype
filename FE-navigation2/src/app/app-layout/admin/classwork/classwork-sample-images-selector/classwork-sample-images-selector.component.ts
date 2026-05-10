import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmModalConfig } from 'src/app/ui/admin/modal-actions-confirmation/modal-actions-confirmation.config';
import { FolderStateService } from 'src/app/app-layout/admin/classwork/classwork-study-detail/folder-state.service';
import {
  BulkSampleImageRequest,
  SampleImageAdminDto,
} from 'src/app/api/models';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AdminService,
  SampleImagesService,
  AssetsService,
  LearningService,
} from 'src/app/api/services';
import { translate } from '@ngneat/transloco';

import { AuthService } from 'src/app/oauth/auth.service';
import { TranslocoService } from '@ngneat/transloco';
import { SampleImageDiagnosisDto } from 'src/app/api/models';

@Component({
  selector: 'app-classwork-sample-images-selector',
  templateUrl: './classwork-sample-images-selector.component.html',
  styleUrl: './classwork-sample-images-selector.component.scss',
})
export class ClassworkSampleImageSelectorComponent implements OnInit {
  constructor(
    private assetsService: AssetsService,
    private sampleService: SampleImagesService,
    private learningService: LearningService,
    private adminServise: AdminService,
    private authService: AuthService,
    private folderState: FolderStateService,
    private router: Router,
    private route: ActivatedRoute,
    private readonly transloco: TranslocoService
  ) {}

  folderId!: number;
  dataSource: MatTableDataSource<
    SampleImageAdminDto | SampleImageDiagnosisDto
  > = new MatTableDataSource<SampleImageAdminDto | SampleImageDiagnosisDto>();
  displayedColumns: string[] = [
    'navbar',
    'name',
    'hasAnnotation',
    'caustryName',
    'keyWords',
    'organName',
    'bodySystemNames',
    'note',
    'id',
  ];

  selectedCount: number = 0;
  selectedSamplesId: number[] = [];
  modalConfirmConfig: ConfirmModalConfig | null = null;
  showModal: boolean = false;

  goBack() {
    if (this.showModal || this.selectedCount === 0)
      this.router.navigate(['..'], { relativeTo: this.route });
    else this.openModal();
  }

  openModal() {
    this.modalConfirmConfig = {
      paragraphs: [
        translate('learning.sampleSelectorComponent.modal.warningMessage'),
      ],
      highlightText: '',
      confirmText: translate('learning.sampleSelectorComponent.modal.confirm'),
      cancelText: translate('learning.sampleSelectorComponent.modal.cancel'),
    };
    this.showModal = true;
  }

  insertSelected() {
    const requestBody: BulkSampleImageRequest = {
      sampleImageIds: this.selectedSamplesId,
    };
    this.learningService
      .learningDirectoryIdSampleImagesPost({
        id: this.folderId,
        body: requestBody,
      })
      .subscribe(() => {
        this.selectedCount = 0;
        this.goBack();
      });
  }

  updateSelected(
    selectedSamples: Set<SampleImageAdminDto | SampleImageDiagnosisDto>
  ) {
    this.selectedCount = selectedSamples.size;
    this.selectedSamplesId = Array.from(selectedSamples).map(s => s.id);
    console.log(this.selectedSamplesId);
  }

  ngOnInit(): void {
    this.folderId = Number(this.route.snapshot.paramMap.get('folderId')!);
    this.folderState.setFolderId(this.folderId);
    this.fetchData();
  }

  private fetchData(): void {
    this.sampleService.adminSampleImagesGet$Json().subscribe(dataAll => {
      this.learningService
        .learningDirectoryIdSampleImagesGet$Json({
          id: this.folderId,
        })
        .subscribe(dataSelected => {
          this.dataSource.data = this.filterAlreadySelectedSampleImages(
            dataAll,
            dataSelected
          );
        });
    });
  }

  filterAlreadySelectedSampleImages(
    data: SampleImageAdminDto[],
    filter: SampleImageAdminDto[]
  ): SampleImageAdminDto[] {
    const selectedIds = new Set(filter.map(item => item.id));
    return data.filter(item => !selectedIds.has(item.id));
  }
}
