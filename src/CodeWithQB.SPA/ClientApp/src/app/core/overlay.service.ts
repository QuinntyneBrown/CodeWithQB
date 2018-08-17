import { Injectable, ComponentRef, Injector } from "@angular/core";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { PortalInjector, ComponentPortal } from "@angular/cdk/portal";
import { OverlayRefProvider } from "../core/overlay-ref-provider";
import { Observable } from "rxjs";
import { ComponentType } from "@angular/cdk/overlay";

export class OverlayService<TComponent> {
  constructor(
    public _injector: Injector,
    public _overlayRefProvider: OverlayRefProvider,
    private _component: ComponentType<TComponent>
  ) { }

  public create(options: { source?: any, injectionTokens?: WeakMap<object,any> } = {}): Observable<any> {
    const overlayRef = this._overlayRefProvider.create();
    const overlayRefWrapper = new OverlayRefWrapper(overlayRef);
    options.injectionTokens = options.injectionTokens || new WeakMap();
    options.injectionTokens.set(OverlayRefWrapper, overlayRefWrapper);
    const overlayComponent = this.attachOverlayContainer(overlayRef, overlayRefWrapper, options.injectionTokens);    
    Object.assign(overlayComponent, options.source);
    return overlayRefWrapper.afterClosed();
  }

  private attachOverlayContainer(overlayRef, overlayRefWrapper, injectionTokens: WeakMap<object, any>) {
    const injector = new PortalInjector(this._injector, injectionTokens);
    const overlayPortal = new ComponentPortal(this._component, null, injector);
    const overlayPortalRef: ComponentRef<TComponent> = overlayRef.attach(overlayPortal);
    return overlayPortalRef.instance;
  }
}
