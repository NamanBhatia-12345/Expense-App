import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit{

  userId: any;
  editUser = {
      fullName: '',
      email: '',
      phoneNumber: ''
  }


  constructor(private authService:AuthService, private route:ActivatedRoute, private router:Router){}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = params['id'];
      this.getUserDetailsById(this.userId);
    });
    
  }

  getUserDetailsById(userId:string): void{
    this.authService.getUserById(userId).subscribe(
      (response:any) => {
        this.editUser = response.result;
      },
      (error:any) => {
        console.error('Error fetching user details:', error);
      }
    );
  }

  updateUser(form:NgForm): void{
    if(form.valid){
      this.authService.updateUser(this.userId,this.editUser).subscribe(
        (response:any) => {
          console.log('User updated successfully.');
          this.router.navigate(['/admin-dashboard']);
        },
        (error:any) => {
          console.error('Error updating user:', error);
        }
      );
    }

  }
}
