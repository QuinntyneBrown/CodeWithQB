import { Injectable, NgZone, Inject } from '@angular/core';
import { Subject } from 'rxjs';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions, LogLevel } from "@aspnet/signalr";
import { baseUrl, accessTokenKey } from './constants';
import { LocalStorageService } from './local-storage.service';

@Injectable()
export class HubClient {
  private _connection: HubConnection;
  public messages$: Subject<any> = new Subject();

  constructor(
    @Inject(baseUrl) private _baseUrl: string,    
    private _ngZone: NgZone,
    private _storage: LocalStorageService
  ) {}

  private _connect: Promise<any>;

  public connect(): Promise<any> {
    if (this._connect) return this._connect;

    this._connect = new Promise(resolve => {

      const options: IHttpConnectionOptions = {        
        accessTokenFactory: () => this._storage.get({
          name: accessTokenKey
        }),
        logMessageContent: true
      };

      this._connection = this._connection || new HubConnectionBuilder()
        .withUrl(`${this._baseUrl}hub`, options)
        .configureLogging(LogLevel.Information)
        .build();

      this._connection.on('message', value => {
        this._ngZone.run(() => this.messages$.next(value));
      });

      this._connection.start().then(() => resolve());
    });

    return this._connect;
  }

  public disconnect() {
    if (this._connection) {
      this._connection.stop();
      this._connect = null;
      this._connection = null;
    }
  }
}
