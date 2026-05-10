import { StaticEndpoints } from 'src/types/StaticEndpoints.enum';
import { Component, OnInit, AfterViewChecked, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import {
  BodySystemDescriptionDto,
  PdfFile,
  SampleImageDiagnosisDto,
} from 'src/app/api/models';
import { AssetsService } from 'src/app/api/services';
import { AuthService } from 'src/app/oauth/auth.service';

@Component({
  selector: 'app-system-detail',
  templateUrl: './system-detail.component.html',
  styleUrl: './system-detail.component.scss',
})
export class SystemDetailComponent implements OnInit, AfterViewChecked {
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<SampleImageDiagnosisDto>();
  systemDescription!: BodySystemDescriptionDto;
  staticIconEndpoint = StaticEndpoints.BodySystemIcons;
  systemPdf?: PdfFile;
  showDiagnosis = true;
  displayedColumns: string[] = [
    'isFavorite',
    'name',
    'hasAnnotation',
    'caustryName',
    'keyWords',
    'note',
  ];

  constructor(
    private route: ActivatedRoute,
    private assetsService: AssetsService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const systemId: number = +params['systemId'];
      this.fetchBodySystemData(systemId);
    });
  }

  ngAfterViewChecked() {
    this.dataSource.sort = this.sort;
  }

  private fetchBodySystemData(systemId: number): void {
    const userToken = this.authService.getAccessToken();
    const userId = this.authService.parseJwt(userToken).sub;

    if (userId != null) {
      this.assetsService
        .assetsBodySystemDetailIdGet$Json({ id: systemId })
        .subscribe(data => {
          this.systemDescription = data.bodySystemDescription;
          //this.systemPdf = data.organPdf;
          this.dataSource.data = data.sampleImages;
        });
    }
  }
}
