import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { ShoppingCartService } from "../shopping-carts/shopping-cart.service";
import { Router } from "@angular/router";
import { switchMap, map } from "rxjs/operators";
import { Product } from "../products/product.model";
import { ProductService } from "../products/product.service";
import { LocalStorageService } from "../core/local-storage.service";
import { currentShoppingCartKey } from "../core/constants";

@Component({
  templateUrl: "./checkout-page.component.html",
  styleUrls: ["./checkout-page.component.css"],
  selector: "app-checkout-page"
})
export class CheckoutPageComponent { 
  constructor(
    private _localStorageService: LocalStorageService,
    private _productService: ProductService,
    private _shoppingCartService: ShoppingCartService,
    private _router: Router
  ) {

  }

  ngOnInit() {
    if (this._shoppingCartService.shoppingCart$.value) {
      var items = this._shoppingCartService.shoppingCart$.value.shoppingCartItems;
      for (var i = 0; i < items.length; i++) {
        this._productService.getById({ productId: items[i].productId })
          .subscribe(x => {
            var products = this.products$.value;
            products.push(x);
            this.products$.next(products);
          });
      }
    }
  }

  public checkout() {

    this._localStorageService.put({ name: currentShoppingCartKey, value: null });

    this._router.navigateByUrl("/");
    //this._shoppingCartService
    //  .checkout()
    //  .pipe(switchMap(() => this._shoppingCartService.getCurrentCart().pipe(map(x => this.shoppingCart$.next(x)))))
    //  .subscribe(() => this._router.navigateByUrl("/"));    
  }

  public products$: BehaviorSubject<Product[]> = new BehaviorSubject([]);

  public get shoppingCart$() { return this._shoppingCartService.shoppingCart$; }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
