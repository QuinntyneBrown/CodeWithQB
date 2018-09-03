import { Component } from "@angular/core";
import { Subject } from "rxjs";
import { AuthService } from "./core/auth.service";

@Component({
  templateUrl: "./public-master-page.component.html",
  styleUrls: ["./public-master-page.component.css"],
  selector: "app-public-master-page"
})
export class PublicMasterPageComponent { 
  constructor(private _authService: AuthService) {

  }
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public tryToLogout() {
    this._authService.logout();
  }
}
