import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService, UserDialogData } from '../user.service';
import { FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';

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
    Validators.required
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

  getPasswordConfirmErrorMessage() {
    return this.passwordConfirmFormControl.hasError('required') ? 'Введите значение' :
      '';
  }

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
