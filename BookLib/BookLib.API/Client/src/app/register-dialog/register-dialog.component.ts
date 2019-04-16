import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { UserService, UserDialogData } from '../user.service';
import { FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.css']
})
export class RegisterDialogComponent {
  hide = true;
  loggedIn: boolean = false;
  registerFailed: boolean;
  failtureMessages: string[];

  loginFormControl = new FormControl("", [
    Validators.required,
    Validators.minLength(4),
    Validators.maxLength(20)
  ]);
  passwordFormControl = new FormControl("", [
    Validators.required,
    Validators.minLength(6)
  ]);
  passwordConfirmFormControl = new FormControl("", [
    Validators.required,
    this.checkPasswordConfirmation()
  ]);

  checkPasswordConfirmation(): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {
      if (c.value != this.passwordFormControl.value) {
        return { 'pass': true };
      }
      return null;
    };
  }

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

  getPasswordConfirmErrorMessage() {
    return this.passwordConfirmFormControl.hasError('required') ? 'Введите значение' :
      this.passwordConfirmFormControl.hasError('pass') ? 'Пароли должны совпадать' :
        '';
  }

  constructor(public dialogRef: MatDialogRef<RegisterDialogComponent>,
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
    this.failtureMessages = [];
    if (this.loginFormControl.valid && this.passwordFormControl.valid && this.passwordConfirmFormControl.valid) {
      let data: UserDialogData = {
        name: this.loginFormControl.value,
        pass: this.passwordFormControl.value
      }
      this.userService.register(data).subscribe(res => {
        this.dialogRef.close();
      }, error => {
        if (error.error.Name) {
          error.error.Name.forEach(el => this.failtureMessages.push(el));
        }
        if (error.error.Pass) {
          error.error.Pass.forEach(el => this.failtureMessages.push(el));
        }
        if (error.error.Model) {
          error.error.Model.forEach(el => this.failtureMessages.push(el));
        }
        this.registerFailed = true;
      });
    }
  }
}
