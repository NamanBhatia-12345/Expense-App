import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {
  users: any[] = [];
  loading: boolean = true;

  constructor(private authService: AuthService, private route: Router) {}

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(): void {
    this.loading = true;
    this.authService.getAllUsers().subscribe(
      (response: any) => {
        if (response.isSuccess) {
          console.log('API Response:', response);
          this.users = response.result;
          this.loading = false;
        } else {
          console.log(response.message);
          this.loading = false;
        }
      },
      (error: any) => {
        console.error('Error in fetching users:', error);
        this.loading = false;
      }
    );
  }
  showUserDetails(userId: string) {
    this.route.navigate(['/user-detail', userId]);
  }

  editUserDetails(userId: string) {
    this.route.navigate(['/edit-user', userId]);
  }

  deleteUserDetails(userId: string): void {
    const isDelete = confirm('Are you sure want to delete');
    if (isDelete) {
      this.authService.deleteUser(userId).subscribe(
        (response: any) => {
          if (response.isSuccess) {
            this.getAllUsers();
            alert('User Deleted Successfully!');
          } else {
            alert(response.message);
          }
        },
        (error: any) => {
          console.error(`Error in deleting user details with Id:- ${userId}`, error);
        }
      );
    }
  }
}
