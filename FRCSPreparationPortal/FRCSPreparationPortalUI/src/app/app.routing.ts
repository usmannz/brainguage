import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AuthGuard } from './services/authguard';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { CategoriesListingComponent } from './pages/categories/categories-listing/categories-listing.component';
import { AdminQuestionsListingComponent } from './pages/questions/admin-question-listing/admin-question-listing.component';
import { MockTestComponent } from './pages/mock-test/mock-test.component';
import { DemoTestComponent } from './pages/demo-test/demo-test.component';
import { PrepTestListingComponent } from './pages/prep-test/prep-test-listing/prep-test-listing.component';
import { PrepTestComponent } from './pages/prep-test/prep-test/prep-test.component';

const routes: Routes =[
  // { path: 'dashboard', pathMatch: 'full', component: DashboardComponent,canActivate: [AuthGuard]  },
  { path: 'questions', pathMatch: 'full', component: AdminQuestionsListingComponent,canActivate: [AuthGuard]  },
  { path: 'categories', pathMatch: 'full', component: CategoriesListingComponent,canActivate: [AuthGuard]  },
  { path: 'mock-test', pathMatch: 'full', component: MockTestComponent,canActivate: [AuthGuard]  },
  { path: 'demo-test', pathMatch: 'full', component: DemoTestComponent,canActivate: [AuthGuard]  },
  { path: 'prep-test-listing', pathMatch: 'full', component: PrepTestListingComponent,canActivate: [AuthGuard]  },
  { path: 'prep-test/:prepTestConfigId', pathMatch: 'full', component: PrepTestComponent,canActivate: [AuthGuard]  },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: LoginComponent,canActivate: [AuthGuard] },

];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes,{
      useHash: true
    })
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
