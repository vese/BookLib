import { Component, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { UserService } from '../user.service';
import { RegisterDialogComponent } from '../register-dialog/register-dialog.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnDestroy {
  subscription: Subscription;
  name: string;
  password: string;
  loggedIn: boolean;

  constructor(private userService: UserService,
    public dialog: MatDialog) {
    this.subscription = this.userService.logChanged$.subscribe(
      res => this.logChanged(res));
    this.logChanged(this.userService.isAuthenticated());
  }

  logChanged(res: boolean): void {
    this.loggedIn = res;
    if (res) {
      this.name = localStorage.getItem("name");
    }
  }

  openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: '400px'
    });
  }

  openRegisterDialog(): void {
    this.dialog.open(RegisterDialogComponent, {
      width: '400px'
    });
  }

  logout(): void {
    this.userService.logout();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
