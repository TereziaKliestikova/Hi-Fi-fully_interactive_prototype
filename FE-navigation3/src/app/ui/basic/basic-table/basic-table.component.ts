import {
  Component,
  Input,
  ViewChild,
  AfterViewChecked,
  SimpleChanges,
  ViewContainerRef,
  HostListener,
  OnInit,
  OnChanges,
  Inject,
} from '@angular/core';
import { TextEditorComponent } from '../text-editor/text-editor.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import {
  PdfFile,
  SampleImageDiagnosisDto,
  SampleImageNoteRequest,
} from 'src/app/api/models';

import { AppRoutes, routeParamsFiller } from 'src/app/app-routing.module';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetsService } from 'src/app/api/services';
import { AuthService } from 'src/app/oauth/auth.service';
import { TranslocoService } from '@ngneat/transloco';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-basic-table',
  templateUrl: './basic-table.component.html',
  styleUrls: ['./basic-table.component.scss'],
})
export class BasicTableComponent
  implements AfterViewChecked, OnInit, OnChanges
{
  @ViewChild('editorContainer', { read: ViewContainerRef })
  editorContainer!: ViewContainerRef;
  @ViewChild(MatSort) sort!: MatSort;
  @Input() dataSource: MatTableDataSource<SampleImageDiagnosisDto> =
    new MatTableDataSource();
  @Input() displayedColumns: string[] = [];
  @Input() showDiagnosis = true;
  @Input() isPdfAttached = false;
  @Input() toRoute!: string;

  filterColumns: string[] = [];
  filterValues: { [key: string]: string } = {};

  textEditorOpen: boolean = false;
  //eslint-disable-next-line @typescript-eslint/no-explicit-any
  currentEditorComponentRef: any;

  hiddenRows: Set<SampleImageDiagnosisDto> = new Set<SampleImageDiagnosisDto>();
  hideRelevantColumns = false;

  constructor(
    private readonly assetsService: AssetsService,
    private readonly authService: AuthService,
    @Inject(Router) private readonly router: Router,
    @Inject(ActivatedRoute) private readonly route: ActivatedRoute,
    private readonly transloco: TranslocoService
  ) {}

  ngOnInit(): void {
    this.dataSource.filterPredicate = this.filterPredicate();
  }

  ngAfterViewChecked(): void {
    this.dataSource.sort = this.sort;
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

  invertHideDiagnosis($event: MouseEvent): void {
    this.showDiagnosis = !this.showDiagnosis;
    $event.stopPropagation();
  }

  filterData(): void {
    this.dataSource.filter = JSON.stringify(this.filterValues);
  }

  clearFilter(index: number): void {
    const key = this.displayedColumns[index];
    this.filterValues[key] = '';
    this.filterData();
  }

  getDynamicMaxHeight(): string {
    return this.isPdfAttached ? 'calc(100vh - 400px)' : 'calc(100vh - 380px)';
  }

  checkIfBooleanColumn(index: number): boolean {
    return (
      this.dataSource.data.length > 0 &&
      typeof this.dataSource.data[0][
        this.displayedColumns[index] as keyof SampleImageDiagnosisDto
      ] == 'boolean'
    );
  }

  getTags(row: string): string[][] {
    const twoDimTagsArray: string[][] = [];
    const oneDimTagsArray = row.split(';');
    for (let i = 0; i < oneDimTagsArray.length; i += 3) {
      const chunk = oneDimTagsArray.slice(i, i + 3);
      twoDimTagsArray.push(chunk);
    }
    return twoDimTagsArray;
  }

  getFlatTags(keyWords: string): string[] {
    return this.getTags(keyWords).flat();
  }

  hasOverflow(element: HTMLElement): boolean {
    return element.scrollWidth > element.clientWidth;
  }

  getVisibleTags(keyWords: string): string[] {
    // choose how many you want visible in collapsed state
    return this.getFlatTags(keyWords).slice(0, 3);
  }

  hasHiddenTags(keyWords: string, element?: HTMLElement): boolean {
    const hasMoreThanVisibleLimit = this.getFlatTags(keyWords).length > 3;
    const isCut = element ? this.hasOverflow(element) : false;

    return hasMoreThanVisibleLimit || isCut;
  }

  isSampleImagesListPage(): boolean {
    return this.router.url.includes('/app/sample-images/list');
  }

  saveNote(row: SampleImageDiagnosisDto): void {
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

  toggleRow(row: SampleImageDiagnosisDto): void {
    if (this.hiddenRows.has(row)) {
      this.hiddenRows.delete(row);
    } else {
      this.hiddenRows.add(row);
    }
  }

  isRowExpanded(row: SampleImageDiagnosisDto): boolean {
    return this.hiddenRows.has(row);
  }

  openEditor(
    row: SampleImageDiagnosisDto,
    inputElement: EventTarget | null
  ): void {
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
    const editedString = htmlString.replace(/<\/?[^>]+(>|$)/g, '');
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

  onRowClicked(row: SampleImageDiagnosisDto): void {
    const fullUrl = this.router.url.split('/');
    fullUrl.pop();
    const parentUrl = fullUrl.join('/');
    this.route.params.subscribe(params => {
      const id = params['organId'] || params['systemId'];
      console.log(
        'parent: ',
        parentUrl,
        'organCatalog',
        AppRoutes.app.organCatalog,
        'systemCatalog: ',
        AppRoutes.app.systemCatalog
      );
      switch (parentUrl) {
        case AppRoutes.app.sampleImagesCatalog:
          this.router.navigate([
            routeParamsFiller(AppRoutes.app.sampleImage, {
              sampleId: row.id,
            }),
          ]);
          break;
        case AppRoutes.app.organCatalog:
          this.router.navigate([
            routeParamsFiller(AppRoutes.app.sampleOrganImage, {
              organId: id,
              sampleId: row.id,
            }),
          ]);
          break;
        case AppRoutes.app.systemCatalog:
          this.router.navigate([
            routeParamsFiller(AppRoutes.app.sampleSystemImage, {
              systemId: id,
              sampleId: row.id,
            }),
          ]);
          break;
      }
    });
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

  private filterPredicate(): (
    data: SampleImageDiagnosisDto,
    filter: string
  ) => boolean {
    return (data: SampleImageDiagnosisDto, filter: string): boolean => {
      const filterValues = JSON.parse(filter);
      return Object.keys(filterValues).every(key => {
        const searchText = filterValues[key]
          .trim()
          .normalize('NFD')
          .replace(/[\u0300-\u036f]/g, '')
          .toLowerCase();
        if (!searchText) return true;

        const value = data[key as keyof SampleImageDiagnosisDto] ?? '';
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

  // as the component is created dynamically, some styles also have to be defined through JS DOM manipulation
  // otherwise they would not work
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
