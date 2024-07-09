import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  errorMessage: string = '';
  loginData = {
    email: '',
    password: ''
  };

  constructor(private loginService:LoginService, private route:Router){}

  onLoginClick() : void{

    if(!this.loginData.email || !this.loginData.password){
      this.errorMessage = 'Please fill in all fields.';
      return;
    }

    this.loginService.login(this.loginData).subscribe(
      (response:any) => {
        if(response.isSuccess){
          sessionStorage.setItem('token',response.result);
          const decodedToken = this.loginService.decodeToken();
          if(this.loginService.isAdmin())
            this.route.navigate(['/admin-dashboard']);
          else
           this.route.navigate(['/user-dashboard']);
        }else{
          this.errorMessage = response.message;
        }
      },
      (error: any) => {
       console.log(`Failed to login. Please try again.${error}`); // Handle HTTP error
      }
    );
    
  }
  @HostListener('document:click', ['$event'])
    onDocumentClick(event: Event) {
    this.errorMessage = '';
  }
}
