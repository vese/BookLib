import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ListBook, FilterParams, Param } from '../BookClasses';
import { BookService } from '../book.service';
import { PageEvent } from '@angular/material';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  filterParams: FilterParams;
  books: ListBook[];

  pageEvent: PageEvent;
  length: number;
  pageSize: number = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  inNameString: string;
  selectedReleaseYear: number;
  selectedAuthorId: number;
  selectedPublisherId: number;
  selectedSeriesId: number;
  selectedCategoryId: number;
  selectedCategoryGenres: Param[];
  selectedGenreId: number;
  selectedHasFree: boolean;
  selectedSortProperty: string;
  selectedSortOrderValue: boolean = true;
  selectedSortOrder: string;

  constructor(private bookService: BookService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.getFilterParams();
  }

  getOptionalParams(): void {
    this.route.queryParamMap.subscribe(res => {
      if (res.get("name")) {
        this.inNameString = res.get("name");
      }
      if (res.get("year")) {
        this.selectedReleaseYear = +res.get("year");
      }
      if (res.get("author")) {
        this.selectedAuthorId = this.filterParams.authors.find(a => a.name === res.get("author")).id;
      }
      if (res.get("publisher")) {
        this.selectedPublisherId = this.filterParams.publishers.find(a => a.name === res.get("publisher")).id;
      }
      if (res.get("series")) {
        this.selectedSeriesId = this.filterParams.series.find(a => a.name === res.get("series")).id;
      }
      if (res.get("category")) {
        this.selectedCategoryId = this.filterParams.categories.find(a => a.category.name === res.get("category")).category.id;
        this.getCategoryGenres();
        if (res.get("genre")) {
          this.selectedGenreId = this.selectedCategoryGenres.find(a => a.name === res.get("genre")).id;
        }
      }
      if (res.get("hasFree")) {
        this.selectedHasFree = res.get("hasFree") === "true";
      }
      if (res.get("sortProperty")) {
        this.selectedSortProperty = res.get("sortProperty");
        if (res.get("orderValue") != null) {
          this.selectedSortOrderValue = res.get("orderValue") === "true";
          this.getSortOrder();
        }
      }
      this.getBooks();
    });
  }

  getBooks($event = null): void {
    this.pageEvent = $event;
    this.bookService.getBooks(
      this.inNameString,
      this.selectedReleaseYear,
      this.selectedAuthorId,
      this.selectedPublisherId,
      this.selectedSeriesId,
      this.selectedCategoryId,
      this.selectedGenreId,
      this.selectedHasFree,
      this.selectedSortProperty,
      this.selectedSortOrder,
      this.pageEvent ? this.pageEvent.pageIndex * this.pageEvent.pageSize : 0,
      this.pageEvent ? this.pageEvent.pageSize : this.pageSize).subscribe(books => {
        this.books = books.books;
        this.length = books.count;
      });


    let params: Params = {};
    if (this.inNameString) {
      params.name = this.inNameString;
    }
    if (this.selectedReleaseYear) {
      params.year = this.selectedReleaseYear;
    }
    if (this.selectedAuthorId) {
      params.author = this.filterParams.authors.find(a => a.id === this.selectedAuthorId).name;
    }
    if (this.selectedPublisherId) {
      params.publisher = this.filterParams.publishers.find(a => a.id === this.selectedPublisherId).name;
    }
    if (this.selectedSeriesId) {
      params.series = this.filterParams.series.find(a => a.id === this.selectedSeriesId).name;
    }
    if (this.selectedCategoryId) {
      params.category = this.filterParams.categories.find(a => a.category.id === this.selectedCategoryId).category.name;
      if (this.selectedGenreId) {
        params.genre = this.selectedCategoryGenres.find(a => a.id === this.selectedGenreId).name;
      }
    }
    if (this.selectedHasFree) {
      params.hasFree = this.selectedHasFree;
    }
    if (this.selectedSortProperty) {
      params.sortProperty = this.selectedSortProperty;
      if (this.selectedSortOrderValue != null) {
        params.orderValue = this.selectedSortOrderValue;
      }
    }
    this.router.navigate([], { queryParams: params });
  }

  getFilterParams(): void {
    this.bookService.getFilterParams().subscribe(params => {
      this.filterParams = params;
      this.getOptionalParams();
    });
  }

  getCategoryGenres(): void {
    if (this.selectedCategoryId) {
      this.selectedCategoryGenres = this.filterParams.categories.find(category => category.category.id === this.selectedCategoryId).genres;
    }
    else {
      this.selectedGenreId = null;
      this.selectedCategoryGenres = [];
    }
  }

  getSortOrder(): void {
    this.selectedSortOrder = this.selectedSortOrderValue ? "asc" : "desc";
  }

  //gotoBookDetail(id: number): void {
  //  let params: Params = {};
  //  if (this.inNameString) {
  //    params.name = this.inNameString;
  //  }
  //  if (this.selectedReleaseYear) {
  //    params.year = this.selectedReleaseYear;
  //  }
  //  if (this.selectedAuthorId) {
  //    params.author = this.filterParams.authors.find(a => a.id === this.selectedAuthorId).name;
  //  }
  //  if (this.selectedPublisherId) {
  //    params.publisher = this.filterParams.publishers.find(a => a.id === this.selectedPublisherId).name;
  //  }
  //  if (this.selectedSeriesId) {
  //    params.series = this.filterParams.series.find(a => a.id === this.selectedSeriesId).name;
  //  }
  //  if (this.selectedCategoryId) {
  //    params.category = this.filterParams.categories.find(a => a.category.id === this.selectedCategoryId).category.name;
  //    if (this.selectedGenreId) {
  //      params.genre = this.selectedCategoryGenres.find(a => a.id === this.selectedGenreId).name;
  //    }
  //  }
  //  if (this.selectedHasFree) {
  //    params.hasFree = this.selectedHasFree;
  //  }
  //  if (this.selectedSortProperty) {
  //    params.sortProperty = this.selectedSortProperty;
  //    if (this.selectedSortOrderValue != null) {
  //      params.orderValue = this.selectedSortOrderValue;
  //    }
  //  }
  //  this.router.navigate(['/book', id], { queryParams: params });
  //}
}
