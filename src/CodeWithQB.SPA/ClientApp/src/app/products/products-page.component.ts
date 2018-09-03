import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./products-page.component.html",
  styleUrls: ["./products-page.component.css"],
  selector: "app-products-page"
})
export class ProductsPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
