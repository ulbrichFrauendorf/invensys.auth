import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientDetailComponent } from './components/clients/client-detail/client-detail.component';
import { UserDetailComponent } from './components/users/user-detail/user-detail.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { ClientListComponent } from './components/clients/client-list/client-list.component';

@NgModule({
  declarations: [
    ClientListComponent,
    ClientDetailComponent,
    UserDetailComponent,
    UserListComponent,
  ],
  imports: [CommonModule],
})
export class AdministrationModule {}
