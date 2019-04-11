import { Injectable } from '@angular/core';
import { ListBook, Book, FilterParams, ViewBook } from './BookClasses';
import { Observable, of } from 'rxjs';
import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  contrUrl: string;
  baseUrl: string;

  constructor(private http: HttpClient,
    private configService: ConfigService,
    private userService: UserService) {
    this.baseUrl = this.configService.getApiURI();
    this.contrUrl = "books/";
  }

  getBooks(inNameString: string,
    selectedReleaseYear: number,
    selectedAuthorId: number,
    selectedPublisherId: number,
    selectedSeriesId: number,
    selectedCategoryId: number,
    selectedGenreId: number,
    selectedHasFree: boolean,
    selectedSortProperty: string,
    selectedSortOrder: string): Observable<ListBook[]> {
    let params: HttpParams = new HttpParams();
    if (inNameString) {
      params = params.set("inName", inNameString);
    }
    if (selectedReleaseYear) {
      params = params.set("releaseYear", "" + selectedReleaseYear);
    }
    if (selectedAuthorId) {
      params = params.set("authorId", "" + selectedAuthorId);
    }
    if (selectedPublisherId) {
      params = params.set("publisherId", "" + selectedPublisherId);
    }
    if (selectedSeriesId) {
      params = params.set("seriesId", "" + selectedSeriesId);
    }
    if (selectedCategoryId) {
      params = params.set("categoryId", "" + selectedCategoryId);
    }
    if (selectedGenreId) {
      params = params.set("genreId", "" + selectedGenreId);
    }
    if (selectedHasFree) {
      params = params.set("hasFree", "" + selectedHasFree);
    }
    if (selectedSortProperty) {
      params = params.set("sort", "" + selectedSortProperty);
    }
    if (selectedSortOrder) {
      params = params.set("order", "" + selectedSortOrder);
    }

    return this.http.get<ListBook[]>(this.baseUrl + this.contrUrl + "filter",
      {
        params: params
      });
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(this.baseUrl + this.contrUrl, { params: new HttpParams().set("id", "" + id) });
  }

  getFilterParams(): Observable<FilterParams> {
    return this.http.get<FilterParams>(this.baseUrl + this.contrUrl + "filterparams");
  }

  addBook(count: number, data: ViewBook): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);

    return this.http.post(this.baseUrl + this.contrUrl, data, {
      params: new HttpParams().set("count", "" + count),
      headers: headers
    });
  }

  editBook(id: number, data: ViewBook): any {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers = headers.append('Authorization', `Bearer ${authToken}`);

    return this.http.put(this.baseUrl + this.contrUrl, data, {
      params: new HttpParams().set("id", "" + id),
      headers: headers
    });
  }
}
