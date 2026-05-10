import {
  Component,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  OnInit,
  AfterViewChecked,
  SimpleChanges,
  OnDestroy,
  OnChanges,
  ViewContainerRef,
  HostListener,
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { PdfFile, SampleImageDiagnosisDto } from 'src/app/api/models';
import { MatSort } from '@angular/material/sort';
import {
  SampleImageNoteRequest,
  SampleImageAdminDto,
  DeleteSampleImageRequest,
  ModifySampleImageRequest,
} from 'src/app/api/models';
import { ConfirmModalConfig } from 'src/app/ui/admin/modal-actions-confirmation/modal-actions-confirmation.config';

import { AppRoutes, routeParamsFiller } from 'src/app/app-routing.module';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AdminService,
  SampleImagesService,
  AssetsService,
} from 'src/app/api/services';
import { AuthService } from 'src/app/oauth/auth.service';
import { TranslocoService } from '@ngneat/transloco';
import { TextEditorComponent } from '../../basic/text-editor/text-editor.component';
import { environment } from '../../../../environments/environment';

// eslint-disable-next-line @typescript-eslint/no-unused-vars
interface NotificationItem {
  message: string;
  timeoutId?: number | null;
  relatedRow?: number[];
  eventBody?: DeleteSampleImageRequest | ModifySampleImageRequest | null;
  eventAction?: string | null;
  onCompletionCallback?: () => void;
}

@Component({
  selector: 'app-classwork-table',
  templateUrl: './classwork-table.component.html',
  styleUrl: './classwork-table.component.scss',
})
export class ClassworkTableComponent
  implements OnInit, AfterViewChecked, OnDestroy, OnChanges
{
  @Input() dataSource!: MatTableDataSource<
    SampleImageAdminDto | SampleImageDiagnosisDto
  >;
  @Input() showDiagnosis = true;
  @Input() toRoute!: string;
  @Input() isAdmin = false;

  @ViewChild('editorContainer', { read: ViewContainerRef })
  editorContainer!: ViewContainerRef;
  textEditorOpen: boolean = false;
  //eslint-disable-next-line @typescript-eslint/no-explicit-any
  currentEditorComponentRef: any;

  @ViewChild(MatSort) sort!: MatSort;
  modalConfig: ConfirmModalConfig | null = null;
  showModal = false;
  names: { id: number; name: string }[] | null = null;
  showEdit: boolean = false;
  showNotif: boolean = true;
  @Input() displayedColumns: string[] = [];

  filterColumns: string[] = [];

  selectedSamples = new Set<SampleImageAdminDto | SampleImageDiagnosisDto>();
  @Output() selectionChanged = new EventEmitter<
    Set<SampleImageAdminDto | SampleImageDiagnosisDto>
  >();

  @Input() hiddenSamples: Set<SampleImageAdminDto> =
    new Set<SampleImageAdminDto>();
  filterValues: { [key: string]: string } = {};
  hiddenRows: Set<SampleImageAdminDto> = new Set<SampleImageAdminDto>();

  constructor(
    private assetsService: AssetsService,
    private sampleService: SampleImagesService,
    private adminServise: AdminService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private readonly transloco: TranslocoService
  ) {}

  ngOnInit(): void {
    this.dataSource.filterPredicate = this.filterPredicate();
    for (const col of this.displayedColumns) {
      this.filterValues[col] = '';
    }
  }

  ngOnDestroy(): void {
    window.onbeforeunload = null;
  }

  ngOnChanges(changes: SimpleChanges): void {
    const cols = changes['displayedColumns']?.currentValue?.map(
      (col: string, index: number) => col + '_' + index
    );
    this.filterColumns = cols || this.filterColumns;

    for (const col of this.displayedColumns) {
      this.filterValues[col] = '';
    }
  }

  onFavouriteIconClick(row: SampleImageDiagnosisDto): void {
    const userToken = this.authService.getAccessToken();
    const userId = this.authService.parseJwt(userToken).sub;

    if (userId != null) {
      row.isFavorite = !row.isFavorite;

      if (row.isFavorite) {
        this.assetsService
          .assetsSampleImageSampleIdFavoritePost({
            sampleId: row.id,
          })
          .subscribe();
      } else {
        this.assetsService
          .assetsSampleImageSampleIdFavoriteDelete({
            sampleId: row.id,
          })
          .subscribe();
      }
    }
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
    return this.displayedColumns[index] === 'navbar';
  }

  checkIfBooleanColumn(index: number): boolean {
    if (!this.dataSource.data.length) return false;

    const columnKey = this.displayedColumns[index];
    // найдём первую не-пустую ячейку1. `
    const firstNonEmpty = this.dataSource.data
      //eslint-disable-next-line @typescript-eslint/no-explicit-any
      .map(row => (row as any)[columnKey])
      .find(v => v !== null && v !== undefined);

    if (firstNonEmpty === undefined) {
      return false;
    }

    if ('isVisible' in this.dataSource.data[0]) {
      return (
        typeof (this.dataSource.data[0] as SampleImageAdminDto)[
          columnKey as keyof SampleImageAdminDto
        ] === 'boolean'
      );
    } else {
      return (
        typeof (this.dataSource.data[0] as SampleImageDiagnosisDto)[
          columnKey as keyof SampleImageDiagnosisDto
        ] === 'boolean'
      );
    }
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

  toggleRow(row: SampleImageAdminDto): void {
    if (this.hiddenRows.has(row)) {
      this.hiddenRows.delete(row);
    } else {
      this.hiddenRows.add(row);
    }
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
      if (this.selectedSamples!.size < this.dataSource.data.length)
        this.selectedSamples = new Set(this.dataSource.data);
      else this.selectedSamples = new Set();
    } else {
      if (!this.selectedSamples.has(rows)) this.selectedSamples.add(rows);
      else this.selectedSamples.delete(rows);
    }
    this.selectionChanged.emit(this.selectedSamples);
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

  isRowExpanded(row: SampleImageAdminDto): boolean {
    return this.hiddenRows.has(row);
  }

  onRowClicked(row: SampleImageAdminDto | SampleImageDiagnosisDto): void {
    const fullUrl = this.router.url.split('/');
    fullUrl.pop();
    const parentUrl = fullUrl.join('/');
    this.route.params.subscribe(params => {
      const studyName = params['studyName'];
      console.log('parent: ', parentUrl);
      console.log('params: ', params);
      console.log(AppRoutes.app.classworkStudyDetail);

      switch (parentUrl) {
        case AppRoutes.app.adminClassworkStudyCategories:
          this.router.navigate([
            routeParamsFiller(AppRoutes.app.adminClassworkSampleImage, {
              sampleId: row.id,
              studyName: studyName,
            }),
          ]);
          console.log('Admin navigating: ', row);
          break;
        case AppRoutes.app.classworkStudyCategories:
          this.router.navigate([
            routeParamsFiller(AppRoutes.app.classworkSampleImage, {
              sampleId: row.id,
              studyName: studyName,
            }),
          ]);
          console.log('Student navigating: ', row);
          break;
      }
    });
  }

  private filterPredicate(): (
    data: SampleImageAdminDto | SampleImageDiagnosisDto,
    filter: string
  ) => boolean {
    return (data, filter) => {
      const filterValues = JSON.parse(filter) as Record<string, string>;

      return Object.keys(filterValues).every(key => {
        const searchText = filterValues[key].trim().toLowerCase();
        if (!searchText) return true;
        //eslint-disable-next-line @typescript-eslint/no-explicit-any
        let rawValue: any;
        if ('isVisible' in data) {
          rawValue = (data as SampleImageAdminDto)[
            key as keyof SampleImageAdminDto
          ];
        } else {
          rawValue = (data as SampleImageDiagnosisDto)[
            key as keyof SampleImageDiagnosisDto
          ];
        }

        const formatted = this.formatTableValue(rawValue ?? '');
        return formatted.includes(searchText);
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

  //Notes dialog
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

    editorElement.style.position = 'fixed';
    editorElement.style.top = `${topPosition}px`;
    editorElement.style.right = '40px';
    editorElement.style.zIndex = '1000';
    editorElement.style.backgroundColor = 'white';
    editorElement.style.borderRadius = '9px';
    editorElement.style.boxShadow = '0px 4px 6px rgba(0, 0, 0, 0.1)';
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
