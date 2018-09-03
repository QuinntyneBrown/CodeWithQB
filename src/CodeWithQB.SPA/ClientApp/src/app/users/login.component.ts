import { Component, Input, Renderer, ElementRef, HostListener } from '@angular/core';
import { Subject } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, tap, map } from 'rxjs/operators';
import { MatSnackBarRef, SimpleSnackBar } from '@angular/material';
import { AuthService } from '../core/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { LoginRedirectService } from '../core/redirect.service';
import { ErrorService } from '../core/error.service';
import { LocalStorageService } from '../core/local-storage.service';
import { rolesKey } from '../core/constants';
import { Router } from '@angular/router';


@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  selector: 'app-login'
})
export class LoginComponent {
  constructor(
    private _authService: AuthService,
    private _elementRef: ElementRef,
    private _errorService: ErrorService,
    private _localStorageService: LocalStorageService,
    private _loginRedirectService: LoginRedirectService,
    private _renderer: Renderer,
    private _router: Router
  ) {}

  ngOnInit() {
    this._authService.logout();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngAfterContentInit() {
    this._renderer.invokeElementMethod(this.usernameNativeElement, 'focus', []);
  }

  public username: string;

  public password: string;

  private _snackBarRef: MatSnackBarRef<SimpleSnackBar>;

  public form = new FormGroup({
    username: new FormControl(this.username, [Validators.required]),
    password: new FormControl(this.password, [Validators.required])
  });

  public get usernameNativeElement(): HTMLElement {
    return this._elementRef.nativeElement.querySelector('#username');
  }

  @HostListener('window:click')
  public dismissSnackBar() {
    if (this._snackBarRef) this._snackBarRef.dismiss();
  }

  public tryToLogin($event) {
    this._authService
      .tryToLogin({
        username: $event.value.username,
        password: $event.value.password
      })
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
      () => {
        
        if (this._localStorageService.get({ name: rolesKey }).indexOf("Mentee") > -1) {
          this._router.navigateByUrl("/");
        } else {
          this._loginRedirectService.redirectPreLogin();
        }        
      },
        errorResponse => this.handleErrorResponse(errorResponse)
      );
  }

  public handleErrorResponse(errorResponse) {
    this._snackBarRef = this._errorService.handle(errorResponse, 'Login Failed');
  }

  ngOnDestroy() {
    this.onDestroy.next();
  }
}
