import { LoginService } from 'src/app/services/login.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(public loginService:LoginService,private route:Router){}

  onLogoutClick() {
    this.loginService.logout();
    this.route.navigate(['/']); // Navigate to home route
  }
}
