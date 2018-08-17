import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Mentee } from "./mentee.model";

@Injectable()
export class MenteeService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Mentee>> {
    return this._client.get<{ mentees: Array<Mentee> }>(`${this._baseUrl}api/mentees`)
      .pipe(
        map(x => x.mentees)
      );
  }

  public getById(options: { menteeId: string }): Observable<Mentee> {
    return this._client.get<{ mentee: Mentee }>(`${this._baseUrl}api/mentees/${options.menteeId}`)
      .pipe(
        map(x => x.mentee)
      );
  }

  public remove(options: { mentee: Mentee }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/mentees/${options.mentee.menteeId}`);
  }

  public create(options: { mentee: Mentee }): Observable<{ menteeId: string }> {
    return this._client.post<{ menteeId: string }>(`${this._baseUrl}api/mentees`, { mentee: options.mentee });
  }

  public update(options: { mentee: Mentee }): Observable<{ menteeId: string }> {
    return this._client.put<{ menteeId: string }>(`${this._baseUrl}api/mentees`, { mentee: options.mentee });
  }
}
