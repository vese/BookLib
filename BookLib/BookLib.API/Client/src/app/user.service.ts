import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LoginResult } from './AuthClasses';

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

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "auth/";
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  checkLogged(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    let res = this.http.get(this.baseUrl + this.contrUrl, { headers });
    res.subscribe(res => {
      this.loggedIn = true;
      this.logChangedSource.next(true);
    }, error => {
      if (this.loggedIn) {
        this.logout();
      }
    });
    return res;
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
