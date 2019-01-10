import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { FormGroup, FormControl } from "@angular/forms";
import { CustomerService } from "./customer.service";
import { Customer } from "./customer.model";
import { map, switchMap, tap, takeUntil } from "rxjs/operators";
import { OverlayRefWrapper } from '../core/overlay-ref-wrapper';

@Component({
  templateUrl: "./upsert-customer-overlay.component.html",
  styleUrls: ["./upsert-customer-overlay.component.css"],
  selector: "app-upsert-customer-overlay",
  host: { 'class': 'mat-typography' }
})
export class UpsertCustomerOverlayComponent { 
  constructor(
    private _customerService: CustomerService,
    private _overlay: OverlayRefWrapper) { }

  ngOnInit() {
    if (this.customerId)
      this._customerService.getById({ customerId: this.customerId })
        .pipe(
          map(x => this.customer$.next(x)),
          switchMap(x => this.customer$),
          map(x => this.form.patchValue({
            name: x.name
          }))
        )
        .subscribe();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public customer$: BehaviorSubject<Customer> = new BehaviorSubject(<Customer>{});
  
  public customerId: string;

  public handleCancelClick() {
    this._overlay.close();
  }

  public handleSaveClick() {
    const customer = new Customer();
    customer.customerId = this.customerId;
    customer.name = this.form.value.name;
    this._customerService.upsert({ customer })
      .pipe(
        map(x => customer.customerId = x.customerId),
        tap(x => this._overlay.close(customer)),
        takeUntil(this.onDestroy)
      )
      .subscribe();
  }

  public form: FormGroup = new FormGroup({
    name: new FormControl(null, []),
    isLive: new FormControl(null,[])
  });
} 
