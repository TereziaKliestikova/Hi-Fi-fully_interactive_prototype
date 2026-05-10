import { RbacRoles } from 'src/types/RbacRoles.enum';
import { AuthService } from 'src/app/oauth/auth.service';
import { Component } from '@angular/core';
import { TranslocoService } from '@ngneat/transloco';
import { TileConfig } from 'src/types/HomeConfig';
import { Observable } from 'rxjs';
import { AppRoutes } from 'src/app/app-routing.module';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
})
export class HomePageComponent {
  constructor(
    private transloco: TranslocoService,
    protected authServices: AuthService,
    private modalService: ModalService
  ) {}

  openUploadModal() {
    this.modalService.open();
  }

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`home.${key}`);

  private translate2 = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`samples.${key}`);

  protected getGridConfig = () => {
    return this.authServices.getRole() == RbacRoles.Admin
      ? this.adminGridConfig
      : this.gridConfig;
  };
  protected getTileConfig = () => {
    return this.authServices.getRole() == RbacRoles.Admin
      ? this.homeAdminConfig
      : this.homeConfig;
  };
  protected isAdmin = () => {
    return this.authServices.getRole() == RbacRoles.Admin;
  };

  adminGridConfig = {
    cols: 4,
    rowHeight: '500px',
    gutterSize: '40px',
    style: { margin: '0px 10px 0px 10px' },
  };

  gridConfig = {
    cols: 3,
    rowHeight: '500px',
    gutterSize: '100px',
    style: { margin: '0px 100px 0px 100px' },
  };

  homeConfig: TileConfig[] = [
    {
      path: AppRoutes.app.organCatalog,
      title: this.translate2('tiles.organ.title'),
      description: this.translate2('tiles.organ.description'),
      imageUrl: 'assets/home/organ.svg',
      alt: 'image of an organ',
    },
    {
      path: AppRoutes.app.systemCatalog,
      title: this.translate2('tiles.system.title'),
      description: this.translate2('tiles.system.description'),
      imageUrl: 'assets/home/system.svg',
      alt: 'image of a system',
      heightOverride: '18em',
      marginTopOverride: '3.5em',
    },
    {
      path: AppRoutes.app.classworkStudyCategories, // TODO change this when you implement this page
      title: this.translate('tiles.classwork.title'),
      description: this.translate('tiles.classwork.description'),
      imageUrl: 'assets/home/classwork.svg',
      alt: 'stacked books',
    },
    {
      path: '/PLACEHOLDER', // TODO change this when you implement this page
      title: this.translate('tiles.tasks.title'),
      description: this.translate('tiles.tasks.description'),
      imageUrl: 'assets/home/tasks.svg',
      alt: 'taks list',
    },
  ];

  homeAdminConfig: TileConfig[] = [
    {
      path: AppRoutes.app.sampleImages,
      title: this.translate('tiles.samples.title'),
      description: this.translate('tiles.samples.description'),
      imageUrl: 'assets/home/samples.svg',
      alt: 'samples',
    },
    {
      path: AppRoutes.app.adminClassworkStudyCategories,
      title: this.translate('tiles.classwork.title'),
      description: this.translate('tiles.classwork.description'),
      imageUrl: 'assets/home/learning.svg',
      alt: 'classwork',
    },
    {
      path: '/PLACEHOLDER',
      title: this.translate('tiles.tasks.title'),
      description: this.translate('tiles.tasks.admindescription'),
      imageUrl: 'assets/home/tasks.svg',
      alt: 'stacked books',
    },
    {
      path: '/PLACEHOLDER',
      title: this.translate('tiles.materials.title'),
      description: this.translate('tiles.materials.description'),
      imageUrl: 'assets/home/classwork.svg',
      alt: 'taks list',
    },
  ];
  protected readonly AppRoutes = AppRoutes;
}
