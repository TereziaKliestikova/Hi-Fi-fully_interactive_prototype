import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from 'src/environments/environment';
import { isDevMode } from '@angular/core';

export const authConfig: AuthConfig = {
  // Url of the Identity Provider
  issuer: environment.apiUrl,

  // Checks if all URLs start with the issuer URL
  // TODO: Do we really don't want to check this?
  strictDiscoveryDocumentValidation: false,

  responseType: 'code',

  // requireHttps: !isDevMode(),
  requireHttps: false,

  // URL of the SPA to redirect the user to after login
  // TODO is this needed?
  redirectUri: window.location.origin + '/home',

  oidc: true,

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'hipa_fe',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile email offline_access',

  showDebugInformation: isDevMode(),

  tokenEndpoint: environment.apiUrl + '/connect/token',
};
