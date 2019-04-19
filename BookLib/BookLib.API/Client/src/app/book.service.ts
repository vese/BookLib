import { Injectable } from '@angular/core';
import { Book, FilterParams, ViewBook, BooksList } from './bookclasses';
import { Observable } from 'rxjs';
import { ConfigService } from './config.service';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class BookService {

  contrUrl: string;
  baseUrl: string;

  constructor(private http: HttpClient,
    private configService: ConfigService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "books/";
  }

  getBooks(inNameString: string,
    releaseYear: number,
    authorId: number,
    publisherId: number,
    seriesId: number,
    categoryId: number,
    genreId: number,
    hasFree: boolean,
    sortProperty: string,
    sortOrder: string,
    start: number,
    count: number): Observable<BooksList> {
    let params: HttpParams = new HttpParams();

    if (inNameString) {
      params = params.set("inName", inNameString);
    }
    if (releaseYear) {
      params = params.set("releaseYear", "" + releaseYear);
    }
    if (authorId) {
      params = params.set("authorId", "" + authorId);
    }
    if (publisherId) {
      params = params.set("publisherId", "" + publisherId);
    }
    if (seriesId) {
      params = params.set("seriesId", "" + seriesId);
    }
    if (categoryId) {
      params = params.set("categoryId", "" + categoryId);
    }
    if (genreId) {
      params = params.set("genreId", "" + genreId);
    }
    if (hasFree) {
      params = params.set("hasFree", "" + hasFree);
    }
    if (sortProperty) {
      params = params.set("sort", "" + sortProperty);
    }
    if (sortOrder) {
      params = params.set("order", "" + sortOrder);
    }

    return this.http.get<BooksList>(this.baseUrl + this.contrUrl + "filter",
      {
        params: params.set("start", "" + start).set("count", "" + count)
      });
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(this.baseUrl + this.contrUrl, { params: new HttpParams().set("id", "" + id) });
  }

  getFilterParams(): Observable<FilterParams> {
    return this.http.get<FilterParams>(this.baseUrl + this.contrUrl + "filterparams");
  }

  addBook(count: number, data: ViewBook): any {
    return this.http.post(this.baseUrl + this.contrUrl, data, {
      params: new HttpParams().set("count", "" + count)
    });
  }

  editBook(id: number, data: ViewBook): any {
    return this.http.put(this.baseUrl + this.contrUrl, data, {
      params: new HttpParams().set("id", "" + id)
    });
  }

  deleteBook(id: number): any {
    return this.http.delete(this.baseUrl + this.contrUrl, {
      params: new HttpParams().set("id", "" + id)
    });
  }
}
