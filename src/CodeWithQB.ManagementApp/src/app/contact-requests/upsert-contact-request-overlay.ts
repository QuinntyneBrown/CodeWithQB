import { Injectable, Injector } from "@angular/core";
import { OverlayRefProvider } from "../core/overlay-ref-provider";
import { UpsertContactRequestOverlayComponent } from "./upsert-contact-request-overlay.component";
import { OverlayService } from '../core/overlay.service';

@Injectable()
export class UpsertContactRequestOverlay extends OverlayService<UpsertContactRequestOverlayComponent> {
  constructor(
    public injector: Injector,
    public overlayRefProvider: OverlayRefProvider
  ) {
    super(injector, overlayRefProvider, UpsertContactRequestOverlayComponent);
  }
}
