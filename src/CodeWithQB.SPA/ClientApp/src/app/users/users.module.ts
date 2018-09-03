import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { LoginSignUpComponent } from './login-sign-up.component';
import { RouterModule } from '@angular/router';

const declarations = [LoginComponent, LoginSignUpComponent];

@NgModule({
  declarations: declarations,
  imports: [CommonModule, CoreModule, SharedModule, RouterModule],
  exports: declarations
})
export class UsersModule {}
