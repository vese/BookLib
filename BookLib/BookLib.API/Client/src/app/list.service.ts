import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { Param } from './bookclasses';

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

  getScheduledList(username: string): Observable<Param[]> {
      return this.http.get<Param[]>(this.baseUrl + this.contrUrl + "favourite", {
      params: new HttpParams().set("username", username)
    });
  }

  getReadList(username: string): Observable<Param[]> {
    return this.http.get<Param[]>(this.baseUrl + this.contrUrl + "read", {
      params: new HttpParams().set("username", username)
    });
  }

  inScheduled(username: string, bookId: number): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + this.contrUrl + "infavourite", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  inRead(username: string, bookId: number): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + this.contrUrl + "inread", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  addToScheduled(username: string, bookId: number): any {
    return this.http.post(this.baseUrl + this.contrUrl + "favourite", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  addToRead(username: string, bookId: number): any {
    return this.http.post(this.baseUrl + this.contrUrl + "read", {}, {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  removeFromScheduled(username: string, bookId: number): any {
    return this.http.delete(this.baseUrl + this.contrUrl + "favourite", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }

  removeFromRead(username: string, bookId: number): any {
    return this.http.delete(this.baseUrl + this.contrUrl + "read", {
      params: new HttpParams().set("username", username).set("bookId", "" + bookId)
    });
  }
}
