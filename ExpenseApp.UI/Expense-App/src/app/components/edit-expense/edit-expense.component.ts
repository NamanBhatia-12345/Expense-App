import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpensesService } from 'src/app/services/expenses.service';

@Component({
  selector: 'app-edit-expense',
  templateUrl: './edit-expense.component.html',
  styleUrls: ['./edit-expense.component.css'],
})
export class EditExpenseComponent implements OnInit {
  expenseId: any;
  editExpense = {
    description: '',
    amount: '',
    date: '',
    paidUserBy: '',
  };
  constructor(
    private expenseService: ExpensesService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.expenseId = params['id'];
      this.getExpenseById(this.expenseId);
    });
  }

  private formatDate(date: string): string {
    const d = new Date(date);
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    return `${d.getFullYear()}-${month}-${day}`;
  }
  getExpenseById(expenseId: number): void {
    this.expenseService.getExpensesById(expenseId).subscribe(
      (response: any) => {
        this.editExpense = response.result;
        this.editExpense.date = this.formatDate(response.result.date);
      },
      (error: any) => {
        console.error('Error fetching expense details:', error);
      }
    );
  }

  updateExpense(form: NgForm): void {
    if (form.valid) {
      this.expenseService
        .updateExpense(this.expenseId, this.editExpense)
        .subscribe(
          (response: any) => {
            console.log(response);
            console.log('Expense updated successfully.');
            this.router.navigate(['/all-expenses']);
          },
          (error: any) => {
            console.error('Error updating expense:', error);
          }
        );
    }
  }
}
