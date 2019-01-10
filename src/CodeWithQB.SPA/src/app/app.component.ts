import { Component } from '@angular/core';
import { CustomerService } from './customers/customer.service';
import { Observable } from 'rxjs';
import { Customer } from './customers/customer.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private readonly _customerService: CustomerService
  ) { }

  ngOnInit() {
    this.customers$ = this._customerService.get();
  }
  public customers$:Observable<Customer[]>;
}
