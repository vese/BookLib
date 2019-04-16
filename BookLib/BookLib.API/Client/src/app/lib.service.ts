import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LibUser, QueueOnBook, LibBook } from './LibClasses';
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

  getUserQueues(): Observable<QueueOnBook[]> {
    throw new Error("Method not implemented.");
  }

  getBooks(): Observable<LibBook[]> {
    throw new Error("Method not implemented.");
  }

  giveBook(username: string, bookId: number): any {
    throw new Error("Method not implemented.");
  }

  returnBook(username: string, bookId: number): any {
    throw new Error("Method not implemented.");
  }

  putInQueueBook(username: string, bookId: number): any {
    throw new Error("Method not implemented.");
  }

  userHasBook(username: string, bookId: number): Observable<boolean> {
    throw new Error("Method not implemented.");
  }
}
