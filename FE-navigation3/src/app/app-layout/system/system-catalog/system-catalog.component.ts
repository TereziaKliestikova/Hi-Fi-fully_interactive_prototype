import { Component, OnInit } from '@angular/core';
import { BodySystemDto, Diagnosis } from 'src/app/api/models';
import { AssetsService } from 'src/app/api/services';
import { SortingOptions } from 'src/types/SortingOptions.enum';
import { AppRoutes } from 'src/app/app-routing.module';
import { StaticEndpoints } from 'src/types/StaticEndpoints.enum';

@Component({
  selector: 'app-system-catalog',
  templateUrl: './system-catalog.component.html',
  styleUrl: './system-catalog.component.scss',
})
export class SystemCatalogComponent implements OnInit {
  allSystems: BodySystemDto[] | null = null;
  diagnoses: Diagnosis[] | null = null;
  showAsTiles = true;
  systemNameSearch: string | null = null;
  diagnosisFilter: number | null = null;
  itemSorting: SortingOptions = SortingOptions.NAME_ASC;
  toRoute = AppRoutes.app.systemDetail;
  idLabel = 'systemId';
  staticIconEndpoint = StaticEndpoints.BodySystemIcons;

  constructor(private assetService: AssetsService) {}

  ngOnInit(): void {
    this.fetchSystems();
    this.fetchDiagnoses();
  }

  fetchDiagnoses = () => {
    this.assetService.assetsDiagnosesListGet$Json().subscribe(data => {
      this.diagnoses = data.diagnoses;
    });
  };

  fetchSystems = () => {
    this.assetService.assetsBodySystemListGet$Json().subscribe(data => {
      this.allSystems = data.bodySystems;
      console.log(this.allSystems);
    });
  };

  filteredSystems = (): BodySystemDto[] | null => {
    console.log(this.systemNameSearch);
    if (!this.allSystems) {
      return null;
    }

    if (!this.systemNameSearch && !this.diagnosisFilter) {
      return this.sortedSystems();
    }

    return this.sortedSystems().filter(o => {
      let nameMatch = true;
      if (this.systemNameSearch) {
        const normalizedSearch = this.normalizeText(this.systemNameSearch);
        const normalizedName = this.normalizeText(o.name);
        nameMatch = normalizedName.includes(normalizedSearch);
      }

      let diagnosisMatch = true;
      if (this.diagnosisFilter) {
        diagnosisMatch =
          o.diagnoses?.some(d => d.id === this.diagnosisFilter) ?? false;
      }

      return nameMatch && diagnosisMatch;
    });
  };

  sortedSystems = (): BodySystemDto[] => {
    if (!this.allSystems) return [];

    const sorted = this.allSystems.slice();

    if (this.itemSorting === SortingOptions.NAME_ASC) {
      return sorted.sort((a, b) =>
        this.normalizeText(a.name).localeCompare(this.normalizeText(b.name))
      );
    } else if (this.itemSorting === SortingOptions.NAME_DESC) {
      return sorted.sort((a, b) =>
        this.normalizeText(b.name).localeCompare(this.normalizeText(a.name))
      );
    }

    throw new Error('Unknown sorting option');
  };

  // search without diakritika
  private normalizeText(text: string): string {
    return text
      ?.normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toLowerCase();
  }
}
