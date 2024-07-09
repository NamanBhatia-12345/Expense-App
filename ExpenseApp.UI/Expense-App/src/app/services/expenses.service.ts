import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  private apiUrl = 'https://localhost:7011/api/expense';

  constructor(private http:HttpClient) { }

  getAllExpenses(): Observable<any>{
    let url = `${this.apiUrl}/get-all-expenses`;
    return this.http.get<any>(url,{responseType:'json'});
  }

  getExpensesById(expenseId:number): Observable<any>{
    let url = `${this.apiUrl}/${expenseId}`;
    return this.http.get<any>(url,{responseType:'json'});
  }

  getUserExpenses():Observable<any>{
    let url = `${this.apiUrl}/user-expenses`;
    return this.http.get<any>(url,{responseType:'json'});
  }

  createExpense(groupId:number,createGroup:any):Observable<any>{
    let url = `${this.apiUrl}/add-expense/${groupId}`;
    return this.http.post<any>(url,createGroup,{responseType:'json'});
  }

  updateExpense(expenseId:number,updateExpense:any): Observable<any>{
    let url = `${this.apiUrl}/update-expense/${expenseId}`;
    return this.http.put<any>(url,updateExpense,{responseType:'json'});
  }

  settleExpense(expenseId:number): Observable<any>{
    let url = `${this.apiUrl}/expense-settle/${expenseId}`;
    return this.http.post<any>(url,{},{responseType:'json'});
  }

}
