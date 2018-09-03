import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { ProductService } from './product.service';
import { HttpClientModule } from '@angular/common/http';
import { ProductsPageComponent } from './products-page.component';
import { ProductPageComponent } from './product-page.component';

const declarations = [
  ProductsPageComponent,
  ProductPageComponent
];

const entryComponents = [

];

const providers = [
  ProductService
];

@NgModule({
  declarations,
  entryComponents,
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
})
export class ProductsModule { }
