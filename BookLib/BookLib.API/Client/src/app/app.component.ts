import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent
{
  loggedIn: boolean;
  isAdmin: boolean;

  onLogged(params: any) {
    this.loggedIn = params.loggedIn;
    this.isAdmin = params.role === "admin";
  }
}
