import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Customer } from "./customer.model";

@Injectable()
export class CustomerService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Customer>> {
    return this._client.get<{ customers: Array<Customer> }>(`${this._baseUrl}api/customers`)
      .pipe(
        map(x => x.customers)
      );
  }

  public getById(options: { customerId: string }): Observable<Customer> {
    return this._client.get<{ customer: Customer }>(`${this._baseUrl}api/customers/${options.customerId}`)
      .pipe(
        map(x => x.customer)
      );
  }

  public remove(options: { customer: Customer }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/customers/${options.customer.customerId}`);
  }

  public upsert(options: { customer: Customer }): Observable<{ customerId: string }> {
    return this._client.post<{ customerId: string }>(`${this._baseUrl}api/customers`, { customer: options.customer });
  }
}
