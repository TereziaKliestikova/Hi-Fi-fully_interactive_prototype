import { Component } from '@angular/core';
import { AppRoutes } from 'src/app/app-routing.module';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrl: './page-not-found.component.scss',
})
export class PageNotFoundComponent {
  protected readonly AppRoutes = AppRoutes;
}
