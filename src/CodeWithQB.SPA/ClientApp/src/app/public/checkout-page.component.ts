import { Component } from "@angular/core";
import { Subject } from "rxjs";
import { ShoppingCartService } from "../shopping-carts/shopping-cart.service";
import { Router } from "@angular/router";
import { switchMap, map } from "rxjs/operators";

@Component({
  templateUrl: "./checkout-page.component.html",
  styleUrls: ["./checkout-page.component.css"],
  selector: "app-checkout-page"
})
export class CheckoutPageComponent { 
  constructor(
    private _shoppingCartService: ShoppingCartService,
    private _router: Router
  ) {

  }
  
  public checkout() {
    this._shoppingCartService
      .checkout()
      .pipe(switchMap(() => this._shoppingCartService.getCurrentCart().pipe(map(x => this.shoppingCart$.next(x)))))
      .subscribe(() => this._router.navigateByUrl("/"));    
  }

  public get shoppingCart$() { return this._shoppingCartService.shoppingCart$; }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
