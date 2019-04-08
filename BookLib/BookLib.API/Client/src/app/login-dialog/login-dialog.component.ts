import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService, UserDialogData } from '../user.service';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent {

  hide = true;
  loggedIn: boolean;
  loginFailed: boolean = false;

  constructor(public dialogRef: MatDialogRef<LoginDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDialogData,
    private userService: UserService) {
    this.loggedIn = this.userService.isLoggedIn();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onLoginClick(): void {
    this.userService.login(this.data).subscribe(res => {
      this.loggedIn = true;
      this.loginFailed = false;
    }, error => {
      this.loggedIn = false;
      this.loginFailed = true;
    });
  }
}
