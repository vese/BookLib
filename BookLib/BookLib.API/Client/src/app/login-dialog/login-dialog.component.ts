import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { UserService, UserDialogData } from '../user.service';
import { FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent implements OnInit, OnDestroy {

  subscription: Subscription;

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
    this.subscription = this.userService.logChanged$.subscribe(
      res => {
        this.loggedIn = res;
      });
  }

  ngOnInit() {
    this.userService.isLoggedIn();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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
      this.userService.login(data).subscribe(res => { }, error => this.loginFailed = true);
    }
  }
}
