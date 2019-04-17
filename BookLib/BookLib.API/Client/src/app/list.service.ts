import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { Param } from './BookClasses';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  contrUrl: string;
  baseUrl: string;

  constructor(private http: HttpClient,
    private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "list/";
  }

  getSheduledList(username: string): Observable<Param[]> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<Param[]>(this.baseUrl + this.contrUrl + "sheduled", {
      params: new HttpParams().set("username", username),
      headers: headers
    });
  }

  getReadList(username: string): Observable<Param[]> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<Param[]>(this.baseUrl + this.contrUrl + "read", {
      params: new HttpParams().set("username", username),
      headers: headers
    });
  }

  inSheduled(username: string, bookId: number): Observable<boolean> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<boolean>(this.baseUrl + this.contrUrl + "insheduled", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  inRead(username: string, bookId: number): Observable<boolean> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<boolean>(this.baseUrl + this.contrUrl + "inread", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  addToSheduled(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.post(this.baseUrl + this.contrUrl + "sheduled", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  addToRead(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.post(this.baseUrl + this.contrUrl + "read", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  removeFromSheduled(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.delete(this.baseUrl + this.contrUrl + "sheduled", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  removeFromRead(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.delete(this.baseUrl + this.contrUrl + "read", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }
}
