import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { HttpClient, HttpParams } from '@angular/common/http';
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

  getComments(bookId: number, beginNumber: number, number: number, order: string): Observable<BookComment[]> {
    let params = new HttpParams();
    if (order) {
      params = params.set("order", "" + order);
    }
    return this.http.get<BookComment[]>(this.baseUrl + this.contrUrl, {
      params: params.set("bookId", "" + bookId).set("beginNumber", "" + beginNumber).set("number", "" + number)
    });
  }
}
