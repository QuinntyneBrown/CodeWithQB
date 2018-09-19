import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { accessTokenKey, baseUrl, userIdKey, rolesKey, usernameKey } from '../core/constants';
import { HubClient } from '../core/hub-client';
import { LocalStorageService } from '../core/local-storage.service';
import { Logger } from './logger.service';
import { Observable } from 'rxjs';

@Injectable()
export class AuthService {
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _httpClient: HttpClient,
    private _hubClient: HubClient,
    private _localStorageService: LocalStorageService,
    private _loggerService: Logger
  ) {}

  public logout() {
    this._hubClient.disconnect();
    this._localStorageService.clear();
  }

  public tryToLogin(options: { username: string; password: string }) {
    this._loggerService.trace('AuthService', 'tryToLogin');

    return this._httpClient.post<{
      username: string,
      userId: string,
      roles: string[],
      accessToken: string
    }>(`${this._baseUrl}api/users/token`, options).pipe(
      map(response => {
        this._localStorageService.put({ name: accessTokenKey, value: response.accessToken });
        this._localStorageService.put({ name: usernameKey, value: response.username });
        this._localStorageService.put({ name: rolesKey, value: response.roles });
        return response.accessToken;
      })
    );
  }
}
