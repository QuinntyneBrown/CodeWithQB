import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./contact-page.component.html",
  styleUrls: ["./contact-page.component.css"],
  selector: "app-contact-page"
})
export class ContactPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
