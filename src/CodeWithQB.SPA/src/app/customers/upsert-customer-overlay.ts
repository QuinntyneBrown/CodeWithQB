import { Injectable, Injector } from "@angular/core";
import { OverlayRefProvider } from "../core/overlay-ref-provider";
import { UpsertCustomerOverlayComponent } from "./upsert-customer-overlay.component";
import { OverlayService } from '../core/overlay.service';

@Injectable()
export class UpsertCustomerOverlay extends OverlayService<UpsertCustomerOverlayComponent> {
  constructor(
    public injector: Injector,
    public overlayRefProvider: OverlayRefProvider
  ) {
    super(injector, overlayRefProvider, UpsertCustomerOverlayComponent);
  }
}
