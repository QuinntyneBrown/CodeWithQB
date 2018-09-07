import { Component, HostBinding, ElementRef } from '@angular/core';
import { AuthService } from './core/auth.service';
import { map, tap, takeUntil } from 'rxjs/operators';
import { LoginRedirectService } from './core/redirect.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './master-page.component.html',
  styleUrls: ['./master-page.component.css'],
  selector: 'app-master-page'
})
export class MasterPageComponent {
  constructor(
    private readonly _authService: AuthService,
    private readonly _loginRedirect: LoginRedirectService
  ) { }
 
  public signOut() {
    this._authService.logout();
    this._loginRedirect.redirectToLogin();
  }
  
  public closeErrorConsole() {
    this.isErrorConsoleOpen = false;
  }

  public clearNotifcations() {

  }

  @HostBinding("class.error-console-is-opened")
  public isErrorConsoleOpen:boolean = null;
}
