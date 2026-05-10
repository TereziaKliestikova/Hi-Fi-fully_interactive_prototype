// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { inject, NgModule } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { RequestPasswordChangeComponent } from './auth/request-change-password/request-password-change.component';
import { ChangePasswordComponent } from './auth/change-password/change-password.component';
import { AuthService } from 'src/app/oauth/auth.service';
import { HomePageComponent } from './app-layout/home-page/home-page.component';
import { AppLayoutComponent } from 'src/app/app-layout/app-layout.component';
import { OrganCatalogComponent } from 'src/app/app-layout/organ/organ-catalog/organ-catalog.component';
import { OrganDetailComponent } from 'src/app/app-layout/organ/organ-detail/organ-detail.component';
import { RouteResolverService } from 'src/app/route-resolver.service';
import { SampleImageComponent } from './app-layout/organ/sample-image/sample-image.component';
import { PageNotFoundComponent } from 'src/app/ui/basic/page-not-found/page-not-found.component';
import { SampleUploadComponent } from './app-layout/admin/sample-upload/sample-upload.component';
import { RbacRoles } from 'src/types/RbacRoles.enum';
import { SystemCatalogComponent } from './app-layout/system/system-catalog/system-catalog.component';
import { SystemDetailComponent } from './app-layout/system/system-detail/system-detail.component';
import { SampleLevelComponent } from './app-layout/sample/sample-level/sample-level.component';
import { SampleCatalogComponent } from './app-layout/sample/sample-catalog/sample-catalog.component';
import { AdminSampleImagesComponent } from './app-layout/admin/sample-images/admin-sample-images/admin-sample-images.component';
import { ClassworkStudyCategoriesComponent } from './app-layout/admin/classwork/classwork-study-categories/classwork-study-categories.component';
import { ClassworkStudyDetailComponent } from './app-layout/admin/classwork/classwork-study-detail/classwork-study-detail.component';
import { ClassworkSampleImageSelectorComponent } from './app-layout/admin/classwork/classwork-sample-images-selector/classwork-sample-images-selector.component';
import { StudyNameResolverService } from './services/sudy-name/stud-name-resolver.service';
// Use this in code for easy editing
/*
Can be improved with a custom routing matcher using only object keys
problem with this implementation is, that we can not use it in routes
and has to be manually updated
*/
export const AppRoutes = {
  auth: {
    login: '/auth/login',
    registration: '/auth/registration',
    requestPasswordChange: '/auth/request-password-change',
    changePassword: '/auth/change-password',
  },
  app: {
    home: '/app',
    sampleImagesCatalog: '/app/sample-images',
    allSampleImages: '/app/sample-images/list',
    sampleImage: '/app/sample-images/list/:sampleId',
    organCatalog: '/app/sample-images/organ',
    organDetail: '/app/sample-images/organ/:organId',
    systemCatalog: '/app/sample-images/system',
    systemDetail: '/app/sample-images/system/:systemId',
    sampleOrganImage: '/app/sample-images/organ/:organId/:sampleId',
    sampleSystemImage: '/app/sample-images/system/:systemId/:sampleId',
    addToFavourite: '',
    sampleUpload: '/app/sample-upload',
    sampleImages: '/app/admin/sample-images',
    adminSampleImage: '/app/admin/sample-images/:sampleId',
    classworkStudyCategories: '/app/classwork',
    classworkStudyDetail: '/app/classwork/:studyName',
    adminClassworkStudyCategories: '/app/admin/classwork',
    adminClassworkStudyDetail: '/app/admin/classwork/:studyName',
    adminClassworkSampleImagesSelector:
      '/app/admin/classwork/:studyName/:folderId',
    adminClassworkSampleImage:
      '/app/admin/classwork/:studyName/sample-image/:sampleId',
    classworkSampleImage: '/app/classwork/:studyName/sample-image/:sampleId',
  },
  // it is not registered in routes, as any route not found will redirect to this,
  // if we want to have a custom 404 page, we can add it to routes
  notFound: '404',
};

// Use this for routes with params
export function routeParamsFiller(
  route: string,
  params: Record<string, number> // for now only number, can be union with string if needed
) {
  return Object.keys(params).reduce(
    (prevRoute, paramKey) =>
      prevRoute.replace(`:${paramKey}`, params[paramKey].toString()),
    route
  );
}

const routes: Routes = [
  {
    path: 'auth',
    canActivate: [
      () =>
        !inject(AuthService).isAuthenticated() ||
        inject(Router).navigate([AppRoutes.app.home]),
    ],
    children: [
      {
        path: 'login',
        component: LoginComponent,
        data: { title: 'Login | HIPA' },
      },
      {
        path: 'registration',
        component: RegistrationComponent,
        data: { title: 'Registration | HIPA' },
      },
      {
        path: 'request-password-change',
        component: RequestPasswordChangeComponent,
        data: { title: 'Change Password | HIPA' },
      },
      {
        path: 'change-password',
        component: ChangePasswordComponent,
        data: { title: 'Change Password | HIPA' },
      },
    ],
  },
  {
    path: 'app',
    canActivate: [
      () =>
        inject(AuthService).isAuthenticated() ||
        inject(Router).navigate([AppRoutes.auth.login]),
    ],
    data: {
      breadcrumbLabelKey: 'home',
      icon: 'home',
    },
    component: AppLayoutComponent,
    children: [
      {
        path: '',
        component: HomePageComponent,
        // special case, when route is empty and key is defined in parent
      },
      {
        path: 'sample-images',
        data: {
          breadcrumbLabelKey: 'sampleImage',
          title: 'Snímky | HIPA',
        },
        children: [
          {
            path: '',
            component: SampleLevelComponent,
          },
          {
            path: 'list',
            data: {
              breadcrumbLabelKey: 'all',
              title: 'Snímky | HIPA',
            },
            children: [
              {
                path: '',
                component: SampleCatalogComponent,
                data: {
                  title: 'Snímky | HIPA',
                },
                // bez tejto picoviny routeParamsFiller nenajde route
              },
              {
                path: ':sampleId',
                component: SampleImageComponent,
                data: {
                  breadcrumbLabelKey: 'sampleImage',
                  title: 'Snímky | HIPA',
                },
                resolve: {
                  sampleId: RouteResolverService.sampleImageResolver,
                },
              },
            ],
          },
          {
            path: 'organ',
            data: {
              breadcrumbLabelKey: 'organCatalog',
              title: 'Orgán | HIPA',
            },
            children: [
              {
                path: '',
                component: OrganCatalogComponent,
                // special case, when route is empty and key is defined in parent
              },
              {
                path: ':organId',
                data: {
                  breadcrumbLabelKey: 'organDetail',
                  title: 'Orgán | HIPA',
                },
                resolve: {
                  organId: RouteResolverService.organNameResolver,
                },
                children: [
                  {
                    path: '',
                    component: OrganDetailComponent,
                    data: {
                      title: 'Orgán | HIPA',
                    },
                  },
                  {
                    path: ':sampleId',
                    component: SampleImageComponent,
                    data: {
                      breadcrumbLabelKey: 'sampleImage',
                      title: 'Snímky | HIPA',
                    },
                    resolve: {
                      sampleId: RouteResolverService.sampleImageResolver,
                    },
                  },
                ],
              },
            ],
          },
          {
            path: 'system',
            data: {
              breadcrumbLabelKey: 'systemCatalog',
              title: 'Systém | HIPA',
            },
            children: [
              {
                path: '',
                component: SystemCatalogComponent,
                data: {
                  title: 'Systém | HIPA',
                },
              },
              {
                path: ':systemId',
                data: {
                  breadcrumbLabelKey: 'systemDetail',
                  title: 'Systém | HIPA',
                },
                resolve: {
                  systemId: RouteResolverService.systemNameResolver,
                },
                children: [
                  {
                    path: '',
                    component: SystemDetailComponent,
                    data: { title: 'Systém | HIPA' },
                  },
                  {
                    path: ':sampleId',
                    component: SampleImageComponent,
                    data: {
                      breadcrumbLabelKey: 'sampleImage',
                      title: 'Snímky | HIPA',
                    },
                    resolve: {
                      sampleId: RouteResolverService.sampleImageResolver,
                    },
                  },
                ],
              },
            ],
          },
        ],
      },
      {
        path: 'classwork',
        data: {
          breadcrumbLabelKey: 'classwork',
          title: 'Výučba | HIPA',
        },
        children: [
          {
            path: '',
            component: ClassworkStudyCategoriesComponent,
          },
          {
            path: ':studyName',
            resolve: {
              studyName: StudyNameResolverService.resolveStudyName,
            },
            data: {
              breadcrumbLabelKey: 'studyName',
              title: 'Výučba | HIPA',
            },
            children: [
              {
                path: '',
                component: ClassworkStudyDetailComponent,
                data: {
                  breadcrumbLabelKey: 'studyNameLabel',
                  title: 'Výučba | HIPA',
                },
              },
              {
                path: 'sample-image/:sampleId',
                component: SampleImageComponent,
                data: {
                  breadcrumbLabelKey: 'sampleImage',
                  title: 'Snímky | HIPA',
                },
                resolve: {
                  sampleId: RouteResolverService.sampleImageResolver,
                },
              },
            ],
          },
        ],
      },
      {
        path: 'sample-upload',
        component: SampleUploadComponent,
        canActivate: [
          () =>
            inject(AuthService).getRole() === RbacRoles.Admin ||
            inject(Router).navigate([AppRoutes.app.home]),
        ],
      },
      {
        path: 'admin',
        canActivate: [
          () =>
            inject(AuthService).getRole() === RbacRoles.Admin ||
            inject(Router).navigate([AppRoutes.app.home]),
        ],
        children: [
          {
            path: '',
            component: HomePageComponent,
            data: {
              title: 'Admin | HIPA',
            },
          },
          {
            path: 'sample-images',
            data: {
              breadcrumbLabelKey: 'sampleImage',
              title: 'Snímky | HIPA',
            },
            // component: AdminSampleImagesComponent,
            children: [
              {
                path: '',
                component: AdminSampleImagesComponent, // list view
              },
              {
                path: ':sampleId',
                component: SampleImageComponent,
                resolve: {
                  sampleId: RouteResolverService.sampleImageResolver,
                },
                data: {
                  breadcrumbLabelKey: 'sampleImage',
                  title: 'Snímky | HIPA',
                },
              },
            ],
          },
          {
            path: 'classwork',
            data: {
              breadcrumbLabelKey: 'classwork',
              title: 'Výučba | HIPA',
            },
            children: [
              {
                path: '',
                component: ClassworkStudyCategoriesComponent,
              },
              {
                path: ':studyName',
                resolve: {
                  studyName: StudyNameResolverService.resolveStudyName,
                },
                children: [
                  {
                    path: '',
                    component: ClassworkStudyDetailComponent,
                  },
                  {
                    path: 'sample-image/:sampleId',
                    component: SampleImageComponent,
                    resolve: {
                      sampleId: RouteResolverService.sampleImageResolver,
                    },
                    data: {
                      breadcrumbLabelKey: 'sampleImage',
                      title: 'Snímky | HIPA',
                    },
                  },
                  {
                    path: ':folderId',
                    component: ClassworkSampleImageSelectorComponent,
                  },
                ],
              },
            ],
          },

          // Add another admin pages
        ],
      },
    ],
  },
  {
    path: '',
    pathMatch: 'full',
    // Defaults to login, if authenticated, it will redirect to home
    redirectTo: AppRoutes.auth.login,
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [],
})
export class AppRoutingModule {}
