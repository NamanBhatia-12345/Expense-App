import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ExpensesService } from 'src/app/services/expenses.service';

@Component({
  selector: 'app-expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.css'],
})
export class ExpenseListComponent implements OnInit {
  expenses: any[] = [];
  loading: boolean = true;
  constructor(
    private expenseService: ExpensesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAllExpenses();
  }

  getAllExpenses(): void {
    this.loading = true;
    this.expenseService.getAllExpenses().subscribe(
      (response: any) => {
        console.log('API Response:', response);
        this.expenses = response.result;
        this.loading = false;
      },
      (error: any) => {
        console.error('Error fetching expenses:', error);
        this.loading = false;
      }
    );
  }

  showExpenseDetails(expenseId: number) {
    this.router.navigate(['/expense-detail', expenseId]);
  }

  updateExpenseDetails(expense:any){
    const expenseId = expense.id;
    if(expense.isSettled){
      alert(`Expense is settled successfully!`);
    }
    else{
    this.router.navigate(['/update-expense', expenseId]);
    }
  }
}
