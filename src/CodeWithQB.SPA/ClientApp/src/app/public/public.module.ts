import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { HomePageComponent } from './home-page.component';
import { ProductsModule } from '../products/products.module';
import { SignUpPageComponent } from './sign-up-page.component';
import { ShoppingCartsModule } from '../shopping-carts/shopping-carts.module';

const declarations = [
  HomePageComponent,
  SignUpPageComponent
];

const entryComponents = [

];

const providers = [

];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    ShoppingCartsModule,
    CoreModule,
    ProductsModule,
    SharedModule,
    ShoppingCartsModule
  ],
  providers,
})
export class PublicModule { }
