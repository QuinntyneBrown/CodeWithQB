import { Component } from "@angular/core";
import { Subject } from "rxjs";
import { ProductService } from "./product.service";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Product } from "./product.model";
import { ActivatedRoute, Router } from "@angular/router";
import { takeUntil } from "rxjs/operators";

@Component({
  templateUrl: "./product-page.component.html",
  styleUrls: ["./product-page.component.css"],
  selector: "app-product-page"
})
export class ProductPageComponent { 
  constructor(
    private _productService: ProductService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router

  ) {

  }

  ngOnInit() {
    if (this.productId) {
      this._productService.getById({ productId: this.productId })
        .subscribe(x => {

          this.product = x;
          this.form.patchValue({
            name: this.product.name,
            description: this.product.description
          });
        });
    }
  }

  public handleSaveClick() {
    let product = new Product();
    
    product.productId = this.product.productId;
    product.name = this.form.value.name;
    product.description = this.form.value.description;
    
    this._productService
      .update({ product })
      .pipe(takeUntil(this.onDestroy))
      .subscribe(() => {
        this.form.reset();
        this._router.navigateByUrl('/admin/products');
      });
  }

  public get productId(): string {
    return this._activatedRoute.snapshot.params['productId'];
  }

  public product: Product = <Product>{};

  public onDestroy: Subject<void> = new Subject<void>();

  public form: FormGroup = new FormGroup({
    name: new FormControl(this.product.name, [Validators.required]),
    description: new FormControl(this.product.description, [Validators.required])
  });


  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
