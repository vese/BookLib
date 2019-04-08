import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { UserService } from '../user.service';
import { RegisterDialogComponent } from '../register-dialog/register-dialog.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  name: string;
  password: string;
  loggedIn: boolean;

  constructor(private userService: UserService,
    public dialog: MatDialog) {
    this.loggedIn = this.userService.isLoggedIn();
    if (this.loggedIn == null) {
      this.userService.checkLogged().subscribe(res => this.loggedIn = true, error => this.loggedIn = false);
    }
  }

  openLoginDialog(): void {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: '400px',
      data: { name: this.name, pass: this.password }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loggedIn = this.userService.isLoggedIn();
    });
  }

  openRegisterDialog(): void {
    const dialogRef = this.dialog.open(RegisterDialogComponent, {
      width: '400px',
      data: { name: this.name, pass: this.password }
    });

    //dialogRef.afterClosed().subscribe(result => {
    //  this.loggedIn = this.userService.isLoggedIn();
    //});
  }

  logout(): void {
    this.loggedIn = false;
    this.userService.logout();
  }

  ngOnInit() {
  }

}
