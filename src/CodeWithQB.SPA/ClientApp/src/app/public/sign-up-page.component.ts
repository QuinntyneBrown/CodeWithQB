import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./sign-up-page.component.html",
  styleUrls: ["./sign-up-page.component.css"],
  selector: "app-sign-up-page"
})
export class SignUpPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
