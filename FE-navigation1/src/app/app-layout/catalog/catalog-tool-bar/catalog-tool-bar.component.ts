import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SortingOptions } from 'src/types/SortingOptions.enum';

// This component is generic and can be used in any catalog tab
@Component({
  selector: 'app-catalog-tool-bar',
  templateUrl: './catalog-tool-bar.component.html',
  styleUrl: './catalog-tool-bar.component.scss',
})
export class CatalogToolBarComponent<T extends { id: number; name: string }> {
  protected readonly SortingOptions = SortingOptions;

  @Input()
  selectValues!: T[] | null;
  @Input()
  showAsTiles!: boolean;
  @Output()
  showAsTilesChange = new EventEmitter<boolean>();
  @Input()
  textSearch!: string | null;
  @Output()
  textSearchChange = new EventEmitter<string | null>();
  @Input()
  selectedIdFilter!: number | null;
  @Output()
  selectedIdFilterChange = new EventEmitter<number | null>();
  @Input()
  itemSorting!: SortingOptions;
  @Output()
  itemSortingChange = new EventEmitter<SortingOptions>();

  invertShowAsTiles = () => {
    this.showAsTiles = !this.showAsTiles;
    this.showAsTilesChange.emit(this.showAsTiles);
  };

  changeTextSearch = (val: string | null) => {
    this.textSearch = val;
    this.textSearchChange.emit(this.textSearch);
  };

  changeSelectedIdFilter = (val: number | null) => {
    this.selectedIdFilter = val;
    this.selectedIdFilterChange.emit(this.selectedIdFilter);
  };

  // for now, we have only two options, so we can just invert the current value
  invertItemSorting = () => {
    this.itemSorting =
      this.itemSorting === SortingOptions.NAME_ASC
        ? SortingOptions.NAME_DESC
        : SortingOptions.NAME_ASC;
    this.itemSortingChange.emit(this.itemSorting);
  };
}
