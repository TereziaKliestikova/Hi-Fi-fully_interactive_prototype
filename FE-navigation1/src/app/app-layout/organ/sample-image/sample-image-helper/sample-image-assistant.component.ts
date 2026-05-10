import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
  Inject,
} from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
import { MatSort } from '@angular/material/sort';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from 'src/app/oauth/auth.service';
import { SampleImagePageFloatingContentPositionService } from 'src/app/services/sample-image-page-floating-content-position.service';
import { Annotation } from 'src/types/Annotation';
import { TranslocoService } from '@ngneat/transloco';
import { AssetsService } from 'src/app/api/services/assets.service';
import { ActivatedRoute } from '@angular/router';

import { TextEditorComponent } from '../../../../ui/basic/text-editor/text-editor.component';

import {
  PdfFile,
  SampleImageAnnotationDto,
  SampleImageDto,
  SampleImageNoteRequest,
} from 'src/app/api/models';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-sample-image-assistant',
  templateUrl: './sample-image-assistant.component.html',
  styleUrl: './sample-image-assistant.component.scss',
})
export class SampleImageAssistantComponent implements OnInit, OnDestroy {
  sampleId: number | undefined;
  @Input() toggleEvent!: Observable<void>;
  @Output() annotationCreate: EventEmitter<Annotation> = new EventEmitter();
  @Output() annotationEdit: EventEmitter<Annotation> = new EventEmitter();
  @ViewChild('descriptionAccordion') accordion!: MatAccordion;
  @ViewChild('editorContainer', { read: ViewContainerRef })
  editorContainer!: ViewContainerRef;
  @ViewChild(MatSort) sort!: MatSort;

  private eventsSubscription!: Subscription;
  assistantVisible: boolean = false;
  descriptionVisible: boolean = true;
  textEditorOpen: boolean = false;
  //eslint-disable-next-line @typescript-eslint/no-explicit-any
  currentEditorComponentRef: any;
  annotationVisible: boolean = true;
  sampleImage!: SampleImageDto;

  annotations: Annotation[] = [];

  constructor(
    private readonly authService: AuthService,
    @Inject(ActivatedRoute) private route: ActivatedRoute,
    private transloco: TranslocoService,
    public sampleImageHelperService: SampleImagePageFloatingContentPositionService,
    private assetsService: AssetsService
  ) {}

  ngOnInit() {
    // TODO remove events and make it simpler
    this.eventsSubscription = this.toggleEvent.subscribe(() =>
      this.toggleAssistantVisibility()
    );

    this.route.params.subscribe(params => {
      this.sampleId = params['sampleId'];
    });

    if (this.sampleId) {
      this.fetchAnnotations(this.sampleId);
      this.fetchSampleData(this.sampleId);
    }
  }

  ngOnDestroy() {
    this.eventsSubscription.unsubscribe();
  }

  private fetchSampleData(sampleId: number): void {
    this.assetsService
      .assetsSampleImageSampleIdGet$Json({ sampleId: sampleId })
      .subscribe(data => {
        this.sampleImage = data;
      });
  }

  private fetchAnnotations(sampleId: number): void {
    this.assetsService
      .assetsSampleImageAnnotationsIdGet$Json({ id: sampleId })
      .subscribe(data => {
        this.annotations = this.parseAnnotations(data.sampleImageAnnotations);
      });
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

  openEditor(row: SampleImageDto, inputElement: EventTarget | null): void {
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
  }

  saveNote(row: SampleImageDto): void {
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

  private parseAnnotations(
    annotations: SampleImageAnnotationDto[]
  ): Annotation[] {
    return annotations.map(annotation => {
      // Parse the boundingBox to the BBOX object
      const boundingBox = JSON.parse(annotation.boundingBox);

      // Extract the coordinates array from the Geometry object
      const coordinates = boundingBox.Geometry.Coordinates[0];

      // Calculate the Minimum Enclosing Rectangle (MER) for the polygon
      let minX = coordinates[0][0],
        minY = coordinates[0][1],
        maxX = coordinates[0][0],
        maxY = coordinates[0][1];

      coordinates.forEach((point: number[]) => {
        minX = Math.min(minX, point[0]);
        maxX = Math.max(maxX, point[0]);
        minY = Math.min(minY, point[1]);
        maxY = Math.max(maxY, point[1]);
      });

      return {
        id: annotation.id,
        name: annotation.name,
        displayedName: annotation.name,
        description: annotation.description,
        coords: {
          minX: minX,
          minY: minY,
          maxX: maxX,
          maxY: maxY,
        },
        visible: false,
      };
    });
  }

  toggleAssistantVisibility() {
    this.assistantVisible = !this.assistantVisible;
  }

  toggleShowDescription(): void {
    this.descriptionVisible = !this.descriptionVisible;
    this.accordion.closeAll();
  }

  revertToggleDescription(): void {
    if (!this.descriptionVisible) {
      this.descriptionVisible = true;
    }
  }

  toggleShowAnnotationName(): void {
    this.annotationVisible = !this.annotationVisible;
    // TODO: Change to select translate
    const area = this.transloco.translate('sampleImage.assistant.area');
    this.annotations.forEach((annotation, index) => {
      annotation.displayedName = this.annotationVisible
        ? annotation.name
        : `${area} ${index + 1}`;
      this.annotationEdit.emit(annotation);
    });
  }

  toggleAnnotation(annotation: Annotation): void {
    annotation.visible = !annotation.visible;
    this.annotationCreate.emit(annotation);
  }

  // Parameter index is not used but needs to be present for the trackBy function
  trackAnnotationById(_index: number, annotation: Annotation) {
    return annotation.id;
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
