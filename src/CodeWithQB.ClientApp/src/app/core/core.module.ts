import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { OverlayRefProvider } from './overlay-ref-provider';
import { OverlayRefWrapper } from './overlay-ref-wrapper';
import { LoginRedirectService } from './redirect.service';
import { LocalStorageService } from './local-storage.service';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { HubClientGuard } from './hub-client-guard';
import { HubClient } from './hub-client';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HeaderInterceptor } from './headers.interceptor';

const declarations: any[] = [

];

const entryComponents: any[] = [

];

const providers: any[] = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HeaderInterceptor,
    multi: true
  },
  AuthService,
  OverlayRefProvider,
  OverlayRefWrapper,
  LoginRedirectService,
  LocalStorageService,

  AuthGuard,
  HubClient,
  HubClientGuard
];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule	
  ],
  providers
})
export class CoreModule { }
