import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProductService } from "../products/product.service";
import { Product } from "../products/product.model";
import { AuthService } from "../core/auth.service";
import { LocalStorageService } from "../core/local-storage.service";
import { accessTokenKey, currentShoppingCartKey } from "../core/constants";
import { ShoppingCartService } from "../shopping-carts/shopping-cart.service";
import { switchMap, takeUntil, tap } from "rxjs/operators";
import { ShoppingCart } from "../shopping-carts/shopping-cart.model";

@Component({
  templateUrl: "./home-page.component.html",
  styleUrls: ["./home-page.component.css"],
  selector: "app-home-page"
})
export class HomePageComponent { 

  constructor(
    private _localStorageService: LocalStorageService,
    private _productService: ProductService,
    private _shoppingCartService: ShoppingCartService,
    
  ) { }

  public get accessToken() {
    return this._localStorageService.get({ name: accessTokenKey });
  }

  ngOnInit() {
    this.products$ = this._productService.get();    
  }

  products$: Observable<Product[]>;

  public readonly onDestroy: Subject<void> = new Subject<void>();

  public handleBuy($event) {    
    var shoppingCart = this._localStorageService.get({ name: currentShoppingCartKey }) as ShoppingCart;
    
    if (shoppingCart == null) {
      this._shoppingCartService
        .create({ shoppingCart: new ShoppingCart() })
        .pipe(tap(x => this._localStorageService.put({ name: currentShoppingCartKey, value: x.shoppingCart })),
        switchMap(() => {
          shoppingCart = this._localStorageService.get({ name: currentShoppingCartKey }) as ShoppingCart;

          return this._shoppingCartService.createShoppingCartItem({
            shoppingCartId: shoppingCart.shoppingCartId,
            productId: $event.product.productId,
            version: shoppingCart.version
          }).pipe(tap(x => {
              this._localStorageService.put({ name: currentShoppingCartKey, value: x.shoppingCart });
            }))
        }),
          takeUntil(this.onDestroy))
        .subscribe();
    } else {      
      this._shoppingCartService.createShoppingCartItem({
        shoppingCartId: shoppingCart.shoppingCartId,
        productId: $event.product.productId,
        version: shoppingCart.version
      })
        .pipe(tap(x => {
          this._localStorageService.put({ name: currentShoppingCartKey, value: x.shoppingCart });
        }), takeUntil(this.onDestroy))
        .subscribe();      
    }
  }
  
  ngOnDestroy() {
    this.onDestroy.next();    
  }
}
