import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { HomePageComponent } from './home-page.component';
import { HomePageService } from './home-page.service';
import { CoursePageComponent } from '../courses/course-page.component';

const declarations = [
    HomePageComponent,
    CoursePageComponent
];

const entryComponents = [
];

const providers = [
  HomePageService
];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    CoreModule,
    SharedModule	
  ],
  providers,
})
export class HomeModule { }
