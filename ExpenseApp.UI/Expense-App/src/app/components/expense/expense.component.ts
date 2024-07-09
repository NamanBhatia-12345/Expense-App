import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpensesService } from 'src/app/services/expenses.service';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.css'],
})
export class ExpenseComponent implements OnInit {
  expense: any | undefined;
  loading:boolean = true;

  constructor(
    private expenseService: ExpensesService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.route.params.subscribe((params) => {
      const expenseId = params['id'];
      this.expenseService.getExpensesById(expenseId).subscribe(
        (response: any) => {
          this.expense = response.result;
          this.loading = false;
        },
        (error: any) => {
          console.error('Error fetching expense details:', error);
          this.loading = false;
        }
      );
    });
  }
}
