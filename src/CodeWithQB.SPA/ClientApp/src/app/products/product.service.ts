import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Product } from "./product.model";

@Injectable()
export class ProductService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Product>> {
    return this._client.get<{ products: Array<Product> }>(`${this._baseUrl}api/products`)
      .pipe(
        map(x => x.products)
      );
  }

  public getById(options: { productId: string }): Observable<Product> {
    return this._client.get<{ product: Product }>(`${this._baseUrl}api/products/${options.productId}`)
      .pipe(
        map(x => x.product)
      );
  }

  public remove(options: { product: Product }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/products/${options.product.productId}`);
  }

  public create(options: { product: Product }): Observable<{ productId: string }> {
    return this._client.post<{ productId: string }>(`${this._baseUrl}api/products`, { product: options.product });
  }

  public update(options: { product: Product }): Observable<{ productId: string }> {
    return this._client.put<{ productId: string }>(`${this._baseUrl}api/products`, { product: options.product });
  }
}
