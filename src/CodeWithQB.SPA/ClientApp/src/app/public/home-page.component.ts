import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProductService } from "../products/product.service";
import { Product } from "../products/product.model";

@Component({
  templateUrl: "./home-page.component.html",
  styleUrls: ["./home-page.component.css"],
  selector: "app-home-page"
})
export class HomePageComponent { 

  constructor(private _productService: ProductService) {

  }

  ngOnInit() {
    this.products$ = this._productService.get();    
  }

  products$: Observable<Product[]>;

  public readonly onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();    
  }
}
