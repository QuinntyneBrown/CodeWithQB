import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Talk } from "./talk.model";

@Injectable()
export class TalkService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Talk>> {
    return this._client.get<{ talks: Array<Talk> }>(`${this._baseUrl}api/talks`)
      .pipe(
        map(x => x.talks)
      );
  }

  public getById(options: { talkId: string }): Observable<Talk> {
    return this._client.get<{ talk: Talk }>(`${this._baseUrl}api/talks/${options.talkId}`)
      .pipe(
        map(x => x.talk)
      );
  }

  public remove(options: { talk: Talk }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/talks/${options.talk.talkId}`);
  }

  public create(options: { talk: Talk }): Observable<{ talkId: string }> {
    return this._client.post<{ talkId: string }>(`${this._baseUrl}api/talks`, { talk: options.talk });
  }

  public update(options: { talk: Talk }): Observable<{ talkId: string }> {
    return this._client.put<{ talkId: string }>(`${this._baseUrl}api/talks`, { talk: options.talk });
  }
}
