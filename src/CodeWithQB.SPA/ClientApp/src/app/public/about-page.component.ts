import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./about-page.component.html",
  styleUrls: ["./about-page.component.css"],
  selector: "app-about-page"
})
export class AboutPageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
