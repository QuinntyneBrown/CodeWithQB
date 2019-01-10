import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map, tap } from 'rxjs/operators';
import { accessTokenKey, baseUrl, currentUserNameKey } from './constants';
import { HubClient } from './hub-client';
import { LocalStorageService } from './local-storage.service';

@Injectable()
export class AuthService {
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _httpClient: HttpClient,    
    private _localStorageService: LocalStorageService
  ) {}

  public tryToLogin(options: { username: string; password: string }) {
    return this._httpClient.post<any>(`${this._baseUrl}api/users/token`, options).pipe(
      map(response => {
        this._localStorageService.put({ name: accessTokenKey, value: response.accessToken });
        this._localStorageService.put({ name: currentUserNameKey, value: options.username });
        return response.accessToken;
      })
    );
  }
}
