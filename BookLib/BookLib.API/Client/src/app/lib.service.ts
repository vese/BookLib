import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LibUser, QueueOnBook, LibBook, Notifications, InQueue, BookOnHands } from './LibClasses';
import { HttpClient, HttpParams } from '@angular/common/http';
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
    return this.http.get<LibUser[]>(this.baseUrl + this.contrUrl + "users");
  }

  getUserQueues(username: string): Observable<QueueOnBook[]> {
    return this.http.get<QueueOnBook[]>(this.baseUrl + this.contrUrl + "userqueues", {
      params: new HttpParams().set("username", username)
    });
  }

  getBookOnHands(username: string): Observable<BookOnHands[]> {
    return this.http.get<BookOnHands[]>(this.baseUrl + this.contrUrl + "booksonhands", {
      params: new HttpParams().set("username", username)
    });
  }

  getBooks(): Observable<LibBook[]> {
    return this.http.get<LibBook[]>(this.baseUrl + this.contrUrl + "books");
  }

  giveBook(username: string, bookId: number): any {
    return this.http.post(this.baseUrl + this.contrUrl + "givebook", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  returnBook(username: string, bookId: number): any {
    return this.http.post(this.baseUrl + this.contrUrl + "returnbook", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  putInQueue(username: string, bookId: number): Observable<number> {
    return this.http.post<number>(this.baseUrl + this.contrUrl + "queue", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  removeFromQueue(username: string, bookId: number): any {
    return this.http.delete(this.baseUrl + this.contrUrl + "queue", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  userInQueue(username: string, bookId: number): Observable<InQueue> {
    return this.http.get<InQueue>(this.baseUrl + this.contrUrl + "queue", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  userHasBook(username: string, bookId: number): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + this.contrUrl + "bookgiven", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  getNotifications(username: string): Observable<Notifications> {
    return this.http.get<Notifications>(this.baseUrl + this.contrUrl + "notifications", {
      params: new HttpParams().set("username", username)
    });
  }

  checkNotReturned(): Observable<number> {
    return this.http.post<number>(this.baseUrl + this.contrUrl + "notreturned", {});
  }
}
