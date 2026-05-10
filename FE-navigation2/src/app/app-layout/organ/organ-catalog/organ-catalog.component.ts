import { Component, OnInit } from '@angular/core';
import { SortingOptions } from 'src/types/SortingOptions.enum';
import { AssetsService } from 'src/app/api/services';
import { OrganDiagnosesDto, Diagnosis } from 'src/app/api/models';
import { AppRoutes } from 'src/app/app-routing.module';
import { StaticEndpoints } from 'src/types/StaticEndpoints.enum';

@Component({
  selector: 'app-organ-catalog',
  templateUrl: './organ-catalog.component.html',
  styleUrls: ['./organ-catalog.component.scss'],
})
export class OrganCatalogComponent implements OnInit {
  allOrgans: OrganDiagnosesDto[] | null = null;
  diagnoses: Diagnosis[] | null = null;
  showAsTiles = true;
  organNameSearch: string | null = null;
  diagnosisFilter: number | null = null;
  itemSorting: SortingOptions = SortingOptions.NAME_ASC;
  toRoute = AppRoutes.app.organDetail;
  idLabel = 'organId';
  staticIconEndpoint = StaticEndpoints.OrganIcons;

  constructor(private assetService: AssetsService) {}

  ngOnInit(): void {
    this.fetchOrgans();
    this.fetchDiagnoses();
  }

  fetchDiagnoses = () => {
    this.assetService.assetsDiagnosesListGet$Json().subscribe(data => {
      this.diagnoses = data.diagnoses;
    });
  };

  fetchOrgans = () => {
    this.assetService.assetsOrganTileListGet$Json().subscribe(data => {
      this.allOrgans = data.organs;
    });
  };

  filteredOrgans = (): OrganDiagnosesDto[] | null => {
    if (!this.allOrgans) {
      return null;
    }

    if (!this.organNameSearch && !this.diagnosisFilter) {
      return this.sortedOrgans();
    }

    return this.sortedOrgans().filter(organ => {
      const nameMatch =
        !this.organNameSearch ||
        this.normalizeText(organ.name).includes(
          this.normalizeText(this.organNameSearch)
        );

      const diagnosisMatch =
        !this.diagnosisFilter ||
        (organ.diagnoses?.some(
          diagnosis => diagnosis.id === this.diagnosisFilter
        ) ??
          false);

      return nameMatch && diagnosisMatch;
    });
  };

  sortedOrgans = (): OrganDiagnosesDto[] => {
    if (!this.allOrgans) return [];

    const sorted = this.allOrgans.slice();

    return sorted.sort((first, second) =>
      this.itemSorting === SortingOptions.NAME_ASC
        ? this.normalizeText(first.name).localeCompare(
            this.normalizeText(second.name)
          )
        : this.normalizeText(second.name).localeCompare(
            this.normalizeText(first.name)
          )
    );
  };

  private normalizeText(text: string): string {
    return text
      ?.normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toLowerCase();
  }
}
