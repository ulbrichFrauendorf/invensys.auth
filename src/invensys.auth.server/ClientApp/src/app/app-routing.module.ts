import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientDetailComponent } from './modules/administration/components/clients/client-detail/client-detail.component';
import { UserDetailComponent } from './modules/administration/components/users/user-detail/user-detail.component';
import { DashboardComponent } from './modules/landing-dashboard/pages/dashboard/dashboard.component';
import { ClientListComponent } from './modules/administration/components/clients/client-list/client-list.component';
import { UserListComponent } from './modules/administration/components/users/user-list/user-list.component';
import { authenticationGuard } from './core/guards/authentication.guard';
import { RegisterComponent } from './modules/authentication/components/register/register.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authenticationGuard],
    children: [
      { path: 'register', component: RegisterComponent },
      { path: 'clients', component: ClientListComponent },
      { path: 'clients/:id', component: ClientDetailComponent },
      { path: 'users', component: UserListComponent },
      { path: 'users/:id', component: UserDetailComponent },
      { path: '**', component: DashboardComponent, pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
