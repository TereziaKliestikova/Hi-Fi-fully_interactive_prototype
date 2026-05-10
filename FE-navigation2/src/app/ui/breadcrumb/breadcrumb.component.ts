import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Observable, of, switchMap } from 'rxjs';
import { map } from 'rxjs/operators';

// This component can be upgraded to use the breadcrumb service
// example here: https://marco.dev/angular-breadcrumb
@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrl: './breadcrumb.component.scss',
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs: Breadcrumb[] = [];

  private translate = (key: string): Observable<string> =>
    this.transloco.selectTranslate(`route.${key}`);

  constructor(
    private activatedRoute: ActivatedRoute,
    private transloco: TranslocoService
  ) {}

  ngOnInit() {
    this.breadcrumbs = this.buildBreadCrumbs(this.activatedRoute.root);
  }

  private buildBreadCrumbs(
    activatedRoute: ActivatedRoute,
    url: string = '',
    breadcrumbs: Breadcrumb[] = []
  ): Breadcrumb[] {
    const children = activatedRoute.children;
    if (children.length === 0) {
      return breadcrumbs;
    }

    const child = children[0];
    const currentRawPart =
      child.routeConfig && child.routeConfig.path ? child.routeConfig.path : '';
    const isDynamic = currentRawPart.includes(':');

    // console.log(currentRawPart);

    // if one app route contains multiple slashes we need to treat it as a single route
    const routeURL = child.snapshot.url.map(segment => segment.path).join('/');
    if (routeURL !== '') url += `/${routeURL}`;

    const label = isDynamic
      ? this.getDynamicPart(currentRawPart)
      : this.translate(child.snapshot.data['breadcrumbLabelKey']);

    console.log(child.snapshot.data['breadcrumbLabelKey']);

    const icon = child.snapshot.data['icon'] as string | undefined;
    const crumb: Breadcrumb = { label, url, icon };
    // skip duplicate breadcrumbs
    if (!breadcrumbs.find(b => b.url === crumb.url)) {
      breadcrumbs.push(crumb);
    }

    return this.buildBreadCrumbs(child, url, breadcrumbs);
  }

  private getDynamicPart(rawPart: string): Observable<string> {
    const parts = rawPart.split('/');
    const dynamicPart = parts.find(p => p.includes(':'))!;
    const name = dynamicPart.replace(':', '');
    return this.activatedRoute.data.pipe(
      map(params => {
        return params[name!];
      }),
      switchMap(value => {
        if (name === 'studyName') {
          return this.transloco.selectTranslate(`route.${value}`);
        } else {
          return of(value);
        }
      })
    );
  }
}

// declared here to avoid use outside of this file
type Breadcrumb = {
  label: Observable<string>;
  url?: string;
  icon?: string;
};
