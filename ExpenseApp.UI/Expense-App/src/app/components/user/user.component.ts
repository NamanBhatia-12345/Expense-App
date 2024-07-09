import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  user: any | undefined;
  loading: boolean = true;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const userId = params['id'];
      this.loading = true;

      this.authService.getUserById(userId).subscribe(
        (response: any) => {
          console.log(response);
          this.user = response.result;
          this.loading = false;
        },
        (error: any) => {
          console.error('Error fetching user details:', error);
          this.loading = false;
        }
      );
    });
  }
}
