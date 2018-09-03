import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.css"],
  selector: "app-header"
})
export class HeaderComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
