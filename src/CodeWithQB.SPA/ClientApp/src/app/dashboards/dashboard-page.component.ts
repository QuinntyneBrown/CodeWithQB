import { Component, ComponentFactoryResolver, ViewContainerRef, ViewChild, ComponentRef, Injector } from "@angular/core";
import { Subject, Observable, BehaviorSubject } from "rxjs";
import { Dashboard } from "./dashboard.model";
import { DashboardService } from "./dashboard.service";
import { takeUntil, map, tap, switchMap, filter } from "rxjs/operators";
import { DashboardCard } from "../dashboard-cards/dashboard-card.model";
import { DashboardCardComponent } from "../dashboard-cards/dashboard-card.component";
import { Overlay } from "@angular/cdk/overlay";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { PortalInjector, ComponentPortal } from "@angular/cdk/portal";
import { AddDashboardCardOverlayComponent } from "./add-dashboard-card-overlay.component";
import { DashboardCardConfigurationOverlay } from "../dashboard-cards/dashboard-card-configuration-overlay";
import { DashboardCardService } from "../dashboard-cards/dashboard-card.service";
import { deepCopy } from "../core/deep-copy";
import { AddDashboardCardOverlay } from "./add-dashboard-card-overlay";



@Component({
  templateUrl: "./dashboard-page.component.html",
  styleUrls: ["./dashboard-page.component.css"],
  selector: "app-dashboard-page"
})
export class DashboardPageComponent {
  constructor(
    private _addDashboardCardOverlay: AddDashboardCardOverlay,
    private _componentFactoryResolver: ComponentFactoryResolver,
    private _dashboardCardConfigurationOverlay: DashboardCardConfigurationOverlay,
    private _dashboardCardService: DashboardCardService,
    private _dashboardService: DashboardService,
    private _injector: Injector,
    private _overlay: Overlay
  ) { }

  @ViewChild("target", { read: ViewContainerRef })
  target: ViewContainerRef;

  public handleConfigurationClick(options: { dashboardCard: DashboardCard }) {
    this._dashboardCardConfigurationOverlay.create(options)
      .pipe(
        map((dashboardCard:DashboardCard) => {
          const currentDashboard: Dashboard = deepCopy(this.dashboard$.value);
          const index = currentDashboard.dashboardCards.findIndex(x => x.dashboardCardId == dashboardCard.dashboardCardId);
          currentDashboard.dashboardCards[index] = dashboardCard;
          this.dashboard$.next(currentDashboard);
        }),
        takeUntil(this.onDestroy)
      )
      .subscribe();
  }

  public handleDeleteClick(options: { dashboardCard: DashboardCard }) {
    const currentDashboard: Dashboard = deepCopy(this.dashboard$.value);
    const index = currentDashboard.dashboardCards.findIndex(x => x.dashboardCardId == options.dashboardCard.dashboardCardId);
    currentDashboard.dashboardCards.splice(index, 1);
    this.dashboard$.next(currentDashboard);

    this._dashboardCardService.remove({ dashboardCard: options.dashboardCard })
      .pipe(takeUntil(this.onDestroy))
      .subscribe();
  }

  ngOnInit() {
    this._dashboardService
      .getDefault()
      .pipe(
        takeUntil(this.onDestroy),
        map(x => this.dashboard$.next(x)),
        switchMap(() => this.dashboard$),
        map((dashboard: Dashboard) => {
          this._dashboardCardComponentRefs.forEach((dtc) => dtc.destroy());
          this._dashboardCardComponentRefs = [];
          dashboard.dashboardCards.forEach((dashboardCard) => this.addDashboardCardComponentRef(dashboardCard));
        })
      )
      .subscribe();
  }

  public handleFabButtonClick() {
    this._addDashboardCardOverlay.create({ dashboardId: this.dashboard$.value.dashboardId })
      .pipe(
        filter(x => x != null),
        map((dashboardCards: Array<DashboardCard>) => {
          const current: Dashboard = deepCopy(this.dashboard$.value);
          dashboardCards.map(dashboardCard => current.dashboardCards.push(dashboardCard));
          this.dashboard$.next(current);
        }),
        takeUntil(this.onDestroy))
      .subscribe()
  }

  public addDashboardCardComponentRef(dashboardCard: DashboardCard) {
    let componentFactory: ComponentRef<DashboardCardComponent>;

    switch (dashboardCard.cardId) {
      default:
        componentFactory = (<any>this._componentFactoryResolver.resolveComponentFactory(DashboardCardComponent));
        break;
    }

    const dashboardCardComponentRef: ComponentRef<DashboardCardComponent> = this.target.createComponent(<any>componentFactory, null, this._injector);

    dashboardCardComponentRef.instance.dashboardCard = dashboardCard;

    dashboardCardComponentRef.instance.onDelete
      .pipe(
        takeUntil(dashboardCardComponentRef.instance.onDestroy),
        tap((dashboardCard) => this.handleDeleteClick({ dashboardCard }))
      ).subscribe();

    dashboardCardComponentRef.instance.onConfigure
      .pipe(
        takeUntil(dashboardCardComponentRef.instance.onDestroy),
        tap((dashboardCard) => this.handleConfigurationClick({ dashboardCard }))
      ).subscribe();

    this._dashboardCardComponentRefs.push(dashboardCardComponentRef);
  }

  public _dashboardCardComponentRefs: Array<ComponentRef<any>> = [];

  public dashboard$: BehaviorSubject<Dashboard> = new BehaviorSubject(null);

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();
  }
}
