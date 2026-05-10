import {
  Component,
  Input,
  ViewChild,
  OnInit,
  AfterViewChecked,
  HostListener,
  OnDestroy,
  ViewContainerRef,
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatSelectChange } from '@angular/material/select';
import {
  // SampleImageDiagnosisDto,
  SampleImageNoteRequest,
  SampleImageAdminDto,
  DeleteSampleImageRequest,
  ModifySampleImageRequest,
  FlagSampleImageRequest,
  OrganDiagnosesDto,
  ModifyAction,
  FlagTypeDto,
} from 'src/app/api/models';
import { ConfirmModalConfig } from 'src/app/ui/admin/modal-actions-confirmation/modal-actions-confirmation.config';

import { translate } from '@ngneat/transloco';
import { AppRoutes, routeParamsFiller } from 'src/app/app-routing.module';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AdminService,
  SampleImagesService,
  AssetsService,
} from 'src/app/api/services';
import { AuthService } from 'src/app/oauth/auth.service';
import { TranslocoService } from '@ngneat/transloco';
// caustry upload
import { MatDialog } from '@angular/material/dialog';
import { AddFileModalComponent } from 'src/app/ui/admin/add-file-modal/add-file-modal.component';
import { UploadService } from 'src/app/services/sample-image_upload/sample-image-upload';
import { TextEditorComponent } from '../../../../ui/basic/text-editor/text-editor.component';
import { environment } from '../../../../../environments/environment';
import { PdfFile } from 'src/app/api/models';

interface NotificationItem {
  message: string;
  timeoutId?: number | null;
  relatedRow?: number[];
  eventBody?: DeleteSampleImageRequest | ModifySampleImageRequest | null;
  eventAction?: string | null;
  onCompletionCallback?: () => void;
}

@Component({
  selector: 'app-admin-sample-images',
  templateUrl: './admin-sample-images.component.html',
  styleUrl: './admin-sample-images.component.scss',
})
export class AdminSampleImagesComponent
  implements OnInit, AfterViewChecked, OnDestroy
{
  @Input() dataSource: MatTableDataSource<SampleImageAdminDto> =
    new MatTableDataSource();
  @Input() showDiagnosis = true;
  @Input() toRoute!: string;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('editorContainer', { read: ViewContainerRef })
  editorContainer!: ViewContainerRef;
  textEditorOpen: boolean = false;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  currentEditorComponentRef: any;

  isChecked: boolean = false; //flag for checked images

  modalConfirmConfig: ConfirmModalConfig | null = null;
  showModal = false;
  names: { id: number; name: string }[] | null = null;
  showEdit: boolean = false;
  showNotif: boolean = true;
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
  filterColumns: string[] = [
    'navbar_0',
    'name_1',
    'hasAnnotation_2',
    'caustryName_3',
    'keyWords_4',
    'organ_5',
    'bodySystemNames_6',
    'note_7',
    'id_8',
  ];

  editorTop = 0;
  editorLeft = 0;
  notifications: NotificationItem[] = [];
  privatRowsIds: number[] = [];
  deletedRowsIds: number[] = [];
  organs: OrganDiagnosesDto[] | null = null;
  flags: FlagTypeDto[] = [];
  selectedSamples: Set<SampleImageAdminDto> = new Set<SampleImageAdminDto>();
  currentEditRow: SampleImageAdminDto | null = null;
  currentEditColumn: string | null = null;
  editConfirm: boolean = false;
  previousState: SampleImageAdminDto[] = [];
  filterValues: { [key: string]: string } = {};
  hiddenRows: Set<SampleImageAdminDto> = new Set<SampleImageAdminDto>();

  selectedOrgan: string | null = 'Brain';

  constructor(
    private assetsService: AssetsService,
    private sampleService: SampleImagesService,
    private adminServise: AdminService,
    private authService: AuthService,
    private uploadService: UploadService,
    private router: Router,
    private route: ActivatedRoute,
    private readonly transloco: TranslocoService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.fetchData();
    this.dataSource.filterPredicate = this.filterPredicate();
    for (const col of this.displayedColumns) {
      this.filterValues[col] = '';
    }
  }

  ngOnDestroy(): void {
    this.executeLastNotification();
    window.onbeforeunload = null;
  }

  @HostListener('window:beforeunload')
  onBeforeUnload() {
    this.executeLastNotification();
  }

  filterData(): void {
    this.dataSource.filter = JSON.stringify(this.filterValues);
  }

  clearFilter(index: number): void {
    const key = this.displayedColumns[index];
    this.filterValues[key] = '';
    this.filterData();
  }

  checkSettingsColumn(index: number): boolean {
    if (index == 0) return true;
    return false;
  }

  checkIfBooleanColumn(index: number): boolean {
    return (
      this.dataSource.data.length > 0 &&
      typeof this.dataSource.data[0][
        this.displayedColumns[index] as keyof SampleImageAdminDto
      ] == 'boolean'
    );
  }

  private fetchData(): void {
    this.sampleService.adminSampleImagesGet$Json().subscribe(data => {
      this.dataSource.data = data;
      console.log(this.dataSource);
    });
    this.adminServise.adminFlagsGet$Json().subscribe(data => {
      this.flags = data;
      console.log(this.flags);
    });

    this.assetsService.assetsOrganTileListGet$Json().subscribe(data => {
      this.organs = data.organs;
      console.log(this.organs);
    });
  }

  ngAfterViewChecked() {
    this.dataSource.sort = this.sort;
  }

  invertHideDiagnosis($event: MouseEvent) {
    this.showDiagnosis = !this.showDiagnosis;
    $event.stopPropagation();
  }

  getTags(row: string): string[][] {
    const twoDimTagsArray: string[][] = [];
    const oneDimTagsArray = row.split(';');
    for (let i = 0; i < oneDimTagsArray.length; i += 2) {
      const chunk = oneDimTagsArray.slice(i, i + 2);
      twoDimTagsArray.push(chunk);
    }
    return twoDimTagsArray;
  }

  saveNote(row: SampleImageAdminDto): void {
    const userToken = this.authService.getAccessToken();
    const userId = this.authService.parseJwt(userToken).sub;

    if (userId != null) {
      const requestBody: SampleImageNoteRequest = {
        note: row.note,
      };

      this.assetsService
        .assetsSampleImageSampleIdNotePost({
          sampleId: row.id,
          body: requestBody,
        })
        .subscribe(() => {});
    }
  }

  selectSample(rows: string | SampleImageAdminDto): void {
    if (typeof rows === 'string') {
      if (this.selectedSamples.size < this.dataSource.data.length){
        this.isChecked = true;
        this.selectedSamples = new Set(this.dataSource.data);}
      else {
        this.isChecked = false;
        this.selectedSamples = new Set();
      }
    } else {
      if (!this.selectedSamples.has(rows)) {
        this.selectedSamples.add(rows);
        this.isChecked = true;
      }
      else this.selectedSamples.delete(rows);

      this.isChecked = this.selectedSamples.size > 0;
    }
  }

  toggleSelectAll(): void {
    const total = this.dataSource.data.length;
    const selected = this.selectedSamples.size;

    if (selected === total) {
      this.selectedSamples.clear();
    } else {
      this.selectedSamples = new Set(this.dataSource.data);
    }
  }

  isAllSelected(): boolean {
    return this.selectedSamples.size === this.dataSource.data.length;
  }

  isAllSelectedIndeterminate(): boolean {
    const selectedCount = this.selectedSamples.size;
    const totalCount = this.dataSource.data.length;
    return selectedCount > 0 && selectedCount < totalCount;
  }

  changeSampleFlag(row: SampleImageAdminDto) {
    let curretFlagIndex = -1;
    let flag = undefined;
    if (row.flagType) {
      curretFlagIndex = this.flags.findIndex(
        item => item.color === row.flagType?.color
      );
    }
    if (curretFlagIndex === this.flags.length - 1) flag = undefined;
    else flag = this.flags[curretFlagIndex + 1];

    if (flag) {
      const requestBody: FlagSampleImageRequest = {
        flagTypeId: flag.id,
      };

      if (this.selectedSamples.size === 0) {
        row.flagType = this.flags[curretFlagIndex + 1];

        this.sampleService
          .adminSampleImagesIdFlagPost({
            id: row.id,
            body: requestBody,
          })
          .subscribe(() => {});

        return;
      }
      this.selectedSamples.forEach(r => {
        r.flagType = this.flags[curretFlagIndex + 1];

        this.sampleService
          .adminSampleImagesIdFlagPost({
            id: r.id,
            body: requestBody,
          })
          .subscribe(() => {});
      });
    } else {
      if (this.selectedSamples.size === 0) {
        row.flagType = this.flags[curretFlagIndex + 1];

        this.sampleService
          .adminSampleImagesIdFlagDelete({
            id: row.id,
          })
          .subscribe(() => {});

        return;
      }
      this.selectedSamples.forEach(r => {
        r.flagType = this.flags[curretFlagIndex + 1];

        this.sampleService
          .adminSampleImagesIdFlagDelete({
            id: r.id,
          })
          .subscribe(() => {});
      });
    }
  }

  isSampleSelected(rows: string | SampleImageAdminDto): boolean {
    if (
      typeof rows === 'string' &&
      this.selectedSamples.size === this.dataSource.data.length
    )
      return true;
    else if (typeof rows !== 'string' && this.selectedSamples.has(rows))
      return true;
    return false;
  }

  createNotificationMessage(action: string, num: number): string {
    const numOfItems = ' (' + num + ') ';
    const message = translate('admin.adminNotification.selectedItems');
    const deleted = translate('admin.adminNotification.wasDeleted');
    const setPrivate = translate('admin.adminNotification.setAsPrivate');
    const setPublic = translate('admin.adminNotification.setAsPublic');
    if (action == 'Delete') return message + numOfItems + deleted;
    if (action == 'Private') return message + numOfItems + setPrivate;
    else return message + numOfItems + setPublic;
  }

  modifySelectedSamplesAddToQueue(
    rows: string | SampleImageAdminDto,
    action: string
  ): void {
    this.previousState = JSON.parse(JSON.stringify(this.dataSource.data));
    const deleteRequestBody: DeleteSampleImageRequest = {};
    const modifyRequestBody: ModifySampleImageRequest = {};
    if (typeof rows === 'string') {
      deleteRequestBody.iDs = Array.from(this.selectedSamples).map(
        item => item.id
      );
      if (action == 'ReverseVisibility') {
        modifyRequestBody.action = ModifyAction.ReverseVisibility;
      } else if (action == 'Publish') {
        modifyRequestBody.action = ModifyAction.Publish;
      } else {
        modifyRequestBody.action = ModifyAction.ToggleHide;
      }
      modifyRequestBody.iDs = Array.from(this.selectedSamples).map(
        item => item.id
      );

      this.selectedSamples = new Set<SampleImageAdminDto>();
    } else {
      deleteRequestBody.iDs = [rows.id];
      if (action == 'ReverseVisibility') {
        modifyRequestBody.action = ModifyAction.ReverseVisibility;
      } else if (action == 'Publish') {
        modifyRequestBody.action = ModifyAction.Publish;
      } else {
        modifyRequestBody.action = ModifyAction.ToggleHide;
      }
      modifyRequestBody.iDs = [rows.id];
    }

    if (this.notifications && this.notifications.length > 0) {
      const notification = this.notifications[0];

      if (notification.timeoutId !== null) {
        clearTimeout(notification.timeoutId);
        if (action == 'Delete')
          this.modifySelectedSamples(
            notification.eventBody as DeleteSampleImageRequest,
            null,
            notification.eventAction
          );
        else
          this.modifySelectedSamples(
            null,
            notification.eventBody as ModifySampleImageRequest,
            notification.eventAction
          );
      }
    }
    const timerCallback = () => {
      this.modifySelectedSamples(deleteRequestBody, modifyRequestBody, action);
    };
    const timeoutId = setTimeout(timerCallback, 5100);

    if (action === 'Delete') {
      const notifiMessage = this.createNotificationMessage(
        'Delete',
        deleteRequestBody.iDs.length
      );
      this.notifications.splice(0, 1, {
        message: notifiMessage,
        timeoutId: timeoutId,
        relatedRow: deleteRequestBody.iDs,
        eventAction: action,
        eventBody: deleteRequestBody,
        onCompletionCallback: timerCallback,
      });
      const deleteIds = deleteRequestBody?.iDs ?? [];
      if (deleteIds.length > 0) {
        this.dataSource.data = this.dataSource.data.filter(
          row => !deleteIds.includes(row.id)
        );
      }
    } else {
      // Update UI state based on explicit action (Publish vs Private vs ReverseVisibility)
      const ids = modifyRequestBody?.iDs ?? [];

      if (action === 'Publish') {
        // Mark selected rows as public (visible)
        this.dataSource.data.forEach(row => {
          if (ids.includes(row.id)) {
            row.isVisible = true;
          }
        });
        const notifiMessage = this.createNotificationMessage('Public', ids.length);
        this.notifications.splice(0, 1, {
          message: notifiMessage,
          timeoutId: timeoutId,
          relatedRow: ids,
          eventAction: action,
          eventBody: modifyRequestBody,
          onCompletionCallback: timerCallback,
        });
      } else if (action === 'ReverseVisibility') {
        let notifiMessage = this.createNotificationMessage('Public', ids.length);
        // Reverse visibility state for each selected row
        this.dataSource.data.forEach(row => {
          if (ids.includes(row.id)) {
            if(row.isVisible) notifiMessage = this.createNotificationMessage('Private', ids.length);
            row.isVisible = !row.isVisible;
          }
        });
        this.notifications.splice(0, 1, {
          message: notifiMessage,
          timeoutId: timeoutId,
          relatedRow: ids,
          eventAction: action,
          eventBody: modifyRequestBody,
          onCompletionCallback: timerCallback,
        });
      } else {
        // Mark selected rows as private (not visible)
        this.dataSource.data.forEach(row => {
          if (ids.includes(row.id)) {
            row.isVisible = false;
          }
        });
        const notifiMessage = this.createNotificationMessage('Private', ids.length);
        this.notifications.splice(0, 1, {
          message: notifiMessage,
          timeoutId: timeoutId,
          relatedRow: ids,
          eventAction: action,
          eventBody: modifyRequestBody,
          onCompletionCallback: timerCallback,
        });
      }
    }
  }

  modifySelectedSamples(
    deleteRequestBody?: DeleteSampleImageRequest | null,
    modifyRequestBody?: ModifySampleImageRequest | null,
    action?: string | null
  ): void {
    if (action == 'Delete' && deleteRequestBody) {
      this.sampleService
        .adminSampleImagesBatchDelete({
          body: deleteRequestBody,
        })
        .subscribe();
    } else if (modifyRequestBody) {
      this.sampleService
        .adminSampleImagesBatchPatch({
          body: modifyRequestBody,
        })
        .subscribe();
    }
  }

  toggleRow(row: SampleImageAdminDto, event: MouseEvent): void {
    if (this.hiddenRows.has(row)) {
      this.hiddenRows.delete(row);
      this.currentEditRow = null;
      this.currentEditColumn = null;
    } else {
      this.hiddenRows.add(row);
      this.currentEditRow = row;
      this.currentEditColumn = 'Tags';
      console.log(this.currentEditRow);
    }

    if (event) {
      const rowElement = event.currentTarget as HTMLElement;
      const containerElement = rowElement.closest(
        '.samples-table'
      ) as HTMLElement;

      if (rowElement && containerElement) {
        const rowElement = event.currentTarget as HTMLElement;
        const container = rowElement.closest('.samples-table') as HTMLElement;

        const rowRect = rowElement.getBoundingClientRect();
        const containerRect = container.getBoundingClientRect();

        this.editorTop = rowRect.bottom - containerRect.top;
        this.editorLeft = rowRect.left - containerRect.left;
        console.log(this.editorTop);
        console.log(this.editorLeft);
      }
    }
  }

  isRowExpanded(row: SampleImageAdminDto): boolean {
    return this.hiddenRows.has(row);
  }

  onRowClicked(row: SampleImageAdminDto): void {
    this.route.params.subscribe(() => {
      this.router.navigate([
        routeParamsFiller(AppRoutes.app.adminSampleImage, {
          sampleId: row.id,
        }),
      ]);
    });
  }

  private filterPredicate(): (
    data: SampleImageAdminDto,
    filter: string
  ) => boolean {
    return (data: SampleImageAdminDto, filter: string): boolean => {
      const filterValues = JSON.parse(filter);
      return Object.keys(filterValues).every(key => {
        const searchText = filterValues[key].trim().toLowerCase();
        if (!searchText) return true;

        const value = data[key as keyof SampleImageAdminDto] ?? '';
        const formattedValue = this.formatTableValue(value);

        return formattedValue.includes(searchText);
      });
    };
  }
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private formatTableValue(value: any): string {
    if (Array.isArray(value)) {
      return value
        .map(item => String(item))
        .join(', ')
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '')
        .toLowerCase();
    }

    if (typeof value === 'boolean') {
      return this.transloco
        .translate(
          `admin.samplesList.table.values.boolCols.${value ? 'yes' : 'no'}`
        )
        .toLowerCase();
    }

    if (typeof value === 'object' && value !== null) {
      return value.name ? String(value.name) : JSON.stringify(value);
    }

    return String(value)
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toLowerCase();
  }

  onFlagDelete(flag: FlagTypeDto | null) {
    if (flag) {
      if (flag.id) {
        this.adminServise
          .adminFlagIdDelete$Response({
            id: flag.id,
          })
          .subscribe(data => {
            console.log(data.status);
          });
        console.log(' deleted:', flag.id);
        this.flags = this.flags.filter(t => t !== flag);
      }
    }
  }

  onFlagEdit(flag: FlagTypeDto | null) {
    if (flag) {
      console.log(' edited:', flag.id);

      if (flag.id) {
        this.adminServise
          .adminFlagsPost({
            body: flag,
          })
          .subscribe(() => {});
      }
    }
  }

  onFlagCreated(label: { name: string; color: string }) {
    console.log(' Created:', label);
    const newFlag: FlagTypeDto = { color: label.color, name: label.name };
    this.adminServise
      .adminFlagsPost({
        body: newFlag,
      })
      .subscribe(() => {
        this.adminServise.adminFlagsGet$Json().subscribe(data => {
          this.flags = data;
          console.log(this.flags);
        });
      });
    // const exists = this.flags.some(item => item.color.toLowerCase() === label.color.toLowerCase());
    // if(!exists){
    //   this.flags.push({id:0, color:label.color, name:label.name})
    // }
  }

  getUsedColors() {
    return this.flags.map(item => item.color.toLowerCase());
  }

  showNotification() {
    this.showNotif = true;
  }

  handleClosedNotifications(type: string) {
    if (this.notifications) {
      const notification = this.notifications[0];

      if (type === 'undo' && notification.timeoutId !== null) {
        console.log('undo');
        clearTimeout(notification.timeoutId);
        this.notifications.splice(0, 1, {
          message: translate('admin.adminNotification.undoAction'),
        });
        this.dataSource.data = this.previousState;
      } else if (type === 'userClose' && notification.timeoutId !== null) {
        this.executeLastNotification();
      } else {
        this.notifications.splice(0, 1);
      }

      if (this.notifications.length == 0) {
        this.showNotif = false;
      }
    }
  }

  executeLastNotification() {
    if (this.notifications) {
      const notification = this.notifications[0];
      if (
        notification?.timeoutId !== null &&
        notification?.onCompletionCallback
      ) {
        clearTimeout(notification.timeoutId);
        //Apply changes instantly if there is any
        notification.onCompletionCallback();
      }

      this.notifications.splice(0, 1);
    }
  }

  closeTagsEditor(): void {
    if (this.currentEditRow) {
      this.hiddenRows.delete(this.currentEditRow);

      const sample: SampleImageAdminDto = this.currentEditRow;
      if (this.organs) {
        const organ = this.organs.find(
          o => o.name === this.currentEditRow?.organName
        );
        this.adminServise.adminEditSamplePost({
          body: sample,
          organId: organ?.id,
        });
        console.log('close save');
        this.currentEditRow = null;
        this.currentEditColumn = null;
        this.editConfirm = false;
      }
    }
  }

  saveTagChanges(newTagString: string): void {
    console.log('save');
    if (this.currentEditRow) {
      this.currentEditRow.keyWords = newTagString;
      const sample: SampleImageAdminDto = this.currentEditRow;
      if (this.organs) {
        const organ = this.organs.find(
          o => o.name === this.currentEditRow?.organName
        );
        console.log(organ);
        this.adminServise
          .adminEditSamplePost({
            body: sample,
            organId: organ?.id,
          })
          .subscribe(() => {
            this.sampleService.adminSampleImagesGet$Json().subscribe(data => {
              this.dataSource.data = data;
              console.log(this.dataSource);
            });
          });
      }
    }
  }

  saveNameChanges() {
    console.log('save');
    if (this.currentEditRow) {
      const sample: SampleImageAdminDto = this.currentEditRow;
      if (this.organs) {
        const organ = this.organs.find(
          o => o.name === this.currentEditRow?.organName
        );
        console.log(organ);
        this.adminServise
          .adminEditSamplePost({
            body: sample,
            organId: organ?.id,
          })
          .subscribe(() => {
            this.sampleService.adminSampleImagesGet$Json().subscribe(data => {
              this.dataSource.data = data;
              console.log(this.dataSource);
            });
          });

        this.currentEditRow = null;
        this.currentEditColumn = null;
        this.editConfirm = false;
      }
    }
  }

  saveOrganChanges(event: MatSelectChange) {
    const newOrgan = event.value;
    this.editConfirm = false;
    if (this.currentEditRow) {
      const sample: SampleImageAdminDto = this.currentEditRow;
      if (this.organs) {
        const organ = this.organs.find(o => o.name === newOrgan);
        this.currentEditRow = null;
        this.currentEditColumn = null;
        this.editConfirm = false;
        this.adminServise
          .adminEditSamplePost({
            body: sample,
            organId: organ?.id,
          })
          .subscribe(() => {
            this.sampleService.adminSampleImagesGet$Json().subscribe(data => {
              this.dataSource.data = data;
              console.log(this.dataSource);
            });
          });
      }
    }
  }
  onRightClickDisContextMenu(event: MouseEvent) {
    event.preventDefault();
  }
  onRightClick(event: MouseEvent, col: string, row: SampleImageAdminDto) {
    this.onRightClickDisContextMenu(event);
    this.selectedOrgan = row.organName;
    this.currentEditRow = row;
    this.currentEditColumn = col;
    if (event) {
      const rowElement = event.currentTarget as HTMLElement;
      const containerElement = rowElement.closest(
        '.samples-table'
      ) as HTMLElement;

      if (rowElement && containerElement) {
        const rowElement = event.currentTarget as HTMLElement;
        const container = rowElement.closest('.samples-table') as HTMLElement;

        const rowRect = rowElement.getBoundingClientRect();
        const containerRect = container.getBoundingClientRect();

        this.editorTop = rowRect.bottom - containerRect.top;
        this.editorLeft = rowRect.left - containerRect.left + 70;
        console.log(this.editorTop);
        console.log(this.editorLeft);
      }
    }
  }

  onEditConfirm() {
    this.editConfirm = true;
    this.editorLeft = this.editorLeft - 58;
    this.editorTop = this.editorTop - 45;
    if (this.currentEditColumn == 'Caustry') {
      this.modalConfirmConfig = {
        title: translate('admin.confirmModal.titleMainDeleteConfirm'),
        paragraphs: [
          translate('admin.confirmModal.textMainDeleteConfirm', {
            file: this.currentEditRow?.caustryFile?.name,
          }),
        ],
        highlightText: '',
        confirmText: translate('admin.confirmModal.delete'),
        cancelText: translate('admin.confirmModal.cancel'),
      };
    }
  }

  onConfirmDeleteCaustry() {
    if (this.currentEditRow) {
      const rowId = this.currentEditRow?.id;
      this.adminServise
        .adminCaustryIdDelete({
          id: rowId,
        })
        .subscribe(() => {
          this.sampleService.adminSampleImagesGet$Json().subscribe(data => {
            this.dataSource.data = data;
            console.log(this.dataSource);
          });
        });
    }
    this.currentEditRow = null;
    this.currentEditColumn = null;
    this.editConfirm = false;
  }

  onCancelModalCaustry() {
    this.currentEditRow = null;
    this.currentEditColumn = null;
    this.editConfirm = false;
  }

  openEditor(row: SampleImageAdminDto, inputElement: EventTarget | null): void {
    if (!(inputElement instanceof HTMLInputElement)) {
      return; // Exit if the target is not an input element
    }

    this.editorContainer.clear();
    this.textEditorOpen = true;

    // dynamic component creation
    this.currentEditorComponentRef =
      this.editorContainer.createComponent(TextEditorComponent);

    this.currentEditorComponentRef.instance.content = row.note;
    this.currentEditorComponentRef.instance.translations = {
      save: this.transloco.translate(
        'organDetail.table.values.note.editor.save'
      ),
      close: this.transloco.translate(
        'organDetail.table.values.note.editor.close'
      ),
    };

    this.currentEditorComponentRef.instance.contentSaved.subscribe(
      (newContent: string) => {
        row.note = newContent;
        this.saveNote(row);
        this.closeEditor();
      }
    );

    this.currentEditorComponentRef.instance.editorClosed.subscribe(() => {
      this.closeEditor();
    });

    const rect = inputElement.getBoundingClientRect();

    if (rect) {
      this.styleTextEditorDynamically(rect);
    }
  }

  closeEditor(): void {
    this.editorContainer.clear();
    this.textEditorOpen = false;
  }

  filterHtmlTags(htmlString: string): string {
    if (!htmlString) {
      return '';
    }
    const editedString = htmlString?.replace(/<\/?[^>]+(>|$)/g, '');
    return editedString.length > 20
      ? editedString.slice(0, 20) + '...'
      : editedString;
  }

  @HostListener('document:click', ['$event'])
  onClick(event: MouseEvent) {
    if (this.textEditorOpen) {
      const editorElement = this.currentEditorComponentRef?.location
        .nativeElement as HTMLElement;
      const target = event.target as HTMLElement;

      if (
        !editorElement.contains(target) &&
        !target.classList.contains('samples-table')
      ) {
        this.closeEditor();
      }
    }
  }

  private styleTextEditorDynamically(rect: DOMRect): void {
    const editorElement = this.currentEditorComponentRef.location
      .nativeElement as HTMLElement;
    const topPosition =
      rect.bottom + 250 > document.documentElement.scrollHeight
        ? window.innerHeight - 260
        : rect.bottom;

    editorElement.style.position = 'absolute';
    editorElement.style.top = `${topPosition + window.scrollY}px`;
    editorElement.style.right = '20px';
    editorElement.style.zIndex = '1000';
    editorElement.style.backgroundColor = 'white';
    editorElement.style.borderRadius = '9px';
    editorElement.style.boxShadow = '0px 4px 6px rgba(0, 0, 0, 0.1)';
  }

  addCaustryDialog(row: SampleImageAdminDto) {
    const name = row.name;
    const dialogRef = this.dialog.open(AddFileModalComponent, {
      width: '481px',
      disableClose: false,
      data: { labelkey: 'caustryLabel', text: name },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        row.caustryFile = {
          id: 0,
          name: 'progress',
          path: '/404',
        };
        this.uploadService
          .uploadCaustry(result, row.id)
          .then(() => {
            row.caustryFile = {
              id: 0,

              name: result.name,
              path: row.path + '/caustryFile' + result.name,
            };
          })
          .catch(() => {
            row.caustryFile = {
              id: 0,
              name: 'error',
              path: row.path + '/caustryFile' + result.name,
            };
          });
      }
    });
  }

  getCaustryPath(caustryFile: PdfFile): string {
    const relativeFolder = caustryFile.path
      .replace('/media', '')
      .replace(/^\/|\/$/g, '');

    const segments = [...relativeFolder.split('/'), caustryFile.name].map(seg =>
      encodeURIComponent(seg)
    );

    return `${environment.apiUrl}/${segments.join('/')}`;
  }
}
