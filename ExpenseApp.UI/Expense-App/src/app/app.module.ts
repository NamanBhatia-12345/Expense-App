import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { GroupListComponent } from './components/group-list/group-list.component';
import { GroupComponent } from './components/group/group.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { CustomInterceptor } from './interceptors/custom.interceptor';
import { UserComponent } from './components/user/user.component';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { ExpenseListComponent } from './components/expense-list/expense-list.component';
import { EditExpenseComponent } from './components/edit-expense/edit-expense.component';
import { ExpenseComponent } from './components/expense/expense.component';
import { CreateGroupComponent } from './components/create-group/create-group.component';
import { CreateExpenseComponent } from './components/create-expense/create-expense.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    GroupListComponent,
    GroupComponent,
    AdminDashboardComponent,
    UserDashboardComponent,
    UserComponent,
    EditUserComponent,
    ExpenseListComponent,
    EditExpenseComponent,
    ExpenseComponent,
    CreateGroupComponent,
    CreateExpenseComponent,
    PageNotFoundComponent,
    UnauthorizedComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: CustomInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
