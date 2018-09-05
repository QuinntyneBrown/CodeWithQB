import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProductService } from "./product.service";
import { Product } from "./product.model";
import { map } from "rxjs/operators";
import { Router } from "@angular/router";

@Component({
  templateUrl: "./products-page.component.html",
  styleUrls: ["./products-page.component.css"],
  selector: "app-products-page"
})
export class ProductsPageComponent { 
  constructor(
    private _productService: ProductService,
    private _router: Router
  ) {
    this.products$ = this._productService
      .get();
  }

  ngOnInit() {
    
  }

  handleCellClick($event) {    
    this._router.navigateByUrl(`/admin/products/edit/${$event.cell.row.rowData.productId}`);
  }

  columns: any[] = [{
    field: "name",
    header: "Name"
  }];

  public readonly products$: Observable<Product[]>;

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
