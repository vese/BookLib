import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService, UserDialogData } from '../user.service';

@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.css']
})
export class RegisterDialogComponent {

  hide = true;
  passConfirm: string;
  loggedIn: boolean = false;
  registerFailed: boolean;
  failtureMessage: string;

  constructor(public dialogRef: MatDialogRef<RegisterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDialogData,
    private userService: UserService) {
    this.loggedIn = this.userService.isLoggedIn();
    if (this.loggedIn == null) {
      this.userService.checkLogged().subscribe(res => this.loggedIn = true, error => this.loggedIn = false);
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onRegisterClick(): void {
    if (this.data.pass == this.passConfirm) {
      this.userService.register(this.data).subscribe(res => {
        this.registerFailed = false;
      }, error => {
        this.failtureMessage = "Неверный логин или пароль или пользователь уже существует!";
        this.registerFailed = true;
      });
    }
    else {
      this.registerFailed = true;
      this.failtureMessage = "Введенные пароли не совпадают!";
    }
  }
}
