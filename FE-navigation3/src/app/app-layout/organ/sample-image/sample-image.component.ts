import {
  Component,
  OnInit,
  ElementRef,
  EventEmitter,
  ViewChild,
  Inject,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssetsService } from 'src/app/api/services/assets.service';
import * as OpenSeadragon from 'openseadragon';
import { SampleImageDto } from 'src/app/api/models/sample-image-dto';
import { SampleImageMetadataDto } from 'src/app/api/models/sample-image-metadata-dto';
import { environment } from 'src/environments/environment';
import { Subject } from 'rxjs';
import { ApiRequestConfiguration } from 'src/app/services/interceptor/ApiRequestConfiguration';
import { Annotation } from 'src/types/Annotation';

@Component({
  selector: 'app-sample-image',
  templateUrl: './sample-image.component.html',
  styleUrl: './sample-image.component.scss',
})
export class SampleImageComponent implements OnInit {
  private viewer!: OpenSeadragon.Viewer;
  sampleImage: SampleImageDto | undefined;
  sampleImageMetadata: SampleImageMetadataDto | undefined;
  sampleImageLoaded = new EventEmitter<boolean>();
  assistantVisibilityToggle: Subject<void> = new Subject<void>();
  sampleSizeReference = 0;
  referenceSizeBarWidth = 0;

  referenceSizeBarbreakpoints = [
    5000, 3000, 2000, 1000, 800, 700, 500, 250, 200, 100, 50, 20, 10, 5, 2, 1,
  ];
  refrenceBreakpointIndex = 0;

  @ViewChild('osdContainer') osdContainer: ElementRef | undefined;

  constructor(
    @Inject(ActivatedRoute) private route: ActivatedRoute,
    private assetsService: AssetsService,
    private apiRequestConfiguration: ApiRequestConfiguration
  ) {}

  currentZoomLevel = 1;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const sampleId: number = params['sampleId'];
      this.fetchSampleData(sampleId);
    });

    this.sampleImageLoaded.subscribe(() => {
      this.initializeOpenSeadragon();
    });
  }

  private fetchSampleData(sampleId: number): void {
    this.assetsService
      .assetsSampleImageSampleIdGet$Json({ sampleId: sampleId })
      .subscribe(data => {
        this.sampleImage = data;
        this.fetchMetadataAndInitializeViewer(this.sampleImage.id);
      });
  }

  private fetchMetadataAndInitializeViewer(id: number): void {
    this.assetsService
      .assetsSampleImageMetadataSampleIdGet$Json$Response({ sampleId: id })
      .subscribe(data => {
        this.sampleImageMetadata = data.body;
        this.sampleImageLoaded.emit(true);
      });
  }

  private initializeOpenSeadragon(): void {
    const container = this.osdContainer!.nativeElement;

    this.viewer = new OpenSeadragon.Viewer({
      element: container,
      // debugMode: true,
      showNavigationControl: false,

      showNavigator: true,
      navigatorId: 'navigatorDiv',
      navigatorDisplayRegionColor: '#f9df57',

      visibilityRatio: 1,
      maxZoomPixelRatio: 20,

      preserveImageSizeOnResize: true,
      blendTime: 0.1,
      animationTime: 0.5,
      gestureSettingsMouse: {
        clickToZoom: false,
      },

      ajaxHeaders: {
        Authorization: this.apiRequestConfiguration.getToken!,
      },
      loadTilesWithAjax: true,

      tileSources:
        environment.apiUrl +
        '/wsi/' +
        this.sampleImage?.path.replace(/^\/media\/wsi\//, ''),
    });

    // This is necessary, because Types of OSD actually doesn't have navigator.loadTilesWithAjax parameter
    // @ts-expect-error-error
    this.viewer.navigator.loadTilesWithAjax = true;
    // This is necessary, because Types of OSD actually doesn't have setAjaxHeaders method
    // @ts-expect-error-error
    this.viewer.navigator.setAjaxHeaders({
      Authorization: this.apiRequestConfiguration.getToken!,
    });

    this.viewer.addHandler('zoom', e => {
      this.updateSizeReferenceBar(e.zoom);
    });

    this.viewer.addHandler('open', () => {
      this.viewer.viewport.goHome();

      const zoomFactor = 1;
      this.viewer.viewport.zoomBy(zoomFactor);

      // Add and apply border overlay to the image
      const imageBounds = this.viewer.world.getItemAt(0).getBounds();
      const borderOverlay = document.createElement('div');
      this.applyStyles(borderOverlay, this.getBorderOverlayStyle());
      this.viewer.addOverlay({
        element: borderOverlay,
        location: imageBounds,
      });
    });
  }

  private updateSizeReferenceBar(zoom: number): void {
    const viewportWithZoom =
      this.sampleImageMetadata!.size!.width!.pixel! / zoom;
    const containerWidth = this.viewer.container.getBoundingClientRect().width;

    const barSize =
      this.referenceSizeBarbreakpoints[this.refrenceBreakpointIndex] /
      this.sampleImageMetadata!.pixel!.width!.micro! /
      (viewportWithZoom / containerWidth);

    if (barSize > 125) {
      if (
        this.refrenceBreakpointIndex + 1 <
        this.referenceSizeBarbreakpoints.length
      ) {
        this.refrenceBreakpointIndex++;
      }
    } else if (barSize < 50) {
      if (this.refrenceBreakpointIndex > 0) {
        this.refrenceBreakpointIndex--;
      }
    }
    this.sampleSizeReference =
      this.referenceSizeBarbreakpoints[this.refrenceBreakpointIndex];
    this.referenceSizeBarWidth =
      this.referenceSizeBarbreakpoints[this.refrenceBreakpointIndex] /
      this.sampleImageMetadata!.pixel!.width!.micro! /
      (viewportWithZoom / containerWidth);
  }

  protected showAnnotation(annotation: Annotation): void {
    if (!annotation.visible) {
      this.viewer.removeOverlay(annotation.id.toString());
      return;
    }

    // Create an overlay element (e.g., a div) and apply the class
    const overlay = document.createElement('div');
    overlay.id = annotation.id.toString();
    this.applyStyles(overlay, this.getOverlayStyle());

    // Create a label for the annotation name
    const nameLabel = document.createElement('div');
    // set classname fir later modification (because textContext does not behave reactively)
    nameLabel.className = `annotation-label-${annotation.id}`;
    nameLabel.textContent = annotation.displayedName;
    this.applyStyles(nameLabel, this.getNameLabelStyle());

    // Add the name label to the overlay
    overlay.appendChild(nameLabel);

    // Add the overlay to the OSD viewer
    this.viewer.addOverlay({
      element: overlay,
      location: this.viewer.viewport.imageToViewportRectangle(
        annotation.coords.minX,
        annotation.coords.minY,
        annotation.coords.maxX - annotation.coords.minX,
        annotation.coords.maxY - annotation.coords.minY
      ),
    });
  }

  updateAnnotationLabel(annotation: Annotation): void {
    const overlayElement = document.getElementById(annotation.id.toString());
    if (overlayElement) {
      const nameLabel = overlayElement.querySelector(
        `.annotation-label-${annotation.id}`
      );
      // if the nameLabel is found, update label name (textContent)
      if (nameLabel) {
        nameLabel.textContent = annotation.displayedName;
      }
    }
  }

  protected toggleMinimize(): void {
    console.warn('Minimalization is not implemented yet!');
  }

  protected toggleAssistant(): void {
    this.assistantVisibilityToggle.next();
  }

  // Applying the styles to the elements
  private applyStyles(
    element: HTMLElement,
    styleObject: Partial<CSSStyleDeclaration>
  ) {
    Object.assign(element.style, styleObject);
  }

  // There are methods to get the styles for the borderOverlay, overlay and label.
  // This is because apply classes didn't work properly in CSS
  private getBorderOverlayStyle() {
    return {
      border: '1px solid #0891B2',
      borderBottom: 'none',
      boxSizing: 'border-box',
      boxShadow: '0 4px 4px -1px rgba(8, 145, 178, 0.3)',
    };
  }

  private getOverlayStyle() {
    return {
      border: '4px solid #0891B2',
      position: 'relative',
      boxSizing: 'border-box',
    };
  }

  private getNameLabelStyle() {
    return {
      position: 'absolute',
      top: '-22px',
      right: '-4px',
      color: '#FFF',
      backgroundColor: '#0891B2',
      width: '99px',
      height: '22px',
      fontWeight: '400',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'flex-start',
      borderRadius: '2px 3px 0 0',
      zIndex: '1',
      userSelect: 'none',
      paddingLeft: '5px',
    };
  }
}
