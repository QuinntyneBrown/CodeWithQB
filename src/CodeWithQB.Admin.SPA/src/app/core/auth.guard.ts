import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { accessTokenKey } from '../core/constants';
import { LocalStorageService } from '../core/local-storage.service';
import { LoginRedirectService } from './redirect.service';
import * as jwtDecode from "jwt-decode";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _loginRedirectService: LoginRedirectService,
    private readonly _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    const token = this._localStorageService.get({ name: accessTokenKey });

    if (this.isTokenValid(token)) {      
      return true
    }

    this._loginRedirectService.lastPath = state.url;

    return this._router.parseUrl(this._loginRedirectService.loginUrl);
    
  }

  public isTokenValid(token:string) {
        
    var current_time = new Date().getTime() / 1000;

    return token && (<any>jwtDecode(token)).exp > current_time;
  }  
}
