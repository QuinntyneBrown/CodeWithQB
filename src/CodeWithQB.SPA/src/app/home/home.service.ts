import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { HomePage } from './home-page.model';

@Injectable()
export class HomeService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<HomePage> {
    return this._client.get<HomePage>(`${this._baseUrl}api/homepage`);
  }
}
