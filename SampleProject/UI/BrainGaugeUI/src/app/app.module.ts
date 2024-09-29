import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { AuthGuard } from './services/authguard';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { TablesComponent } from './pages/tables/tables.component';
import { IconsComponent } from './pages/icons/icons.component';
import { MapsComponent } from './pages/maps/maps.component';
import { ClipboardModule } from 'ngx-clipboard';
import { CookieModule, CookieService } from 'ngx-cookie';
import { UserService } from './services/user.service';
import { LoginComponent } from './pages/login/login.component';
import { ContextService } from './services/context.service';
import { PasswordToggleDirective } from './directives/password-toggle.directive';
import { TruncateTextDirective } from './directives/truncate-text.directive';
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { Questionsomponent } from './pages/Questions/questions.component';


@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule,
    ClipboardModule,
    CookieModule.forRoot(),
    ReactiveFormsModule,
    ToastrModule.forRoot({
      // Configure global options here
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),

  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent,
    DashboardComponent,
    Questionsomponent,
    UserProfileComponent,
    TablesComponent,
    IconsComponent,
    MapsComponent,
    LoginComponent,
    PasswordToggleDirective,
    TruncateTextDirective,

  ],
  providers: [AuthGuard,{ provide: LocationStrategy, useClass: PathLocationStrategy},
    CookieService,
    UserService,
    ContextService,
    provideAnimations(), // required animations providers

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
