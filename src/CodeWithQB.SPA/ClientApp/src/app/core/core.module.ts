import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { CanDeactivateComponentGuard } from './can-deactivate-component.guard';
import { ErrorService } from './error.service';
import { HeaderInterceptor } from './headers.interceptor';
import { HubClient } from './hub-client';
import { HubClientGuard } from './hub-client-guard';
import { LaunchSettings } from './launch-settings';
import { LocalStorageService } from './local-storage.service';
import { Logger } from './logger.service';
import { OverlayRefProvider } from './overlay-ref-provider';
import { LoginRedirectService } from './redirect.service';
import { UnauthorizedResponseInterceptor } from './unauthorized-response.interceptor';

const providers = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HeaderInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: UnauthorizedResponseInterceptor,
    multi: true
  },
  AuthGuard,
  AuthService,
  CanDeactivateComponentGuard,
  ErrorService,
  HubClient,
  HubClientGuard,
  LaunchSettings,
  LocalStorageService,
  LoginRedirectService,
  Logger,
  OverlayRefProvider
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers,
  exports: []
})
export class CoreModule {}
