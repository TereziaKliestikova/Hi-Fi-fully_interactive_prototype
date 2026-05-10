import { Injectable } from '@angular/core';
import { HttpRequest } from '@angular/common/http';

/**
 * Configuration for the performed HTTP requests
 */
@Injectable()
export class ApiRequestConfiguration {
  private nextAuthHeader: string | null = null;
  private nextAuthValue: string | null = null;

  /** Getter for the token string */
  get getToken(): string | null {
    return this.nextAuthValue;
  }

  /** Set to basic authentication */
  basic(token: string): void {
    this.nextAuthHeader = 'Authorization';
    this.nextAuthValue = 'Bearer ' + token;
  }

  /** Set to session key */
  nextAsSession(sessionKey: string): void {
    this.nextAuthHeader = 'Session';
    this.nextAuthValue = sessionKey;
  }

  /** Clear any authentication headers (to be called after logout) */
  clear(): void {
    this.nextAuthHeader = null;
    this.nextAuthValue = null;
  }

  /** Apply the current authorization headers to the given request */
  apply(req: HttpRequest<unknown>): HttpRequest<unknown> {
    if (!this.nextAuthHeader || !this.nextAuthValue) {
      return req;
    }
    const headers = {} as { [name: string]: string };
    headers[this.nextAuthHeader] = this.nextAuthValue;
    // Apply the headers to the request
    return req.clone({
      setHeaders: headers,
    });
  }
}
