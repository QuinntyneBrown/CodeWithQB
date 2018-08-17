import { Component } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { map, switchMap } from "rxjs/operators";
import { Card } from "../cards/card.model";
import { CardService } from "../cards/card.service";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { DashboardCard } from "../dashboard-cards/dashboard-card.model";
import { DashboardCardService } from "../dashboard-cards/dashboard-card.service";

@Component({
  templateUrl: "./add-dashboard-card-overlay.component.html",
  styleUrls: ["./add-dashboard-card-overlay.component.css"],
  selector: "app-add-dashboard-card-overlay"
})
export class AddDashboardCardOverlayComponent {
  constructor(
    private readonly _overlay: OverlayRefWrapper,
    private readonly _cardService: CardService,
    private readonly _dashboardCardService: DashboardCardService
  ) {
    this.cards$ = this._cardService.get();
  }

  public dashboardId: string;

  private readonly cards$: Observable<Card[]>;

  public selectedCards: Array<Card> = [];
  
  public handleCardClick(card: Card) {
    this.cardIsSelected(card)
      ? this.selectedCards.splice(this.selectedCards.indexOf(card), 1)
      : this.selectedCards.push(card);
  }

  public cardIsSelected(card: Card) {
    return this.selectedCards.indexOf(card) > -1;
  }

  public tryToAddDashboardCards() {
    let dashboardCards = [];

    for (let i = 0; i < this.selectedCards.length; i++) {
      let dashboardCard = new DashboardCard();
      dashboardCard.cardId = this.selectedCards[i].cardId;
      dashboardCard.dashboardId = this.dashboardId;
      dashboardCards.push(dashboardCard);
    }

    this._dashboardCardService.saveRange({ dashboardCards })
      .pipe(
        switchMap(x => this._dashboardCardService.getByIds({ dashboardCardIds: x.dashboardCardIds })),
        map(dashboardCards => this._overlay.close(dashboardCards))
      )
      .subscribe();   
  }

  public handleCancelClick() { this._overlay.close(); }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() { this.onDestroy.next(); }
}
