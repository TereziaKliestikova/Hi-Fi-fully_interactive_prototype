import {
  HttpHeaders,
  HttpContextToken,
  HttpContext,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn } from '@angular/router';
import { AssetsService } from 'src/app/api/services/assets.service';

// Not an angular service, as this class is in static context for route resolvers
// @Injectable({
//   providedIn: 'root',
// })
export class RouteResolverService {
  public static organNameResolver: ResolveFn<string> = (
    route: ActivatedRouteSnapshot
  ) => {
    const id = Number(route.paramMap.get('organId'));
    const userId = String(route.paramMap.get('userId'));

    const headers = new HttpHeaders().set('userId', String(userId));
    const headersToken = new HttpContextToken<HttpHeaders>(
      () => new HttpHeaders()
    );
    const context = new HttpContext().set(headersToken, headers);
    // TODO consider creating endpoint for fetching organ name
    return new Promise(resolve => {
      inject(AssetsService)
        .assetsOrganDetailIdGet$Json({ id: id }, context)
        .subscribe(data => {
          resolve(data.organDescription.name);
        });
    });
  };

  public static systemNameResolver: ResolveFn<string> = (
    route: ActivatedRouteSnapshot
  ) => {
    const id = Number(route.paramMap.get('systemId'));
    // TODO consider creating endpoint for fetching organ name
    return new Promise(resolve => {
      inject(AssetsService)
        .assetsBodySystemDetailIdGet$Json({
          id: id,
        })
        .subscribe(data => {
          resolve(data.bodySystemDescription.name);
        });
    });
  };

  public static sampleImageResolver: ResolveFn<string> = (
    route: ActivatedRouteSnapshot
  ) => {
    const id = Number(route.paramMap.get('sampleId'));
    // TODO consider creating endpoint for fetching sample image name
    return new Promise(resolve => {
      inject(AssetsService)
        .assetsSampleImageSampleIdGet$Json({
          sampleId: id,
        })
        .subscribe(data => {
          resolve(data.name);
        });
    });
  };

  public static studyCategoryResolver: ResolveFn<string> = (
    route: ActivatedRouteSnapshot
  ) => {
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const study = route.paramMap.get('studyName');
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    return new Promise(resolve => {
      //TODO: implement here the data fetching for each study
      return null;
    });
  };
}
