import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { DashboardCardService } from './dashboard-card.service';
import { DashboardCardComponent } from './dashboard-card.component';
import { DashboardCardConfigurationOverlay } from './dashboard-card-configuration-overlay';
import { DashboardCardConfigurationOverlayComponent } from './dashboard-card-configuration-overlay.component';

const declarations = [
  DashboardCardComponent,
  DashboardCardConfigurationOverlayComponent
];

const providers = [
  DashboardCardService,
  DashboardCardConfigurationOverlay
];

const entryComponents = [
  DashboardCardComponent,
  DashboardCardConfigurationOverlayComponent
]

@NgModule({
  declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CoreModule,
    SharedModule	
  ],
  providers,
  entryComponents
})
export class DashboardCardsModule { }
