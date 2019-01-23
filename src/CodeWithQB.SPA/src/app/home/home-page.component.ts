import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { HomePageService } from './home-page.service';
import { HomePage } from './home-page.model';

@Component({
  templateUrl: "./home-page.component.html",
  styleUrls: ["./home-page.component.css"],
  selector: "app-home-page"
})
export class HomePageComponent { 
  constructor(private readonly _homePageService: HomePageService) {

  }

  ngOnInit() {
    this.home$ = this._homePageService.get();
  }

  public home$: Observable<HomePage>;

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
