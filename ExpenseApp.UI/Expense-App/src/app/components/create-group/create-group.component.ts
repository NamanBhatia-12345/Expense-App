import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css'],
})
export class CreateGroupComponent {
  errorMessage: string = '';

  createGroup = {
    name: '',
    description: '',
  };

  constructor(private groupService: GroupService, private route: Router) {}

  onCreateGroup(): void {
    if (!this.createGroup.name || !this.createGroup.description) {
      this.errorMessage = 'Please fill in all fields.';
      return;
    }

    this.groupService.createGroup(this.createGroup).subscribe(
      (response: any) => {
        console.log(response);
        if(response.isSuccess){
          console.log(response.result);
          console.log('Group created successfully.');
          this.route.navigate(['/']);
        }
        else{
          this.errorMessage = response.message;
        }
      },
      (error: any) => {
        this.errorMessage = 'Failed to create the group. Please try again.'; // Handle HTTP error
      });
  }
  @HostListener('document:click', ['$event'])
    onDocumentClick(event: Event) {
    this.errorMessage = '';
  }
}
