import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NavComponent } from './layouts/nav/nav.component';
import { AuthenticationModule } from './modules/authentication/authentication.module';
import { UiModule } from './modules/shared/ui/ui.module';
import { AdministrationModule } from './modules/administration/administration.module';
import { LandingDashboardModule } from './modules/landing-dashboard/landing-dashboard.module';
import { ApiErrorInterceptor } from './core/interceptors/api-error.interceptor';

@NgModule({
  declarations: [AppComponent, NavComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthenticationModule,
    LandingDashboardModule,
    AdministrationModule,
    UiModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ApiErrorInterceptor, multi:true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
