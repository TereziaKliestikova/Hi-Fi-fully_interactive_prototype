import { Component, OnInit, AfterViewChecked, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetsService } from 'src/app/api/services';
import {
  OrganDescriptionDto,
  PdfFile,
  SampleImageDiagnosisDto,
} from 'src/app/api/models';
import { StaticEndpoints } from 'src/types/StaticEndpoints.enum';
import { AuthService } from 'src/app/oauth/auth.service';

@Component({
  selector: 'app-organ-detail',
  templateUrl: './organ-detail.component.html',
  styleUrls: ['./organ-detail.component.scss'],
})
export class OrganDetailComponent implements OnInit, AfterViewChecked {
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<SampleImageDiagnosisDto>();
  organDescription!: OrganDescriptionDto;
  staticIconEndpoint = StaticEndpoints.OrganIcons;
  organPdf?: PdfFile;
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
      const organId: number = +params['organId'];
      this.fetchOrganData(organId);
    });
  }

  ngAfterViewChecked() {
    this.dataSource.sort = this.sort;
  }

  private fetchOrganData(organId: number): void {
    const userToken = this.authService.getAccessToken();
    const userId = this.authService.parseJwt(userToken).sub;

    if (userId != null) {
      this.assetsService
        .assetsOrganDetailIdGet$Json({ id: organId })
        .subscribe(data => {
          this.organDescription = data.organDescription;
          this.organPdf = data.organPdf;
          this.dataSource.data = data.sampleImages;
        });
    }
  }
}
