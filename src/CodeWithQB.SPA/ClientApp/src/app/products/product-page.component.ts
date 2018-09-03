import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./product-page.component.html",
  styleUrls: ["./product-page.component.css"],
  selector: "app-product-page"
})
export class ProductPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
