import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { UserService, UserDialogData } from '../user.service';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent {

  hide = true;
  loggedIn: boolean = false;
  loginFailed: boolean = false;

  loginFormControl = new FormControl("", [
    Validators.required,
    Validators.minLength(4),
    Validators.maxLength(20)
  ]);
  passwordFormControl = new FormControl("", [
    Validators.required,
    Validators.minLength(6)
  ]);

  getLoginErrorMessage() {
    return this.loginFormControl.hasError('required') ? 'Введите значение' :
      this.loginFormControl.hasError('minlength') ? 'Длина поля должна быть минимум 4 символов' :
        this.loginFormControl.hasError('maxlength') ? 'Длина поля должна быть максимум 20 символов' :
          '';
  }

  getPasswordErrorMessage() {
    return this.passwordFormControl.hasError('required') ? 'Введите значение' :
      this.passwordFormControl.hasError('minlength') ? 'Длина поля должна быть минимум 6 символов' :
        '';
  }

  constructor(public dialogRef: MatDialogRef<LoginDialogComponent>,
    private userService: UserService) {
    this.loggedIn = this.userService.isLoggedIn();
    if (this.loggedIn == null) {
      this.userService.checkLogged().subscribe(res => this.loggedIn = true, error => this.loggedIn = false);
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onLoginClick(): void {
    if (this.loginFormControl.valid && this.passwordFormControl.valid) {
      let data: UserDialogData = {
        name: this.loginFormControl.value,
        pass: this.passwordFormControl.value
      }
      this.userService.login(data).subscribe(res => {
        this.loggedIn = true;
        this.loginFailed = false;
      }, error => {
        this.loggedIn = false;
        this.loginFailed = true;
      });
    }
  }
}
