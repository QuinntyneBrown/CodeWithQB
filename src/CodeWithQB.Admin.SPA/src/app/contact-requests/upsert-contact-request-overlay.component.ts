import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { FormGroup, FormControl } from "@angular/forms";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { ContactRequestService } from "./contact-request.service";
import { ContactRequest } from "./contact-request.model";
import { map, switchMap, tap, takeUntil } from "rxjs/operators";

@Component({
  templateUrl: "./upsert-contact-request-overlay.component.html",
  styleUrls: ["./upsert-contact-request-overlay.component.css"],
  selector: "app-upsert-contact-request-overlay",
  host: { 'class': 'mat-typography' }
})
export class UpsertContactRequestOverlayComponent { 
  constructor(
    private _contactRequestService: ContactRequestService,
    private _overlay: OverlayRefWrapper) { }

  ngOnInit() {
    if (this.contactRequestId)
      this._contactRequestService.getById({ contactRequestId: this.contactRequestId })
        .pipe(
          map(x => this.contactRequest$.next(x)),
          switchMap(x => this.contactRequest$),
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

  public contactRequest$: BehaviorSubject<ContactRequest> = new BehaviorSubject(<ContactRequest>{});
  
  public contactRequestId: string;

  public handleCancelClick() {
    this._overlay.close();
  }

  public handleSaveClick() {
    const contactRequest = new ContactRequest();
    contactRequest.contactRequestId = this.contactRequestId;
    contactRequest.name = this.form.value.name;
    this._contactRequestService.create({ contactRequest })
      .pipe(
        map(x => contactRequest.contactRequestId = x.contactRequestId),
        tap(x => this._overlay.close(contactRequest)),
        takeUntil(this.onDestroy)
      )
      .subscribe();
  }

  public form: FormGroup = new FormGroup({
    name: new FormControl(null, [])
  });
} 
