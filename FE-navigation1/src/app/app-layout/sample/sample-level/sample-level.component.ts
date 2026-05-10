import { Component } from '@angular/core';
import { TranslocoService } from '@ngneat/transloco';
import { TileConfig } from 'src/types/HomeConfig';
import { AppRoutes } from 'src/app/app-routing.module';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sample-level',
  templateUrl: './sample-level.component.html',
  styleUrl: './sample-level.component.scss',
})
export class SampleLevelComponent {
  constructor(private transloco: TranslocoService) {}

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`samples.${key}`);

  sampleCatalogConfig: TileConfig[] = [
    {
      path: AppRoutes.app.allSampleImages,
      title: this.translate('tiles.allSamples.title'),
      description: this.translate('tiles.allSamples.description'),
      imageUrl: 'assets/home/samples.svg',
      alt: 'image of an samples',
    },
    {
      path: AppRoutes.app.organCatalog,
      title: this.translate('tiles.organ.title'),
      description: this.translate('tiles.organ.description'),
      imageUrl: 'assets/home/organ.svg',
      alt: 'image of an organ',
    },
    {
      path: AppRoutes.app.systemCatalog,
      title: this.translate('tiles.system.title'),
      description: this.translate('tiles.system.description'),
      imageUrl: 'assets/home/system.svg',
      alt: 'image of a system',
      heightOverride: '18em',
      marginTopOverride: '3.5em',
    },
  ];
}
