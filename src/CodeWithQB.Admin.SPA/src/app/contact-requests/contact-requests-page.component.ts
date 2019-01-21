import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./contact-requests-page.component.html",
  styleUrls: ["./contact-requests-page.component.css"],
  selector: "app-contact-requests-page"
})
export class ContactRequestsPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
