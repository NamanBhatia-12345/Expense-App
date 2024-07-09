import { Component, OnInit } from '@angular/core';
import { ExpensesService } from 'src/app/services/expenses.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit{

  expenses: any[] = [];
  loading: boolean = true;

  constructor(private expenseService:ExpensesService){}
  ngOnInit(): void {
    this.getLoggedInUserExpenses();
    
  }
  getLoggedInUserExpenses(): void{
    this.loading = true;
    this.expenseService.getUserExpenses().subscribe(
      (response:any) => {
        this.expenses = response.result;
        this.loading = false;
      },
      (error: any) => {
        console.error('Error in fetching expenses of logged in user:', error);
        this.loading = false;
      }
    );
  }

  onSettleExpense(expenseId:any){
    const settle = confirm('Are you sure want to settle the expense');
    if(settle){
      this.expenseService.settleExpense(expenseId).subscribe(
        (response: any) => {
          if(response.isSuccess){
            this.getLoggedInUserExpenses();
            alert('Expense Settled Successfully!');
          }else{
            alert(response.message);
          }
        },
        (error: any) => {
          console.error(`Error in settling the expense with Id:- ${expenseId}`, error);
        }
      );
    }
  }

}
