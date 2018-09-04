import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { ShoppingCartItem } from "./shopping-cart-item.model";

@Injectable()
export class ShoppingCartItemService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ShoppingCartItem>> {
    return this._client.get<{ shoppingCartItems: Array<ShoppingCartItem> }>(`${this._baseUrl}api/shoppingCartItems`)
      .pipe(
        map(x => x.shoppingCartItems)
      );
  }

  public getById(options: { shoppingCartItemId: string }): Observable<ShoppingCartItem> {
    return this._client.get<{ shoppingCartItem: ShoppingCartItem }>(`${this._baseUrl}api/shoppingCartItems/${options.shoppingCartItemId}`)
      .pipe(
        map(x => x.shoppingCartItem)
      );
  }

  public remove(options: { shoppingCartItem: ShoppingCartItem }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/shoppingCartItems/${options.shoppingCartItem.shoppingCartItemId}`);
  }

  public create(options: { shoppingCartItem: ShoppingCartItem }): Observable<{ shoppingCartItemId: string }> {
    return this._client.post<{ shoppingCartItemId: string }>(`${this._baseUrl}api/shoppingCartItems`, { shoppingCartItem: options.shoppingCartItem });
  }

  public update(options: { shoppingCartItem: ShoppingCartItem }): Observable<{ shoppingCartItemId: string }> {
    return this._client.put<{ shoppingCartItemId: string }>(`${this._baseUrl}api/shoppingCartItems`, { shoppingCartItem: options.shoppingCartItem });
  }
}
