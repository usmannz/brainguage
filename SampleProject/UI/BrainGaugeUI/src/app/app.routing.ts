import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AuthGuard } from './services/authguard';
import { LoginComponent } from './pages/login/login.component';
import { QuestionsComponent } from './pages/questions/questions.component';

const routes: Routes =[
  { path: 'dashboard', pathMatch: 'full', component: DashboardComponent,canActivate: [AuthGuard]  },
  { path: 'questions', pathMatch: 'full', component: QuestionsComponent,canActivate: [AuthGuard]  },
  { path: 'login', component: LoginComponent },
  { path: '**', component: DashboardComponent,canActivate: [AuthGuard] },

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
