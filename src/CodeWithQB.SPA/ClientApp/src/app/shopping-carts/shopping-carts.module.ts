import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { ShoppingCartService } from './shopping-cart.service';
import { ShoppingCartItemService } from './shopping-cart-item.service';

const declarations = [

];

const entryComponents = [

];

const providers = [
  ShoppingCartService,
  ShoppingCartItemService
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
export class ShoppingCartsModule { }
