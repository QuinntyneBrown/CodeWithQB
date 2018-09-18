import { Component } from "@angular/core";
import { Subject, Observable, BehaviorSubject } from "rxjs";
import { AuthService } from "./core/auth.service";
import { usernameKey, accessTokenKey, shoppingCartInfoKey } from "./core/constants";
import { LocalStorageService } from "./core/local-storage.service";
import { ShoppingCartService } from "./shopping-carts/shopping-cart.service";
import { ShoppingCart } from "./shopping-carts/shopping-cart.model";
import { map, takeUntil } from "rxjs/operators";

@Component({
  templateUrl: "./public-master-page.component.html",
  styleUrls: ["./public-master-page.component.css"],
  selector: "app-public-master-page"
})
export class PublicMasterPageComponent { 
  constructor(
    private _authService: AuthService,
    private _localStorageService: LocalStorageService,
    private _shoppingCartService: ShoppingCartService
  ) {

  }

  public get shoppingCart$(): BehaviorSubject<ShoppingCart> {
    return this._shoppingCartService.shoppingCart$;
  }

  ngOnInit() {
    if (this.accessToken) {
      var shoppingCartInfo = JSON.parse(this._localStorageService.get({ name: shoppingCartInfoKey }));

      if (shoppingCartInfo) {
        alert(shoppingCartInfo.version);

        this._shoppingCartService.getById({ shoppingCartId: shoppingCartInfo.shoppingCartId })
          .pipe(map(x => this.shoppingCart$.next(x)), takeUntil(this.onDestroy))
          .subscribe();
      }
    }
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public tryToLogout() {
    this._authService.logout();
  }

  public get username() {
    return this._localStorageService.get({ name: usernameKey });
  }

  public get accessToken() {
    return this._localStorageService.get({ name: accessTokenKey });
  }
}
