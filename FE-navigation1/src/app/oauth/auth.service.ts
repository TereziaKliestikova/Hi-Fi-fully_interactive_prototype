import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from './auth-config';
import { ApiRequestConfiguration } from 'src/app/services/interceptor/ApiRequestConfiguration';
import { HipaJwt } from 'src/types/HipaJwt.type';
import { RbacRoles } from 'src/types/RbacRoles.enum';

@Injectable({
  providedIn: 'root',
})
class HipaOAuthService extends OAuthService {
  public init(): void {
    this.configure(authConfig);
    this.setStorage(localStorage);
  }
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private oAuthService: HipaOAuthService,
    private apiRequestConfiguration: ApiRequestConfiguration
  ) {
    this.oAuthService.init();
    // Automatically refresh the token 75% before it expires
    this.oAuthService.setupAutomaticSilentRefresh();
    this.isAuthenticated() &&
      this.apiRequestConfiguration.basic(this.getAccessToken());
  }

  public fetchToken = async (
    email: string,
    password: string
  ): Promise<void> => {
    const tokenRes = await this.oAuthService.fetchTokenUsingPasswordFlow(
      email,
      password
    );
    this.apiRequestConfiguration.basic(tokenRes.access_token);
  };

  public logout = (): void => {
    this.oAuthService.logOut();
    this.apiRequestConfiguration.clear();
  };

  public getRefreshToken = (): string => {
    const token = this.oAuthService.getRefreshToken();
    if (!token) throw new Error('Refresh token not present');
    return token;
  };

  public getAccessToken = (): string => {
    const token = this.oAuthService.getAccessToken();
    if (!token) throw new Error('Access token not present');
    return token;
  };

  public isAuthenticated = (): boolean => {
    const isAuthenticated = this.oAuthService.hasValidAccessToken();
    // For situation when token expires and user has it interceptor,
    // it is not necessary to clear the token but more of a cleanup
    isAuthenticated || this.apiRequestConfiguration.clear();
    return isAuthenticated;
  };

  public parseJwt = (t: string): HipaJwt => {
    // Might throw an error if token is not valid,
    //  but this is inconsistent state where someone is tampering with the token
    //  or there is a bug in the system, either way we want to know about it
    return JSON.parse(atob(t.split('.')[1])) as HipaJwt;
  };

  public getRole = (): RbacRoles => {
    const token = this.getAccessToken();
    const hipaJWT = JSON.parse(atob(token.split('.')[1])) as HipaJwt;
    return hipaJWT.user_role;
  };
}
