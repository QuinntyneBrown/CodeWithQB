import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./login-page.component.html",
  styleUrls: ["./login-page.component.css"],
  selector: "app-login-page"
})
export class LoginPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
