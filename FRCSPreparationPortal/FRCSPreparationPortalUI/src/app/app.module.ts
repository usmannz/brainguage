import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { AuthGuard } from './services/authguard';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { ClipboardModule } from 'ngx-clipboard';
import { CookieModule, CookieService } from 'ngx-cookie';
import { UserService } from './services/user.service';
import { LoginComponent } from './pages/login/login.component';
import { ContextService } from './services/context.service';
import { PasswordToggleDirective } from './directives/password-toggle.directive';
import { TruncateTextDirective } from './directives/truncate-text.directive';
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { PaginationComponent } from './pages/pagination/pagination.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select'; // if you're using selects
import {MatDialogModule} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from './shared/confirmation-dialog/confirmation-dialog.component';
import { AdminQuestionsListingComponent } from './pages/questions/admin-question-listing/admin-question-listing.component';
import { AdminQuestionEditComponent } from './pages/questions/admin-question-edit/admin-question-edit.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { RegisterComponent } from './pages/register/register.component';
import { CategoriesListingComponent } from './pages/categories/categories-listing/categories-listing.component';
import { CategoryEditComponent } from './pages/categories/categories-edit/category-edit.component';
import { MockTestComponent } from './pages/mock-test/mock-test.component';
import { DemoTestComponent } from './pages/demo-test/demo-test.component';
import { PrepTestListingComponent } from './pages/prep-test/prep-test-listing/prep-test-listing.component';
import { PrepTestConfigComponent } from './pages/prep-test/prep-test-config/prep-test-config.component';
import { PrepTestComponent } from './pages/prep-test/prep-test/prep-test.component';


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
    MatExpansionModule,
    ToastrModule.forRoot({
      // Configure global options here
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule, // if necessary
    MatDialogModule,
    MatAutocompleteModule,
    MatInputModule

  ],
  declarations: [
    AppComponent,
    DashboardComponent,
    AdminQuestionsListingComponent,
    MockTestComponent,
    DemoTestComponent,
    UserProfileComponent,
    LoginComponent,
    RegisterComponent,
    PasswordToggleDirective,
    TruncateTextDirective,
    PaginationComponent,
    AdminQuestionEditComponent,
    ConfirmationDialogComponent,
    CategoriesListingComponent,
    CategoryEditComponent,
    PrepTestListingComponent,
    PrepTestConfigComponent,
    PrepTestComponent


  ],
  providers: [AuthGuard,{ provide: LocationStrategy, useClass: PathLocationStrategy},
    CookieService,
    UserService,
    ContextService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    provideAnimations(), // required animations providers

  ],
  entryComponents: [
    AdminQuestionEditComponent,
    CategoryEditComponent,
    PrepTestConfigComponent
],
  bootstrap: [AppComponent]
})
export class AppModule { }
