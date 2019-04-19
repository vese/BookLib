import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginResult } from './AuthClasses';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Roles } from './roles';

export interface UserDialogData {
  name: string;
  pass: string;
}

@Injectable({
  providedIn: 'root'
})

export class UserService {

  contrUrl: string;
  baseUrl: string;

  private loggedIn: boolean;
  private logChangedSource = new Subject<boolean>();
  logChanged$ = this.logChangedSource.asObservable();

  constructor(
    private http: HttpClient,
    private configService: ConfigService,
    private router: Router) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "auth/";
  }

  getToken(): string {
    let token: string = localStorage.getItem("auth_token");
    return token;
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    const helper = new JwtHelperService();
    let logged = !helper.isTokenExpired(token);
    if (this.loggedIn && !logged) {
      this.logout();
    }
    return logged;
  }

  public isAdmin(): boolean {
    return localStorage.getItem("role") === Roles.Admin;
  }

  redirectToHome(): void {
    this.router.navigate(['/home']);
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('name');
    localStorage.removeItem('role');
    this.loggedIn = false;
    this.logChangedSource.next(false);
  }

  login(data: UserDialogData): Observable<LoginResult> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let res = this.http.post<LoginResult>(this.baseUrl + this.contrUrl + 'login/', data, { headers });
    res.subscribe(r => {
      localStorage.setItem('auth_token', r.auth_token);
      localStorage.setItem('name', r.username);
      localStorage.setItem('role', r.role);
      this.loggedIn = true;
      this.logChangedSource.next(true);
    }, error => {
      this.loggedIn = false;
    })
    return res;
  }

  register(data: UserDialogData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let res = this.http.post(this.baseUrl + this.contrUrl + 'register/', data, { headers });
    return res;
  }
}
