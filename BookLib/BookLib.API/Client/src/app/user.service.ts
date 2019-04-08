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
  private name: string;
  
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "auth/";
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  getName(): string {
    return this.name;
  }

  login(data: UserDialogData): Observable<LoginResult> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    let res = this.http.post<LoginResult>(this.baseUrl + this.contrUrl + 'login/', data, { headers });
    res.subscribe(r => {
      localStorage.setItem('auth_token', r.auth_token);
      this.loggedIn = true;
      this.name = r.username;
    })
    return res;
  }
}
