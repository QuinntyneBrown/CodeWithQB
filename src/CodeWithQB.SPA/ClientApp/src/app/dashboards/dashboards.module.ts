import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { DashboardPageComponent } from './dashboard-page.component';
import { AddDashboardCardOverlay } from './add-dashboard-card-overlay';
import { AddDashboardCardOverlayComponent } from './add-dashboard-card-overlay.component';
import { CardsModule } from '../cards/cards.module';
import { DashboardCardsModule } from '../dashboard-cards/dashboard-cards.module';
import { DashboardService } from './dashboard.service';

const declarations = [
  DashboardPageComponent,
  AddDashboardCardOverlayComponent
];

const entryComponents = [
  AddDashboardCardOverlayComponent
];

const providers = [
  AddDashboardCardOverlay,
  DashboardService
];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CardsModule,
    CoreModule,
    DashboardCardsModule,
    SharedModule	
  ],
  providers,
})
export class DashboardsModule { }
