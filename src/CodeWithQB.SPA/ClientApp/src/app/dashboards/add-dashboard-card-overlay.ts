import { Injectable, ComponentRef, Injector } from "@angular/core";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { PortalInjector, ComponentPortal } from "@angular/cdk/portal";
import { AddDashboardCardOverlayComponent } from "./add-dashboard-card-overlay.component";
import { OverlayRefProvider } from "../core/overlay-ref-provider";
import { Observable } from "rxjs";

@Injectable()
export class AddDashboardCardOverlay {
  constructor(
    public _injector: Injector,
    public _overlayRefProvider: OverlayRefProvider
  ) { }

  public create(options: { dashboardId?: string } = {}): Observable<any> {
    const overlayRef = this._overlayRefProvider.create();
    const overlayRefWrapper = new OverlayRefWrapper(overlayRef);
    const overlayComponent = this.attachOverlayContainer(overlayRef, overlayRefWrapper);
    overlayComponent.dashboardId = options.dashboardId;
    return overlayRefWrapper.afterClosed();
  }

  public attachOverlayContainer(overlayRef, overlayRefWrapper) {
    const injectionTokens = new WeakMap();
    injectionTokens.set(OverlayRefWrapper, overlayRefWrapper);
    const injector = new PortalInjector(this._injector, injectionTokens);
    const overlayPortal = new ComponentPortal(AddDashboardCardOverlayComponent, null, injector);
    const overlayPortalRef: ComponentRef<AddDashboardCardOverlayComponent> = overlayRef.attach(overlayPortal);
    return overlayPortalRef.instance;
  }
}
