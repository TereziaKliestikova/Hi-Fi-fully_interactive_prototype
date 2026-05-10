import { ActivatedRouteSnapshot, ResolveFn } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class StudyNameResolverService {
  public static resolveStudyName: ResolveFn<string> = (
    route: ActivatedRouteSnapshot
  ) => {
    return route.paramMap.get('studyName')!;
  };
}
