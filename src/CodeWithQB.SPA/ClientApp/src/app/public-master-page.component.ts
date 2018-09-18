import { Component } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";
import { AuthService } from "./core/auth.service";
import { accessTokenKey, currentShoppingCartKey, usernameKey } from "./core/constants";
import { LocalStorageService } from "./core/local-storage.service";
import { ShoppingCart } from "./shopping-carts/shopping-cart.model";

@Component({
  templateUrl: "./public-master-page.component.html",
  styleUrls: ["./public-master-page.component.css"],
  selector: "app-public-master-page"
})
export class PublicMasterPageComponent { 
  constructor(
    private _authService: AuthService,
    private _localStorageService: LocalStorageService 
  ) { }

  public shoppingCart$: BehaviorSubject<ShoppingCart> = new BehaviorSubject<ShoppingCart>(null);
  
  ngOnInit() {
    if (this.accessToken) {      
      var shoppingCart = this._localStorageService.get({ name: currentShoppingCartKey });

      if (shoppingCart)        
        this.shoppingCart$.next(shoppingCart as ShoppingCart);      
    }

    this._localStorageService.localStorageServiceChanged.subscribe(() => {      
      this.shoppingCart$.next(this._localStorageService.get({ name: currentShoppingCartKey }) as ShoppingCart);
    });
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
