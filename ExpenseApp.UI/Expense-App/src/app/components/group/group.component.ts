import { GroupService } from './../../services/group.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit{

  group: any|undefined;
  loading: boolean = true;
  constructor(private route:ActivatedRoute, private groupService:GroupService, private router:Router, public loginService:LoginService){}

  ngOnInit(): void {
    this.loading = true;
    this.route.params.subscribe(params => {
      const groupId = params['id'];
      this.groupService.getGroupById(groupId).subscribe(
        (response:any) => {
          this.group = response.result;
          this.loading = false;
        },
        (error) => {
          console.error('Error fetching group details:', error);
          this.loading = false;
        }
      )
    })
  }

  createExpense(id:number){
    this.router.navigate(['/create-expense',id]);
  }
  deleteGroup(groupId:number){
    const isDelete = confirm('Are you sure want to the group');
    if(isDelete){
      this.groupService.deleteGroup(groupId).subscribe(
        (response: any) => {
          if(response.isSuccess){
            alert('Group Deleted Successfully!');
            this.router.navigate(['/']);
          }
          else{
            alert(response.message);
          }
        },
        (error: any) => {
          console.error(`Error in deleting the group with Id:- ${groupId}`, error);
        }
      );

    }
  }

}
