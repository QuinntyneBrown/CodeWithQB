import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProductService } from "../products/product.service";
import { Product } from "../products/product.model";
import { AuthService } from "../core/auth.service";
import { LocalStorageService } from "../core/local-storage.service";
import { accessTokenKey, shoppingCartInfoKey } from "../core/constants";
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
    private _shoppingCartService: ShoppingCartService
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
    var shoppingCartInfo = JSON.parse(this._localStorageService.get({ name: shoppingCartInfoKey }));
    
    if (shoppingCartInfo == null) {
      this._shoppingCartService
        .create({ shoppingCart: new ShoppingCart() })
        .pipe(tap(x => {
          this._localStorageService.put({
            name: shoppingCartInfoKey, value: JSON.stringify({
              shoppingCartId: x.shoppingCartId,
              version: x.version
            })
          });
        }),
        switchMap(() => {
          var info = JSON.parse(this._localStorageService.get({ name: shoppingCartInfoKey }));

          return this._shoppingCartService.createShoppingCartItem({
            shoppingCartId: info.shoppingCartId,
            productId: $event.product.productId,
            version: info.version
          }).pipe(tap(x => {
            var info = JSON.parse(this._localStorageService.get({ name: shoppingCartInfoKey }));
            info.version = x.version;
            this._localStorageService.put({ name: shoppingCartInfoKey, value: JSON.stringify(info) });
            }))
        }),
          takeUntil(this.onDestroy))
        .subscribe();
    } else {
      var info = JSON.parse(this._localStorageService.get({ name: shoppingCartInfoKey }));

      this._shoppingCartService.createShoppingCartItem({
        shoppingCartId: info.shoppingCartId,
        productId: $event.product.productId,
        version: info.version
      })
        .pipe(tap(x => {
          var info = JSON.parse(this._localStorageService.get({ name: shoppingCartInfoKey }));
          info.version = x.version;
          this._localStorageService.put({ name: shoppingCartInfoKey, value: JSON.stringify(info) });
        }),
        takeUntil(this.onDestroy))
        .subscribe();      
    }
  }

  ngOnDestroy() {
    this.onDestroy.next();    
  }
}
