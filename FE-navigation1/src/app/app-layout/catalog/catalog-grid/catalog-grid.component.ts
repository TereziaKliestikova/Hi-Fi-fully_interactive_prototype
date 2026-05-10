import { Component, Input, OnInit } from '@angular/core';
import { AppRoutes, routeParamsFiller } from 'src/app/app-routing.module';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Subject, takeUntil } from 'rxjs';

// This component is generic and can be used in any catalog tab
@Component({
  selector: 'app-catalog-grid',
  templateUrl: './catalog-grid.component.html',
  styleUrl: './catalog-grid.component.scss',
})
export class CatalogGridComponent<T extends { id: number; name: string }>
  implements OnInit
{
  @Input()
  showAsTiles!: boolean;
  @Input()
  itemsData!: T[] | null;
  @Input()
  toRoute!: string;
  @Input()
  idLabel!: string;
  @Input()
  staticIconEndpoint!: string;

  cols = 6;

  private destroy$ = new Subject<void>();

  constructor(private breakpointObserver: BreakpointObserver) {}

  ngOnInit() {
    this.breakpointObserver
      .observe([
        Breakpoints.XSmall,
        Breakpoints.Small,
        Breakpoints.Medium,
        Breakpoints.Large,
        Breakpoints.XLarge,
      ])
      .pipe(takeUntil(this.destroy$))
      .subscribe(result => {
        if (result.breakpoints[Breakpoints.XSmall]) {
          this.cols = 2; // Mobile phones
        } else if (result.breakpoints[Breakpoints.Small]) {
          this.cols = 3; // Tablets
        } else if (result.breakpoints[Breakpoints.Medium]) {
          this.cols = 4; // Small desktops
        } else if (result.breakpoints[Breakpoints.Large]) {
          this.cols = 6; // Medium desktops
        } else {
          this.cols = 6; // Large desktops
        }
      });
  }

  navigate = (item: T) => {
    return routeParamsFiller(this.toRoute, { [this.idLabel]: item.id });
  };

  protected readonly AppRoutes = AppRoutes;
}
