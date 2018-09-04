import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProductService } from "../products/product.service";
import { Product } from "../products/product.model";
import { AuthService } from "../core/auth.service";
import { LocalStorageService } from "../core/local-storage.service";
import { accessTokenKey } from "../core/constants";
import { ShoppingCartService } from "../shopping-carts/shopping-cart.service";

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
    alert($event.product.name);
  }

  ngOnDestroy() {
    this.onDestroy.next();    
  }
}
