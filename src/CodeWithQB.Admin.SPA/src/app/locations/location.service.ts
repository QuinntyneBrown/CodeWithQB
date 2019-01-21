import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Location } from "./location.model";

@Injectable()
export class LocationService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Location>> {
    return this._client.get<{ locations: Array<Location> }>(`${this._baseUrl}api/locations`)
      .pipe(
        map(x => x.locations)
      );
  }

  public getById(options: { locationId: string }): Observable<Location> {
    return this._client.get<{ location: Location }>(`${this._baseUrl}api/locations/${options.locationId}`)
      .pipe(
        map(x => x.location)
      );
  }

  public remove(options: { location: Location }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/locations/${options.location.locationId}`);
  }

  public create(options: { location: Location }): Observable<{ locationId: string }> {
    return this._client.post<{ locationId: string }>(`${this._baseUrl}api/locations`, { location: options.location });
  }

  public update(options: { location: Location }): Observable<{ locationId: string }> {
    return this._client.put<{ locationId: string }>(`${this._baseUrl}api/locations`, { location: options.location });
  }
}
