import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { ContactRequestService } from './contact-request.service';
import { ContactRequestsPageComponent } from './contact-requests-page.component';
import { UpsertContactRequestOverlay } from './upsert-contact-request-overlay';
import { UpsertContactRequestOverlayComponent } from './upsert-contact-request-overlay.component';

const declarations = [
  ContactRequestsPageComponent,
  UpsertContactRequestOverlayComponent
];

const entryComponents = [
  UpsertContactRequestOverlayComponent
];

const providers = [
  ContactRequestService,
  UpsertContactRequestOverlay
];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CoreModule,
    SharedModule	
  ],
  providers,
})
export class ContactRequestsModule { }
