import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorTestComponent } from './components/error-test/error-test.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';



@NgModule({
  declarations: [
    ErrorTestComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule
  ]
})
export class LandingDashboardModule { }
