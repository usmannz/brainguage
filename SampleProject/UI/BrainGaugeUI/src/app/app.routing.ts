import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AuthGuard } from './services/authguard';
import { LoginComponent } from './pages/login/login.component';
import { AdminQuestionsListingComponent } from './pages/questions/admin-question-listing/admin-question-listing.component';
import { QuestionAssignmentComponent } from './pages/questions/question-assignement/question-assignment.component';
import { RegisterComponent } from './pages/register/register.component';
import { QuizComponent } from './pages/quiz/questions/admin-question-listing/quiz.component';
import { CategoriesListingComponent } from './pages/categories/categories-listing/categories-listing.component';

const routes: Routes =[
  // { path: 'dashboard', pathMatch: 'full', component: DashboardComponent,canActivate: [AuthGuard]  },
  { path: 'questions', pathMatch: 'full', component: AdminQuestionsListingComponent,canActivate: [AuthGuard]  },
  { path: 'questions-assignment', pathMatch: 'full', component: QuestionAssignmentComponent,canActivate: [AuthGuard]  },
  { path: 'quiz', pathMatch: 'full', component: QuizComponent,canActivate: [AuthGuard]  },
  { path: 'categories', pathMatch: 'full', component: CategoriesListingComponent,canActivate: [AuthGuard]  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: AdminQuestionsListingComponent,canActivate: [AuthGuard] },

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
