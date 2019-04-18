import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LibUser, QueueOnBook, LibBook, Notifications, InQueue, BookOnHands } from './LibClasses';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class LibService {

  contrUrl: string;
  baseUrl: string;

  constructor(private http: HttpClient,
    private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "lib/";
  }

  getUsers(): Observable<LibUser[]> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<LibUser[]>(this.baseUrl + this.contrUrl + "users", { headers: headers });
  }

  getUserQueues(username: string): Observable<QueueOnBook[]> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<QueueOnBook[]>(this.baseUrl + this.contrUrl + "userqueues", {
      params: new HttpParams().set("username", username),
      headers: headers
    });
  }

  getBookOnHands(username: string): Observable<BookOnHands[]> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<BookOnHands[]>(this.baseUrl + this.contrUrl + "booksonhands", {
      params: new HttpParams().set("username", username),
      headers: headers
    });
  }

  getBooks(): Observable<LibBook[]> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<LibBook[]>(this.baseUrl + this.contrUrl + "books", { headers: headers });
  }

  giveBook(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.post(this.baseUrl + this.contrUrl + "givebook", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  returnBook(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.post(this.baseUrl + this.contrUrl + "returnbook", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  putInQueue(username: string, bookId: number): Observable<number> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.post<number>(this.baseUrl + this.contrUrl + "queue", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  removeFromQueue(username: string, bookId: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.delete(this.baseUrl + this.contrUrl + "queue", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  userInQueue(username: string, bookId: number): Observable<InQueue> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<InQueue>(this.baseUrl + this.contrUrl + "queue", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  userHasBook(username: string, bookId: number): Observable<boolean> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<boolean>(this.baseUrl + this.contrUrl + "bookgiven", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId),
      headers: headers
    });
  }

  getNotifications(username: string): Observable<Notifications> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.get<Notifications>(this.baseUrl + this.contrUrl + "notifications", {
      params: new HttpParams().set("username", username),
      headers: headers
    });
  }

  checkNotReturned(): Observable<number> {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    return this.http.post<number>(this.baseUrl + this.contrUrl + "notreturned", {}, { headers: headers });
  }
}
