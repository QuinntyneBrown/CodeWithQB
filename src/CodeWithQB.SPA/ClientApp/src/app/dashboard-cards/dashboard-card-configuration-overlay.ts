import { Injectable, ComponentRef, Injector } from "@angular/core";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { PortalInjector, ComponentPortal } from "@angular/cdk/portal";
import { DashboardCardConfigurationOverlayComponent } from "./dashboard-card-configuration-overlay.component";
import { OverlayRefProvider } from "../core/overlay-ref-provider";
import { DashboardCard } from "./dashboard-card.model";
import { Dashboard } from "../dashboards/dashboard.model";
import { Observable } from "rxjs";

@Injectable()
export class DashboardCardConfigurationOverlay {
  constructor(
    public _injector: Injector,
    public _overlayRefProvider: OverlayRefProvider
  ) { }

  public create(options: { dashboardCard: DashboardCard }): Observable<any> {
    const overlayRef = this._overlayRefProvider.create();
    const overlayRefWrapper = new OverlayRefWrapper(overlayRef);
    const overlayComponent = this.attachOverlayContainer(overlayRef, overlayRefWrapper);
    overlayComponent.dashboardCard = options.dashboardCard;
    return overlayRefWrapper.afterClosed();
  }

  public attachOverlayContainer(overlayRef, overlayRefWrapper) {
    const injectionTokens = new WeakMap();
    injectionTokens.set(OverlayRefWrapper, overlayRefWrapper);
    const injector = new PortalInjector(this._injector, injectionTokens);
    const overlayPortal = new ComponentPortal(DashboardCardConfigurationOverlayComponent, null, injector);
    const overlayPortalRef: ComponentRef<DashboardCardConfigurationOverlayComponent> = overlayRef.attach(overlayPortal);
    return overlayPortalRef.instance;
  }
}
