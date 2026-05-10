import { forwardRef, NgModule, Provider } from '@angular/core';
import { BrowserModule, DomSanitizer } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { OAuthModule } from 'angular-oauth2-oidc';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { LoginComponent } from './auth/login/login.component';
import { RequestPasswordChangeComponent } from './auth/request-change-password/request-password-change.component';
import { LabelComponent } from './ui/basic/label/label.component';
import { ButtonComponent } from './ui/basic/button/button.component';
import { AnchorComponent } from './ui/basic/anchor/anchor.component';
import { TextWithLinkComponent } from './ui/basic/text-with-link/text-with-link.component';
import { FormComponent } from './ui/forms/form/form.component';
import { FormAdminNotificationComponent } from './ui/forms/form-admin-notification/form-admin-notification.component';
import { InputComponent } from './ui/forms/input/input.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { AuthPageLayoutComponent } from './auth/auth-page-layout/auth-page-layout.component';
import { AuthService } from './oauth/auth.service';
import { ChangePasswordComponent } from './auth/change-password/change-password.component';
import { TranslocoRootModule } from './transloco-root.module';
import { ApiModule } from './api/api.module';
import { FormNotificationComponent } from './ui/forms/form-notification/form-notification.component';
import { environment } from '../environments/environment';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NavBarComponent } from './ui/nav-bar/nav-bar.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { OrganCatalogComponent } from './app-layout/organ/organ-catalog/organ-catalog.component';
import { SystemCatalogComponent } from './app-layout/system/system-catalog/system-catalog.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { NgOptimizedImage } from '@angular/common';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SpinnerComponent } from 'src/app/ui/basic/spinner/spinner.component';
import { BreadcrumbComponent } from 'src/app/ui/breadcrumb/breadcrumb.component';
import { IconComponent } from './ui/basic/icon/icon.component';
import { HomePageComponent } from './app-layout/home-page/home-page.component';
import { SampleLevelComponent } from './app-layout/sample/sample-level/sample-level.component';
import { HeaderComponent } from './app-layout/header/header.component';
import { AppLayoutComponent } from 'src/app/app-layout/app-layout.component';
import { CatalogToolBarComponent } from 'src/app/app-layout/catalog/catalog-tool-bar/catalog-tool-bar.component';
import { CatalogGridComponent } from 'src/app/app-layout/catalog/catalog-grid/catalog-grid.component';
import { OrganDetailComponent } from 'src/app/app-layout/organ/organ-detail/organ-detail.component';
import { SystemDetailComponent } from './app-layout/system/system-detail/system-detail.component';
import { SampleImageComponent } from 'src/app/app-layout/organ/sample-image/sample-image.component';
import { SampleCatalogComponent } from './app-layout/sample/sample-catalog/sample-catalog.component';
import { RouteResolverService } from 'src/app/route-resolver.service';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { SampleImageAssistantComponent } from 'src/app/app-layout/organ/sample-image/sample-image-helper/sample-image-assistant.component';
import { PageNotFoundComponent } from 'src/app/ui/basic/page-not-found/page-not-found.component';
import { HasAccessDirective } from 'src/app/directives/has-access.directive';
import { ApiInterceptor } from 'src/app/services/interceptor/ApiInterceptor';
import { ApiRequestConfiguration } from 'src/app/services/interceptor/ApiRequestConfiguration';
import { SampleUploadComponent } from './app-layout/admin/sample-upload/sample-upload.component';
import { DescriptionComponent } from './ui/basic/description/description.component';
import { BasicTableComponent } from './ui/basic/basic-table/basic-table.component';
import { FlagsComponent } from './ui/admin/flags/flags.component';
import { EditFlagsComponent } from './ui/admin/edit-flags/edit-flags.component';
import { DeleteFlagConfirmComponent } from './ui/admin/edit-flags-delete-confirmation/edit-flags-delete-confirmation.component';
import { AddFlagsComponent } from './ui/admin/add-flags/add-flags.component';
import { TagEditorComponent } from './ui/admin/edit-tags/edit-tags.component';
import { CardButtonComponent } from './ui/basic/card-button/card-button.component';
import { AdminHomePageComponent } from './app-layout/admin/home-page/admin-home-page/admin-home-page.component';
import { AdminSampleImagesComponent } from './app-layout/admin/sample-images/admin-sample-images/admin-sample-images.component';
import { UploadProgressComponent } from './ui/basic/upload-progress/upload-progress.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { AddFileModalComponent } from './ui/admin/add-file-modal/add-file-modal.component';
import { ClassworkStudyCategoriesComponent } from './app-layout/admin/classwork/classwork-study-categories/classwork-study-categories.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ClassworkStudyDetailComponent } from './app-layout/admin/classwork/classwork-study-detail/classwork-study-detail.component';
import { FolderViewComponent } from './ui/basic/folder-view/folder-view.component';
import { MatTreeModule } from '@angular/material/tree';
import { FolderChildrenFilesComponent } from './ui/basic/folder-children-files/folder-children-files.component';
import { ConfirmModalComponent } from './ui/admin/modal-actions-confirmation/modal-actions-confirmation.component';
import { ClassworkTableComponent } from './ui/admin/classwork-table/classwork-table.component';
import { ClassworkSampleImageSelectorComponent } from './app-layout/admin/classwork/classwork-sample-images-selector/classwork-sample-images-selector.component';
import { ModalService } from './services/modal.service';
import { ToggleButtonComponent } from 'src/app/ui/basic/toggle/toggle.component';
import { TextEditorComponent } from './ui/basic/text-editor/text-editor.component';

export const API_INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useExisting: forwardRef(() => ApiInterceptor),
  multi: true,
};

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    RequestPasswordChangeComponent,
    ClassworkTableComponent,
    LabelComponent,
    ButtonComponent,
    AnchorComponent,
    TextWithLinkComponent,
    FormComponent,
    FormAdminNotificationComponent,
    InputComponent,
    AuthPageLayoutComponent,
    ChangePasswordComponent,
    FormNotificationComponent,
    NavBarComponent,
    ConfirmModalComponent,
    OrganCatalogComponent,
    SystemCatalogComponent,
    CatalogToolBarComponent,
    CatalogGridComponent,
    OrganDetailComponent,
    SystemDetailComponent,
    SampleImageComponent,
    SpinnerComponent,
    BreadcrumbComponent,
    IconComponent,
    HomePageComponent,
    HeaderComponent,
    AppLayoutComponent,
    SampleImageAssistantComponent,
    PageNotFoundComponent,
    SampleUploadComponent,
    HasAccessDirective,
    DescriptionComponent,
    BasicTableComponent,
    ToggleButtonComponent,
    FlagsComponent,
    EditFlagsComponent,
    DeleteFlagConfirmComponent,
    AddFlagsComponent,
    TagEditorComponent,
    CardButtonComponent,
    AdminHomePageComponent,
    AdminSampleImagesComponent,
    CardButtonComponent,
    AdminSampleImagesComponent,
    SampleLevelComponent,
    SampleCatalogComponent,
    FolderViewComponent,
    ClassworkStudyDetailComponent,
    ClassworkStudyCategoriesComponent,
    AddFileModalComponent,
    FolderChildrenFilesComponent,
    ClassworkSampleImageSelectorComponent,
    TextEditorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatCheckboxModule,
    OAuthModule.forRoot(),
    TranslocoRootModule,
    ApiModule.forRoot({ rootUrl: environment.apiUrl }),
    MatProgressSpinnerModule,
    MatTabsModule,
    MatGridListModule,
    NgOptimizedImage,
    MatProgressBarModule,
    MatToolbarModule,
    MatDividerModule,
    FormsModule,
    MatSelectModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    MatExpansionModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatSidenavModule,
    MatTreeModule,
    UploadProgressComponent,
    CommonModule,
    MatMenuModule,
    MatDialogModule,
  ],
  providers: [
    AuthService,
    ApiRequestConfiguration,
    API_INTERCEPTOR_PROVIDER,
    ApiInterceptor,
    RouteResolverService,
    ModalService,
  ],
  bootstrap: [AppComponent],
  exports: [],
})
export class AppModule {
  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
  ) {
    // Register individual icons
    this.iconRegistry.addSvgIcon(
      'flag',
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'assets/admin/flag-icon.svg'
      )
    );
  }
}
