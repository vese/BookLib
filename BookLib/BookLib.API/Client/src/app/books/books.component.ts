import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ListBook, FilterParams, Param } from '../BookClasses';
import { BookService } from '../book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})

export class BooksComponent implements OnInit {

  filterParams: FilterParams;
  books: ListBook[];

  inNameString: string;
  releaseYear: number;
  authorId: number;
  publisherId: number;
  seriesId: number;
  categoryId: number;
  categoryGenres: Param[];
  genreId: number;
  hasFree: boolean;
  sortProperty: string;
  sortOrderValue: boolean = true;
  sortOrder: string;

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
      if (res.get("releaseYear")) {
        this.releaseYear = +res.get("releaseYear");
      }
      if (res.get("authorId")) {
        let aid: number = +res.get("authorId");
        this.authorId = this.filterParams.authors.find(a => a.id === +res.get("authorId")).id;
      }
      if (res.get("publisherId")) {
        this.publisherId = this.filterParams.publishers.find(a => a.id === +res.get("publisherId")).id;
      }
      if (res.get("seriesId")) {
        this.seriesId = this.filterParams.series.find(a => a.id === +res.get("seriesId")).id;
      }
      if (res.get("categoryId")) {
        this.categoryId = this.filterParams.categories.find(a => a.category.id === +res.get("categoryId")).category.id;
        this.getCategoryGenres();
        if (res.get("genreId")) {
          this.genreId = this.categoryGenres.find(a => a.id === +res.get("genreId")).id;
        }
      }
      if (res.get("hasFree")) {
        this.hasFree = res.get("hasFree") === "true";
      }
      if (res.get("sortProperty")) {
        this.sortProperty = res.get("sortProperty");
        if (res.get("orderValue") != null) {
          this.sortOrderValue = res.get("orderValue") === "true";
          this.getSortOrder();
        }
      }
      this.getBooks();
    });
  }

  getBooks(): void {
    this.bookService.getBooks(
      this.inNameString,
      this.releaseYear,
      this.authorId,
      this.publisherId,
      this.seriesId,
      this.categoryId,
      this.genreId,
      this.hasFree,
      this.sortProperty,
      this.sortOrder).subscribe(b => this.books = b);


    let params: Params = {};

    if (this.inNameString) {
      params.name = this.inNameString;
    }
    if (this.releaseYear) {
      params.releaseYear = this.releaseYear;
    }
    if (this.authorId) {
      params.authorId = this.filterParams.authors.find(a => a.id === this.authorId).id;
    }
    if (this.publisherId) {
      params.publisherId = this.filterParams.publishers.find(a => a.id === this.publisherId).id;
    }
    if (this.seriesId) {
      params.seriesId = this.filterParams.series.find(a => a.id === this.seriesId).id;
    }
    if (this.categoryId) {
      params.categoryId = this.filterParams.categories.find(a => a.category.id === this.categoryId).category.id;
      if (this.genreId) {
        params.genreId = this.categoryGenres.find(a => a.id === this.genreId).id;
      }
    }
    if (this.hasFree) {
      params.hasFree = this.hasFree;
    }
    if (this.sortProperty) {
      params.sortProperty = this.sortProperty;
      if (this.sortOrderValue != null) {
        params.orderValue = this.sortOrderValue;
      }
    }
    this.router.navigate([], { queryParams: params });
  }

  getCategoryGenres(): void {
    if (this.categoryId) {
      this.categoryGenres = this.filterParams.categories.find(c => c.category.id === this.categoryId).genres;
    }
    else {
      this.genreId = null;
      this.categoryGenres = [];
    }
  }

  getSortOrder(): void {
    this.sortOrder = this.sortOrderValue ? "asc" : "desc";
  }

  getFilterParams(): void {
    this.bookService.getFilterParams().subscribe(
    p => {
      this.filterParams = p;
      this.getOptionalParams();
    });
  }

}
