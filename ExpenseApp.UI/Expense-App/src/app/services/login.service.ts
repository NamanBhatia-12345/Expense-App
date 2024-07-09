import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { jwtDecode } from "jwt-decode";


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  currentUser : any = null;
  private apiUrl = 'https://localhost:7011/api/auth/login';


  constructor(private http:HttpClient) { }

  login(loginData:any):Observable<any>{
    return this.http.post<any>(this.apiUrl,loginData,{responseType:'json'})
    }

    getToken():string | null{
      return sessionStorage.getItem('token');
    }

     decodeToken(): any {
      const token = this.getToken();
      if (token) {
        try {
          const decoded: any = jwtDecode(token);
          return {
            name: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
            role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
            id: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
            fullName: decoded["Name"] 
          };
        } catch (Error) {
          console.error('Error decoding token:', Error);
          return null;
        }
      }
      return null;
    }

     getUserRole(): string | null {
      const decodedToken: any = this.decodeToken();
      return decodedToken ? decodedToken.role : null; // Assuming roles are stored in the token
    }

     isUser(): boolean {
      const role = this.getUserRole();
      return role === 'User';
    }
    
     isAdmin(): boolean {
      const role = this.getUserRole();
      return role === 'Admin';
    }
  
     isLoggedIn(): boolean {
      return !!this.getToken();
    }
    
    logout(): void {
      sessionStorage.removeItem('token');
    }
}