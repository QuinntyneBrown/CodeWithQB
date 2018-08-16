import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Card } from "./card.model";

@Injectable()
export class CardService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Card>> {
    return this._client.get<{ cards: Array<Card> }>(`${this._baseUrl}api/cards`)
      .pipe(
        map(x => x.cards)
      );
  }

  public getById(options: { cardId: string }): Observable<Card> {
    return this._client.get<{ card: Card }>(`${this._baseUrl}api/cards/${options.cardId}`)
      .pipe(
        map(x => x.card)
      );
  }

  public remove(options: { card: Card }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/cards/${options.card.cardId}`);
  }

  public create(options: { card: Card }): Observable<{ cardId: string }> {
    return this._client.post<{ cardId: string }>(`${this._baseUrl}api/cards`, { card: options.card });
  }

  public update(options: { card: Card }): Observable<{ cardId: string }> {
    return this._client.put<{ cardId: string }>(`${this._baseUrl}api/cards`, { card: options.card });
  }
}
