import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./public-master-page.component.html",
  styleUrls: ["./public-master-page.component.css"],
  selector: "app-public-master-page"
})
export class PublicMasterPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
