import { Component, Output, EventEmitter } from "@angular/core";
import { Subject } from "rxjs";
import { LocalStorageService } from "../core/local-storage.service";
import { usernameKey, accessTokenKey } from "../core/constants";

@Component({
  templateUrl: "./login-sign-up.component.html",
  styleUrls: ["./login-sign-up.component.css"],
  selector: "app-login-sign-up"
})
export class LoginSignUpComponent { 
  constructor(private _localStorageService: LocalStorageService) {

  }

  private readonly onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public get username() {
    return this._localStorageService.get({ name: usernameKey });
  }

  public get accessToken() {
    return this._localStorageService.get({ name: accessTokenKey });
  }

  @Output()
  public logout: EventEmitter<any> = new EventEmitter();
}
