import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  name: string;
  password: string;

  constructor(private userService: UserService,
    public dialog: MatDialog) { }

  openLoginDialog(): void {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: '400px',
      data: { name: this.name, pass: this.password }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.name = this.userService.getName();
    });
  }

  ngOnInit() {
  }

}
