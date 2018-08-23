import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { MasterPageComponent } from './master-page.component';
import { AppRoutingModule } from './app-routing.module';
import { UsersModule } from './users/users.module';
import { baseUrl } from './core/constants';
import { CardsModule } from './cards/cards.module';
import { DashboardCardsModule } from './dashboard-cards/dashboard-cards.module';
import { DashboardsModule } from './dashboards/dashboards.module';
import { EventsModule } from './events/events.module';


@NgModule({
  declarations: [
    AppComponent,
    AnonymousMasterPageComponent,
    MasterPageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    AppRoutingModule,

    CardsModule,
    CoreModule,
    DashboardCardsModule,
    DashboardsModule,
    EventsModule,
    SharedModule,
    UsersModule
  ],
  providers: [
    { provide: baseUrl, useValue: 'http://localhost:51578/' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
