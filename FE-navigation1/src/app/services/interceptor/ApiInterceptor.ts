import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { ApiRequestConfiguration } from 'src/app/services/interceptor/ApiRequestConfiguration';
// import { Router } from '@angular/router';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(
    private apiRequestConfiguration: ApiRequestConfiguration
    // private router: Router
  ) {}

  intercept(
    req: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    // Apply the headers
    req = this.apiRequestConfiguration.apply(req);

    // Also handle errors globally
    return next.handle(req).pipe(
      tap({
        next: x => x,
        error: err => {
          // If is error from the server it has error object with status and title
          if (err.error) {
            // Disabled for now, we do not even have a 404 page (just blank page) and causes issues during development
            console.log('API error deteted: ', err);
            // if (err.error.status === 404) {
            //   this.router.navigate([AppRoutes.notFound]);
            //   return;
            // }
          }

          // Handle this err
          console.error(
            `Unhandled error performing request, status code = ${err.status}`
          );
        },
      })
    );
  }
}
