import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { baseUrl } from './core/constants';
import { CustomersModule } from './customers/customers.module';
import { IdentityModule } from './identity/identity.module';
import { ContactRequestsModule } from './contact-requests/contact-requests.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    IdentityModule,
    CustomersModule,
    ContactRequestsModule
  ],
  providers: [{
    provide: baseUrl,
    useValue:"https://codewithqb.azurewebsites.net/"
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
