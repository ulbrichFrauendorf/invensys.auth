import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientDetailComponent } from './clients/client-detail/client-detail.component';
import { ClientListComponent } from './clients/client-list/client-list.component';
import { LandingDashboardComponent } from './landing-dashboard/landing-dashboard.component';
import { UserDetailComponent } from './users/user-detail/user-detail.component';

const routes: Routes = [
  {path: '', component: LandingDashboardComponent},
  {path: 'clients', component: ClientListComponent},
  {path: 'clients/:id', component: ClientDetailComponent},
  {path: 'users/:id', component: UserDetailComponent},
  {path: '**', component: LandingDashboardComponent, pathMatch: 'full'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
