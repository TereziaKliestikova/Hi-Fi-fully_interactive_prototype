import { AfterViewChecked, Component, OnInit, ViewChild } from '@angular/core';
import { SampleImageDiagnosisDto } from 'src/app/api/models';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetsService } from 'src/app/api/services';
import { AuthService } from 'src/app/oauth/auth.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-sample-catalog',
  templateUrl: './sample-catalog.component.html',
  styleUrl: './sample-catalog.component.scss',
})
export class SampleCatalogComponent implements OnInit, AfterViewChecked {
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<SampleImageDiagnosisDto>();
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
    this.fetchSampleImages();
  }

  ngAfterViewChecked() {
    this.dataSource.sort = this.sort;
  }

  private fetchSampleImages(): void {
    const userToken = this.authService.getAccessToken();
    const userId = this.authService.parseJwt(userToken).sub;

    if (userId != null) {
      this.assetsService
        .assetsSamplesImagesAllGet$Json()
        .subscribe((data: SampleImageDiagnosisDto[]) => {
          this.dataSource.data = data;
        });
    }
  }
}
