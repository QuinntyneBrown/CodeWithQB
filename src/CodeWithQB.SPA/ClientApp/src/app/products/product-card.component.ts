import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Subject } from "rxjs";
import { Product } from "./product.model";

@Component({
  templateUrl: "./product-card.component.html",
  styleUrls: ["./product-card.component.css"],
  selector: "app-product-card"
})
export class ProductCardComponent { 

  private readonly onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  @Input()
  public product: Product = <Product>{};

  @Output()
  public buy: EventEmitter<any> = new EventEmitter<any>();

  @Input()
  public isAuthenticated: boolean = false;
}
