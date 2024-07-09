import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpensesService } from 'src/app/services/expenses.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-create-expense',
  templateUrl: './create-expense.component.html',
  styleUrls: ['./create-expense.component.css']
})
export class CreateExpenseComponent implements OnInit{

  errorMessage: string = '';
  groupId: any;

  createExpense = {
    description: '',
    amount: '',
    date: '',
    paidUserBy: ''
  };


  constructor(private expenseService:ExpensesService, private loginService:LoginService, private route:ActivatedRoute, private router: Router){}

  user: any = this.loginService.decodeToken();
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.groupId = params['groupId'];
      console.log(this.groupId);
    });
    this.createExpense.paidUserBy = this.user.fullName;
  }



  onCreateExpense(): void{
    if(!this.createExpense.description || !this.createExpense.amount || !this.createExpense.date){
      this.errorMessage = 'Please fill in all fields.';
      return;
    }
    this.expenseService.createExpense(this.groupId,this.createExpense).subscribe(
      (response: any) => {
        debugger;
        if(response.isSuccess){
          console.log('Expense created successfully.');
          this.router.navigate(['/user-dashboard']);
        }
        else{
          alert(response.message);
        }
      },
      (error: any) => {
        console.error(`Error in creating the expense:- ${error}`);
      }
    );
  }

  @HostListener('document:click', ['$event'])
    onDocumentClick(event: Event) {
    this.errorMessage = '';
  }
}
