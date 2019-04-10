import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
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
  
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "auth/";
  }

  isLoggedIn(): boolean {
    if (this.loggedIn == null) {
      this.checkLogged().subscribe(res => this.loggedIn = true, error => this.logout());
    }
    return this.loggedIn;
  }

  checkLogged(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    let res = this.http.get(this.baseUrl + this.contrUrl, { headers });
    return res;
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('name');
    localStorage.removeItem('role');
    this.loggedIn = false;
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
    }, error => this.loggedIn = false)
    return res;
  }

  register(data: UserDialogData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let res = this.http.post(this.baseUrl + this.contrUrl + 'register/', data, { headers });
    return res;
  }
}
