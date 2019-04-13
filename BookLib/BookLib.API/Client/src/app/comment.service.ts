import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { BookComment } from './BookClasses';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  contrUrl: string;
  baseUrl: string;

  constructor(private http: HttpClient,
    private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "comments/";
  }

  getComments(bookId: number, start: number, count: number, order: string): Observable<BookComment[]> {
    let params = new HttpParams();
    if (order) {
      params = params.set("order", "" + order);
    }
    return this.http.get<BookComment[]>(this.baseUrl + this.contrUrl, {
      params: params.set("bookId", "" + bookId).set("start", "" + start).set("count", "" + count)
    });
  }

  addComment(text: string, mark: number, name: string, id: number): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);
    console.log(headers);
    console.log();
    return this.http.post(this.baseUrl + this.contrUrl, {}, {
      params: new HttpParams().set("text", "" + text).set("mark", "" + mark).set("username", "" + name).set("bookId", "" + id),
      headers: headers
    });
  }

  commentExists(name: string, id: number): any {
    return this.http.get(this.baseUrl + this.contrUrl + "exists", {
      params: new HttpParams().set("username", "" + name).set("bookId", "" + id)
    })
  }
}
