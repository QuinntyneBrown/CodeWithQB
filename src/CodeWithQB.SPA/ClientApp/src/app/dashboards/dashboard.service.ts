import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Dashboard } from "./dashboard.model";

@Injectable()
export class DashboardService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Dashboard>> {
    return this._client.get<{ dashboards: Array<Dashboard> }>(`${this._baseUrl}api/dashboards`)
      .pipe(
        map(x => x.dashboards)
      );
  }

  public getDefault(): Observable<Dashboard> {
    return this._client.get<{ dashboard: Dashboard }>(`${this._baseUrl}api/dashboards/default`)
      .pipe(
        map(x => x.dashboard)
      );
  }

  public getById(options: { dashboardId: string }): Observable<Dashboard> {
    return this._client.get<{ dashboard: Dashboard }>(`${this._baseUrl}api/dashboards/${options.dashboardId}`)
      .pipe(
        map(x => x.dashboard)
      );
  }

  public remove(options: { dashboard: Dashboard }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/dashboards/${options.dashboard.dashboardId}`);
  }

  public create(options: { dashboard: Dashboard }): Observable<{ dashboardId: string }> {
    return this._client.post<{ dashboardId: string }>(`${this._baseUrl}api/dashboards`, { dashboard: options.dashboard });
  }

  public update(options: { dashboard: Dashboard }): Observable<{ dashboardId: string }> {
    return this._client.put<{ dashboardId: string }>(`${this._baseUrl}api/dashboards`, { dashboard: options.dashboard });
  }
}
