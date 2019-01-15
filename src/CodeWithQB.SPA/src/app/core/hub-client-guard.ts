import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { HubClient } from './hub-client';
import { Observable } from 'rxjs';
import { LoginRedirectService } from './redirect.service';
import { AuthService } from './auth.service';

@Injectable()
export class HubClientGuard implements CanActivate {
  constructor(
    private _authService: AuthService,
    private _hubClient: HubClient,
    private _loginRedirectService: LoginRedirectService,
    private readonly _router: Router
  ) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean | UrlTree> {
    return new Promise((resolve,reject) =>
      this._hubClient.connect().then(() => {
        resolve(true);
      }, () => {
        this._loginRedirectService.lastPath = state.url;

        reject(false);
        
        return this._router.parseUrl(this._loginRedirectService.loginUrl);
      })
    );
  }
}
