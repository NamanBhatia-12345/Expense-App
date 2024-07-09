import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { GroupListComponent } from './components/group-list/group-list.component';
import { GroupComponent } from './components/group/group.component';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { UserComponent } from './components/user/user.component';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { ExpenseListComponent } from './components/expense-list/expense-list.component';
import { EditExpenseComponent } from './components/edit-expense/edit-expense.component';
import { ExpenseComponent } from './components/expense/expense.component';
import { CreateGroupComponent } from './components/create-group/create-group.component';
import { CreateExpenseComponent } from './components/create-expense/create-expense.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { CanActivateGuardService } from './guards/can-activate-guard.service';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';

const routes: Routes = 
[

  {
    path: '',
    component: GroupListComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'group-detail/:id',
    component: GroupComponent,
  },
  {
    path: 'user-dashboard',
    component: UserDashboardComponent,
    canActivate: [CanActivateGuardService], data:{Role: "User"}
  },
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent,
    canActivate: [CanActivateGuardService], data:{Role: "Admin"}
  },
  {
    path: 'user-detail/:id',
    component: UserComponent,
    canActivate: [CanActivateGuardService], data:{Role: "Admin"}
  },
  {
    path: 'edit-user/:id',
    component: EditUserComponent,
    canActivate: [CanActivateGuardService], data:{Role: "Admin"}
  },
  {
    path: 'all-expenses',
    component: ExpenseListComponent,
    canActivate: [CanActivateGuardService], data:{Role: "Admin"}
  },
  {
    path: 'expense-detail/:id',
    component: ExpenseComponent,
    canActivate: [CanActivateGuardService], data:{Role: "Admin"}
  },
  {
    path: 'update-expense/:id',
    component: EditExpenseComponent,
    canActivate: [CanActivateGuardService], data:{Role: "Admin"}
  },
  {
    path: 'create-group',
    component: CreateGroupComponent,
    canActivate: [CanActivateGuardService], data:{Role: "User"}
  },
  {
    path: 'create-expense/:groupId',
    component: CreateExpenseComponent,
    canActivate: [CanActivateGuardService], data:{Role: "User"}
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
