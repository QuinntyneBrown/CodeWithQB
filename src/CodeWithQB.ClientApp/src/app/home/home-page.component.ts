import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { HomePageService } from './home-page.service';
import { HomePage } from './home-page.model';

@Component({
  templateUrl: "./home-page.component.html",
  styleUrls: ["./home-page.component.css"],
  selector: "app-home-page"
})
export class HomePageComponent { 
  constructor(private readonly _homePageService: HomePageService) { }

  public homePage$: Observable<HomePage> = this._homePageService.get();
}
