import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { CustomersPageComponent } from './customers-page.component';
import { CustomerService } from './customer.service';
import { UpsertCustomerOverlayComponent } from './upsert-customer-overlay.component';
import { UpsertCustomerOverlay } from './upsert-customer-overlay';

const declarations = [
  CustomersPageComponent,
  UpsertCustomerOverlayComponent
];

const entryComponents = [
  UpsertCustomerOverlayComponent
];

const providers = [
  CustomerService,
  UpsertCustomerOverlay
];

@NgModule({
  declarations,
  exports: declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    CoreModule,
    SharedModule	
  ],
  providers,
  entryComponents
})
export class CustomersModule { }
