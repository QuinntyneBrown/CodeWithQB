import { Component, ViewChild, Input, Output, EventEmitter } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { IgxGridComponent } from "igniteui-angular";

@Component({
  templateUrl: "./grid.component.html",
  styleUrls: ["./grid.component.css"],
  selector: "app-grid"
})
export class GridComponent { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();    
  }

  @Input()
  public columns: any[] = [];

  @ViewChild("grid")
  grid: IgxGridComponent;

  @Input()
  data: any;

  afterViewInit: boolean;

  @Output()
  cellClick: EventEmitter<any> = new EventEmitter();

  public tryToDelete(cell: any) {
    console.log(cell);
  }
  ngAfterViewInit() {
    this.afterViewInit = true;
  }

  ngDoCheck() {
    if (this.grid && this.afterViewInit)
      this.grid.reflow();
  }
}
