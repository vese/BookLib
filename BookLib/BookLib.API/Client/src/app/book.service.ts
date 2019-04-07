import { Injectable } from '@angular/core';
import { ListBook, Book } from './BookClasses';
import { Observable, of } from 'rxjs';
import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  contrUrl: string;
  baseUrl: string;

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "books/";
  }

  getBooks(): Observable<ListBook[]> {
    return this.http.get<ListBook[]>(this.baseUrl + this.contrUrl + "filter");
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(this.baseUrl + this.contrUrl, { params: new HttpParams().set("id", "" + id) });
    return of({
      name: "adsd",
      isbn: "sad",
      author: "asd",
      description: "dssssssssssss",
      publisher: "sda",
      category: "ds",
      genre: "da",
      releaseYear: 2017,
      series: "dsa",
      commentsCount: 10,
      averageMark: 5,
      freeCount: 10,
      comments: [
        {
          text: "atlichna",
          mark: 5
        },
        {
          text: "ploho",
          mark: 1
        }]
    });
  }
}
