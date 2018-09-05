import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { AuthGuard } from './core/auth.guard';
import { MasterPageComponent } from './master-page.component';
import { LoginComponent } from './users/login.component';
import { DashboardPageComponent } from './dashboards/dashboard-page.component';
import { HomePageComponent } from './public/home-page.component';
import { PublicMasterPageComponent } from './public-master-page.component';
import { ProductsPageComponent } from './products/products-page.component';
import { ProductPageComponent } from './products/product-page.component';
import { CheckoutPageComponent } from './public/checkout-page.component';
import { ContactPageComponent } from './public/contact-page.component';
import { AboutPageComponent } from './public/about-page.component';

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
    path: 'admin',
    component: MasterPageComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardPageComponent
      },
      {
        path: 'products',
        component: ProductsPageComponent
      },
      {
        path: 'products/edit/:productId',
        component: ProductPageComponent
      }
    ]
  },
  {
    path: '',
    component: PublicMasterPageComponent,

    children: [
      {
        path: '',
        component: HomePageComponent,
      },
      {
        path: 'checkout',
        component: CheckoutPageComponent,
      },
      {
        path: 'about',
        component: AboutPageComponent,
      },
      {
        path: 'contact',
        component: ContactPageComponent,
      },
      {
        path: 'signup',
        component: HomePageComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
