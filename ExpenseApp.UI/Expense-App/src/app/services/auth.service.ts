import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:7011/api/auth';

  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<any> {
    let url = `${this.apiUrl}/get-all-users`;
    return this.http.get<any>(url, { responseType: 'json' });
  }

  getUserById(userId: string): Observable<any> {
    let url = `${this.apiUrl}/user-detail/${userId}`;
    return this.http.get<any>(url, { responseType: 'json' });
  }

  updateUser(userId: string, updatedUser: any): Observable<any> {
    const url = `${this.apiUrl}/update-user/${userId}`;
    return this.http.put<any>(url, updatedUser, { responseType: 'json' });
  }

  deleteUser(userId: string): Observable<any> {
    let url = `${this.apiUrl}/delete-users/${userId}`;
    return this.http.delete<any>(url, { responseType: 'json' });
  }
}
