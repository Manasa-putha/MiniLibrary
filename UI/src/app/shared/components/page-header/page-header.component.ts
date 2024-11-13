import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { UserStateService } from '../../services/user-state.service';

@Component({
  selector: 'page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss'],
})
export class PageHeaderComponent implements OnInit {
  loggedIn: boolean = false;
  name: string = '';
  tokensAvailable: number = 0;

  constructor(
    private apiService: ApiService,
    private userStateService: UserStateService
  ) {
    this.userStateService.getTokensAvailable().subscribe((tokens) => {
      this.tokensAvailable = tokens;
    });
  }

  ngOnInit() {
    if (this.apiService.isLoggedIn()) {
      this.apiService.getUserById().subscribe(user => {
        if (user) {
          this.loggedIn = true;
          this.name = user.name;
          this.tokensAvailable = user.tokensAvailable;
        }
      });
    }

    this.apiService.userStatus.subscribe({
      next: (res) => {
        if (res == 'loggedIn') {
          this.apiService.getUserById().subscribe(user => {
            if (user) {
              this.loggedIn = true;
              this.name = user.name;
              this.tokensAvailable = user.tokensAvailable;
            }
          });
        } else {
          this.loggedIn = false;
          this.name = '';
          this.tokensAvailable = 0;
        }
      },
    });
  }

  logout() {
    this.apiService.logOut();
  }
}
