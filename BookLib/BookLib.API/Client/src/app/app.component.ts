import { Component } from '@angular/core';
import { UserService } from './user.service';
import { Roles } from './roles';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  loggedIn: boolean;
  isAdmin: boolean;

  constructor(private userService: UserService) {
    this.userService.logChanged$.subscribe(res => this.logChanged(res));
    this.logChanged(this.userService.isAuthenticated());
  }

  logChanged(res: boolean): void {
    this.loggedIn = res;
    if (res) {
      this.isAdmin = localStorage.getItem("role") === Roles.Admin;
    }
  }
}
