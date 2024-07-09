import { Router } from '@angular/router';
import { GroupService } from './../../services/group.service';
import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.css'],
})
export class GroupListComponent implements OnInit {
  groups: any[] = [];
  loading: boolean = true;
  constructor(
    private groupService: GroupService,
    private router: Router,
    public loginService: LoginService
  ) {}

  ngOnInit(): void {
    this.getAllGroups();
  }

  getAllGroups(): void {
    this.loading = true;
    this.groupService.getGroups().subscribe(
      (response: any) => {
        console.log('API Response:', response);
        this.groups = response.result;
        this.loading = false;
      },
      (error) => {
        console.error('Error fetching books:', error);
        this.loading = false;
      }
    );
  }

  addMembers(groupId: number) {
    this.groupService.addMembers(groupId).subscribe(
      (response: any) => {
        if (response.isSuccess) {
          alert('Member added successfully');
          this.router.navigate(['/group-detail', groupId]);
        } else {
          alert(response.message);
        }
      },
      (error: any) => {
        console.error(
          `Error in adding member in the group with Id:${groupId}`,
          error
        );
      }
    );
  }

  showGroupDetails(groupId: number) {
    this.router.navigate(['/group-detail', groupId]);
  }
}
