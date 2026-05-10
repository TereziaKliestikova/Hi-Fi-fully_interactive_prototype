import { Component, OnInit, SecurityContext, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { SampleImagesService } from '../../../../api/services/sample-images.service';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { LearningService } from '../../../../api/services/learning.service';
import { ClassworkService } from 'src/app/api/services';
import { DirectoryTreeDto } from '../../../../api/models/directory-tree-dto';
import {
  BulkSampleImageRequest,
  DirectoryDetailDto,
  PdfFileDto,
  SampleImageAdminDto,
  SampleImageDiagnosisDto,
} from 'src/app/api/models';
import { MatDialog } from '@angular/material/dialog';
import { AddFileModalComponent } from 'src/app/ui/admin/add-file-modal/add-file-modal.component';
import { UploadService } from 'src/app/services/sample-image_upload/sample-image-upload';
import { translate } from '@ngneat/transloco';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmModalConfig } from 'src/app/ui/admin/modal-actions-confirmation/modal-actions-confirmation.config';
import { RbacRoles } from 'src/types/RbacRoles.enum';
import { AuthService } from 'src/app/oauth/auth.service';
import { FolderStateService } from './folder-state.service';
import { environment } from '../../../../../environments/environment';
import { DomSanitizer } from '@angular/platform-browser';
interface IModal {
  name: string;
  open: boolean;
  modalConfig: ConfirmModalConfig;
  createAction?: (args: unknown) => void;
  confirmAction?: () => void;
}

interface NotificationItem {
  message: string;
}
@Component({
  selector: 'app-classwork-study-detail',
  templateUrl: './classwork-study-detail.component.html',
  styleUrl: './classwork-study-detail.component.scss',
})
export class ClassworkStudyDetailComponent implements OnInit {
  studyName: string | null = null;

  readonly keyWords = signal<string[]>([]);
  currentKeyWord = new BehaviorSubject<string>('');
  allKeyWords: string[] = [];
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  filteredkeyWords: string[] = this.allKeyWords;

  description?: string;

  readonly files = signal<PdfFileDto[]>([]);
  childrenFolderNames: string[] = [];

  modals: { [key: string]: IModal } | null = null;
  currentlyOpenModal: IModal | null = null;

  notifications: NotificationItem[] = [];

  dataSource = new MatTableDataSource<
    SampleImageAdminDto | SampleImageDiagnosisDto
  >();
  displayedColumns: string[] = [];

  folderType: string = 'Root';

  directoryTree?: DirectoryTreeDto[];
  selectedFolder?: DirectoryDetailDto;
  selectedFolderId?: number; // this is used for selecting item in the folder view component, do not use this
  currentUrl?: string;
  selectedSamplesId: number[] = [];

  accessType: boolean = this.authServices.getRole() == RbacRoles.Admin;
  placeholderImage: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private folderState: FolderStateService,
    private sampleImageService: SampleImagesService,
    private learningService: LearningService,
    private classworkService: ClassworkService,
    private dialog: MatDialog,
    private uploadService: UploadService,
    private sanitizer: DomSanitizer,
    protected authServices: AuthService
  ) {}

  ngOnInit(): void {
    this.currentUrl = this.router.url;
    const urlSegment: string | undefined = this.currentUrl.split('/').pop();
    this.studyName = urlSegment!.charAt(0).toUpperCase() + urlSegment!.slice(1);
    this.childrenFolderNames = ['Folder1', 'Folder2', 'Folder3', 'Folder4'];
    console.log('Study name: ' + this.studyName);
    this.getFolderTree();

    if (this.studyName === 'Pregradual') {
      this.placeholderImage = 'assets/admin/pregradual-icon.svg';
    } else {
      this.placeholderImage = 'assets/admin/postgradual-icon.svg';
    }

    this.currentKeyWord.subscribe(value => {
      this.filteredkeyWords = value
        ? this.allKeyWords.filter(kw =>
            kw.toLowerCase().includes(value.toLowerCase())
          )
        : [...this.allKeyWords];
    });

    this.folderState.currentFolderId$.subscribe(id => {
      this.selectedFolderId = id!;
    });
    this.getDisplayedColumns();
  }
  getDisplayedColumns() {
    this.accessType
      ? (this.displayedColumns = [
          'navbar',
          'name',
          'hasAnnotation',
          'keyWords',
          'note',
          'id',
        ])
      : (this.displayedColumns = [
          'isFavorite',
          'name',
          'hasAnnotation',
          'caustryName',
          'keyWords',
        ]);
  }

  imageSelectorRoute() {
    const target = `${this.currentUrl}/${this.selectedFolder!.id}`;
    this.router.navigateByUrl(target);
  }

  // select endpoint based on role
  getDirectoryDetails(id: number): Observable<DirectoryDetailDto> {
    return this.accessType
      ? this.learningService.learningDirectoriyIdDetailGet$Json({ id })
      : this.classworkService.classworkDirectoriyIdDetailGet$Json({ id });
  }

  getDirectorySampleImages(
    id: number
  ): Observable<SampleImageAdminDto[] | SampleImageDiagnosisDto[]> {
    return this.accessType
      ? this.learningService.learningDirectoryIdSampleImagesGet$Json({ id })
      : this.classworkService.classworkDirectoryIdSampleImagesGet$Json({ id });
  }

  getDirectoryStudyGategory(category: string): Observable<DirectoryTreeDto[]> {
    return this.accessType
      ? this.learningService.learningDirectoriesStudyCategoryGet$Json({
          studyCategory: category,
        })
      : this.classworkService.classworkDirectoriesStudyCategoryGet$Json({
          studyCategory: category,
        });
  }

  folderDeleted(name: string) {
    this.createNotification(
      `"${name}"${translate('learning.studyPage.notifications.deletedSuccessfullyPostfix')}`
    );
  }

  updateSelected(
    selectedSamples: Set<SampleImageAdminDto | SampleImageDiagnosisDto>
  ) {
    this.selectedSamplesId = Array.from(selectedSamples).map(s => s.id);
  }

  deleteSelectedImages() {
    const requestBody: BulkSampleImageRequest = {
      sampleImageIds: this.selectedSamplesId,
    };
    if (this.selectedFolder) {
      this.learningService
        .learningDirectoryIdSampleImagesDelete({
          id: this.selectedFolder.id,
          body: requestBody,
        })
        .subscribe(() => {
          this.selectedSamplesId = [];
          this.createNotification(
            translate('learning.studyPage.notifications.sampleImagesDeleted')
          );
          this.learningService
            .learningDirectoryIdSampleImagesGet$Json({
              id: this.selectedFolder!.id,
            })
            .subscribe(data => {
              console.log(data);
              this.dataSource!.data = data;
            });
        });
    }
  }

  selectFolder(folderId: number) {
    console.log('Folder ID: ' + folderId);
    this.selectedFolderId = folderId;
    this.selectedFolderChanged(folderId);
  }

  selectedFolderChanged(folderId: number | null) {
    if (folderId == null) {
      this.selectedFolder = undefined;
      return;
    }
    this.folderState.setFolderId(folderId);
    this.selectedSamplesId = [];

    //warning
    // this.selectedFolderId = folderId;
    this.getDirectoryDetails(folderId).subscribe(details => {
      this.selectedFolder = details;
      this.files.set(details.files ?? []);
      const keywords = details.keyWords?.split(',') ?? [];
      this.filteredkeyWords = [...keywords];
      this.allKeyWords = keywords;
      this.keyWords.set(keywords);
    });

    this.getDirectorySampleImages(folderId).subscribe(data => {
      console.log('Data Image Folder');
      console.log(data);
      this.dataSource.data = data;
    });
  }

  toggleSelectedFolderVisibility() {
    if (this.selectedFolder) {
      this.learningService
        .learningDirectoryIdVisibilityPatch({
          id: this.selectedFolder.id!,
          body: {
            isPublic: !this.selectedFolder.isPublic,
          },
        })
        .subscribe(() => {
          if (this.selectedFolder!.isPublic) {
            this.folderHiddenNotify(this.selectedFolder!.name!);
          } else {
            this.folderPublishedNotify(this.selectedFolder!.name!);
          }
          this.selectedFolder!.isPublic = !this.selectedFolder!.isPublic;
          this.selectedFolderId = this.selectedFolder?.id;
          this.getFolderTree();
        });
    }
  }

  folderDetailsChanged(folderId: number) {
    if (folderId == this.selectedFolder?.id) {
      this.getDirectoryDetails(folderId).subscribe(details => {
        this.selectedFolder = details;
        this.files.set(details.files ?? []);
      });
    }
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
    this.updateFolderDetails();
    this.currentKeyWord.next('');
  }

  remove(fruit: string): void {
    const currentkeyWords = this.keyWords();
    const index = currentkeyWords.indexOf(fruit);
    if (index >= 0) {
      currentkeyWords.splice(index, 1);
      this.keyWords.set([...currentkeyWords]);
    }
    this.updateFolderDetails();
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

  updateFolderDetails() {
    if (!this.selectedFolder?.id) {
      console.log('Folder ID is not specified');
      return;
    }

    this.selectedFolder.keyWords = this.keyWords().join(',');

    this.learningService
      .learningDirectoryIdUpdatePatch({
        id: this.selectedFolder?.id,
        body: {
          keyWords: this.selectedFolder.keyWords,
          description: this.selectedFolder.description,
        },
      })
      .subscribe();
  }

  folderHiddenNotify(foldernName: string) {
    this.createNotification(
      `"${foldernName}"${translate('learning.studyPage.notifications.folderHiddenPostfix')}`
    );
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  folderPublishedNotify(_foldernName: string) {
    this.createNotification(
      translate('learning.studyPage.notifications.foldersPublished')
    );
  }

  onFileRemoved(id: number, name: string) {
    this.learningService
      .learningDirectoriesFileFileIdDelete$Plain({ fileId: id })
      .subscribe(data => {
        console.log(data);
        this.files.set(this.files().filter(f => f.id !== id));
        this.createNotification(
          `"${name}"${translate(
            'learning.studyPage.notifications.deletedSuccessfullyPostfix'
          )}`
        );
      });
  }

  onFileClick(fileId: number) {
    // Suppose your file model has a `name` like "report-123.pdf"
    const file = this.files().find(f => f.id === fileId);
    if (!file) {
      return;
    }
    let relativeFolder = file.path.replace('/media/pdfs', '');
    // remove any leading/trailing slashes so split() is clean
    relativeFolder = relativeFolder.replace(/^\/|\/$/g, '');

    // encode each folder name
    const segments = [...relativeFolder.split('/'), file.name].map(seg =>
      encodeURIComponent(seg)
    );

    const safePath = segments.join('/');
    const safeUrl = `${environment.apiUrl}/files/${safePath}`;

    window.location.href = this.sanitizer.sanitize(
      SecurityContext.URL,
      safeUrl
    ) as string;
  }

  fileNames() {
    return this.files().map(f => f.name as string);
  }

  fileIds() {
    return this.files().map(f => f.id!) ?? [];
  }

  childFolderNames() {
    return (
      this.selectedFolder?.children?.map(child => child.name! as string) ?? []
    );
  }

  childFolderIds() {
    return this.selectedFolder?.children?.map(child => child.id!) ?? [];
  }

  folderFileUpload() {
    const dialogRef = this.dialog.open(AddFileModalComponent, {
      width: '481px',
      disableClose: false,
      data: { labelkey: 'folderFileLabel', text: this.selectedFolder?.name },
    });

    const successCallback = () => {
      this.folderDetailsChanged(this.selectedFolder!.id!);
    };

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const folderId = this.selectedFolder?.id;
        if (folderId) {
          this.uploadService
            .uploadFolderFile(result, folderId, successCallback)
            .then(() => {
              console.log('Folder File Upload Success');
            })
            .catch(() => {
              console.log('Folder File Upload Failed');
            });
        } else {
          console.log('Folder is not specified');
        }
      }
    });
  }

  createFolder(newFolderName: string) {
    console.log(this.folderType);
    const newFolderBody: { name: string; studyCategory?: string } = {
      name: newFolderName,
    };
    if (this.folderType == 'Root') {
      newFolderBody.studyCategory = this.studyName!;
      this.learningService
        .learningDirectoriesNewPost$Json({
          body: newFolderBody,
        })
        .subscribe(response => {
          this.selectedFolderId = response.directoryId;
          this.getFolderTree();
        });
    } else {
      this.learningService
        .learningDirectoryIdNewPost$Json({
          id: this.selectedFolder!.id!,
          body: newFolderBody,
        })
        .subscribe(() => {
          this.getFolderTree();
          this.folderDetailsChanged(this.selectedFolder!.id!);
        });
    }
    this.folderType = 'Root';
    if (this.modals) {
      this.modals['Create'].open = false;
    }
  }

  getFolderTree() {
    this.getDirectoryStudyGategory(this.studyName!).subscribe(directories => {
      this.directoryTree = directories;
    });
  }

  openModal(modalType: string) {
    if (modalType == 'Create') {
      this.createAndOpenCreateFolderModal();
    }
    if (modalType == 'ConfirmPublish') {
      this.createAndOpenConfirmPublishModal();
    }

    if (modalType == 'ConfirmSampleImageDelete') {
      this.createAndOpenDeleteSampleImagesModal();
    }
  }

  private createAndOpenConfirmPublishModal() {
    this.learningService
      .learningDirectoryIdParentsGet$Json({
        id: this.selectedFolder!.id!,
      })
      .subscribe(data => {
        const folderNamesString = data.map(d => d.name as string).join(', ');
        let privateParentsText = '';
        if (folderNamesString) {
          privateParentsText =
            translate('learning.studyPage.dialog.privateParents') + ':';
          // privateParentsText.push(folderNamesString);
        }

        this.modals = {
          ...this.modals,
          ConfirmPublish: {
            name: 'ConfirmPublish',
            open: true,
            modalConfig: {
              title: translate('learning.studyPage.actions.publishFolder'),
              paragraphs: [
                translate('learning.studyPage.dialog.confirmPublishQuestion'),
                privateParentsText,
              ],
              highlightText: folderNamesString,
              cancelText: translate('learning.studyPage.actions.cancel'),
              confirmText: translate(
                'learning.studyPage.actions.publishFolder'
              ),
            },
            confirmAction: () => {
              this.toggleSelectedFolderVisibility();
              this.modals!['ConfirmPublish'].open = false;
            },
          },
        };
        this.currentlyOpenModal = this.modals!['ConfirmPublish'];
      });
  }

  private createAndOpenDeleteSampleImagesModal() {
    this.modals = {
      ...this.modals,
      ConfirmSampleImageDelete: {
        name: 'ConfirmDelete',
        open: true,
        modalConfig: {
          title: translate('learning.studyPage.actions.deleteImages'),
          paragraphs: [
            translate(
              'learning.studyPage.dialog.confirmDeleteSampleImagesQuestion'
            ),
          ],
          cancelText: translate('learning.studyPage.actions.cancel'),
          confirmText: translate('learning.studyPage.actions.deleteImages'),
        },
        confirmAction: () => {
          this.deleteSelectedImages();
          this.modals!['ConfirmSampleImageDelete'].open = false;
        },
      },
    };
    this.currentlyOpenModal = this.modals!['ConfirmSampleImageDelete'];
  }

  private createAndOpenCreateFolderModal() {
    this.modals = {
      Create: {
        name: 'Create',
        open: true,
        modalConfig: {
          title: translate('learning.studyPage.actions.newFolder'),
          confirmText: translate('learning.studyPage.actions.createNewFolder'),
          cancelText: translate('learning.studyPage.actions.cancel'),
          textBoxPlaceholderText: translate(
            'learning.studyPage.actions.newFolder'
          ),
        },
        createAction: (args: unknown) => {
          this.createFolder(args as string);
        },
      },
    };
    this.currentlyOpenModal = this.modals!['Create'];
  }

  private createNotification(message: string) {
    this.notifications.splice(0, 1, {
      message: message,
    });
  }

  notificationClosed() {
    if (this.notifications) {
      this.notifications.splice(0, 1);
    }
  }
}
