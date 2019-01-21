import { OverlayRef } from '@angular/cdk/overlay';
import { BehaviorSubject, Subject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class OverlayRefWrapper {
  constructor(private overlayRef: OverlayRef) { }

  close(result: any = null): void {    
    this.overlayRef.dispose();
    this.result$.next(result);
    this._afterClosed.next(result);
  }

  public result$: BehaviorSubject<any> = new BehaviorSubject(null);

  public afterClosed(): Observable<any> {
    return this._afterClosed;
  }

  private _afterClosed = new Subject<any>();
}
