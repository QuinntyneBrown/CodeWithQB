import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./main-nav.component.html",
  styleUrls: ["./main-nav.component.css"],
  selector: "app-main-nav"
})
export class MainNavComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
