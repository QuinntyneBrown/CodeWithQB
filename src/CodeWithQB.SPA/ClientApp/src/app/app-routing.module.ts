import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { AuthGuard } from './core/auth.guard';
import { MasterPageComponent } from './master-page.component';
import { LoginComponent } from './users/login.component';
import { DashboardPageComponent } from './dashboards/dashboard-page.component';

export const routes: Routes = [
  {
    path: 'login',
    component: AnonymousMasterPageComponent,
    children: [
      {
        path: '',
        component: LoginComponent
      }
    ]
  },
  {
    path: '',
    component: MasterPageComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: DashboardPageComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
