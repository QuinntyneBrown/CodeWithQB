import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { ContactRequest } from "./contact-request.model";

@Injectable()
export class ContactRequestService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ContactRequest>> {
    return this._client.get<{ contactRequests: Array<ContactRequest> }>(`${this._baseUrl}api/contactRequests`)
      .pipe(
        map(x => x.contactRequests)
      );
  }

  public getById(options: { contactRequestId: string }): Observable<ContactRequest> {
    return this._client.get<{ contactRequest: ContactRequest }>(`${this._baseUrl}api/contactRequests/${options.contactRequestId}`)
      .pipe(
        map(x => x.contactRequest)
      );
  }

  public remove(options: { contactRequest: ContactRequest }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/contactRequests/${options.contactRequest.contactRequestId}`);
  }

  public create(options: { contactRequest: ContactRequest }): Observable<{ contactRequestId: string }> {
    return this._client.post<{ contactRequestId: string }>(`${this._baseUrl}api/contactRequests`, { contactRequest: options.contactRequest });
  }

  public update(options: { contactRequest: ContactRequest }): Observable<{ contactRequestId: string }> {
    return this._client.put<{ contactRequestId: string }>(`${this._baseUrl}api/contactRequests`, { contactRequest: options.contactRequest });
  }
}
