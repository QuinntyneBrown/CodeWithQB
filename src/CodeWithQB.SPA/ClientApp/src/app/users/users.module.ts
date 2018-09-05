import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';

const declarations = [LoginComponent];

@NgModule({
  declarations: declarations,
  imports: [CommonModule, CoreModule, SharedModule, RouterModule],
  exports: declarations
})
export class UsersModule {}
