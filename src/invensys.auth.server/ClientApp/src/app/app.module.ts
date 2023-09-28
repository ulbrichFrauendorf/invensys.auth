import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

//Ui design modules
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input'
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './layouts/nav/nav.component';
import { ClientDetailComponent } from './modules/administration/components/clients/client-detail/client-detail.component';
import { UserDetailComponent } from './modules/administration/components/users/user-detail/user-detail.component';
import { AuthenticationModule } from './modules/authentication/authentication.module';
import { UserListComponent } from './modules/administration/components/users/user-list/user-list.component';
import { MatSnackBar } from '@angular/material/snack-bar';


@NgModule({
    declarations: [
        AppComponent,
        NavComponent,
        ClientDetailComponent,
        UserDetailComponent,
        UserListComponent
    ],
    imports: [
        AuthenticationModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatSlideToggleModule,
        MatMenuModule,
        MatToolbarModule,
        MatSidenavModule,
        MatListModule,
        MatButtonModule,
        MatIconModule,
        MatDialogModule
    ],
    providers: [
        MatSnackBar
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
