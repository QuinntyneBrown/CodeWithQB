import { Component } from "@angular/core";
import { Subject } from "rxjs";

@Component({
  templateUrl: "./course-page.component.html",
  styleUrls: ["./course-page.component.css"],
  selector: "app-course-page"
})
export class CoursePageComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
