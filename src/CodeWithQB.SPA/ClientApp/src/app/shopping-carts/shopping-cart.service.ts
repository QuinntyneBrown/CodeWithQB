import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { ShoppingCart } from "./shopping-cart.model";
import { ShoppingCartItem } from "./shopping-cart-item.model";

@Injectable()
export class ShoppingCartService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public shoppingCart$: BehaviorSubject<ShoppingCart> = new BehaviorSubject(null);

  public get(): Observable<Array<ShoppingCart>> {
    return this._client.get<{ shoppingCarts: Array<ShoppingCart> }>(`${this._baseUrl}api/shoppingCarts`)
      .pipe(
        map(x => x.shoppingCarts)
      );
  }

  public getCurrentCart(): Observable<ShoppingCart> {
    return this._client.get<{ shoppingCart: ShoppingCart }>(`${this._baseUrl}api/shoppingCarts/current`)
      .pipe(
        map(x => x.shoppingCart)
      );
  }

  public getById(options: { shoppingCartId: string }): Observable<ShoppingCart> {
    return this._client.get<{ shoppingCart: ShoppingCart }>(`${this._baseUrl}api/shoppingCarts/${options.shoppingCartId}`)
      .pipe(
        map(x => x.shoppingCart)
      );
  }

  public remove(options: { shoppingCart: ShoppingCart }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/shoppingCarts/${options.shoppingCart.shoppingCartId}`);
  }

  public checkout(): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/shoppingCarts/checkout`,null);
  }

  public create(options: { shoppingCart: ShoppingCart }): Observable<{ shoppingCart: ShoppingCart }> {
    return this._client.post<{ shoppingCart: ShoppingCart }>(`${this._baseUrl}api/shoppingCarts`, { shoppingCart: options.shoppingCart });
  }

  public createShoppingCartItem(options: { shoppingCartId: string, productId: string, version: number }): Observable<{ shoppingCart: ShoppingCart }> {
    return this._client.post<{ shoppingCart: ShoppingCart }>(`${this._baseUrl}api/shoppingCarts/${options.shoppingCartId}/shoppingCartItem`, {
      shoppingCartId: options.shoppingCartId,
      productId: options.productId,
      version: options.version
    });
  }

  public update(options: { shoppingCart: ShoppingCart }): Observable<{ shoppingCartId: string }> {
    return this._client.put<{ shoppingCartId: string }>(`${this._baseUrl}api/shoppingCarts`, { shoppingCart: options.shoppingCart });
  }
}
