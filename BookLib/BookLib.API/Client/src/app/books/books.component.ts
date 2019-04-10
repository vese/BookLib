import { Component, OnInit } from '@angular/core';
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

  constructor(private bookService: BookService) {
  }

  ngOnInit() {
    this.getBooks();
    this.getFilterParams();
  }

  getBooks(): void {
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
      this.selectedSortOrder).subscribe(books => this.books = books);
  }

  getFilterParams(): void {
    this.bookService.getFilterParams().subscribe(params => this.filterParams = params);
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
    this.selectedSortOrderValue ? this.selectedSortOrder = "desc" : this.selectedSortOrder = "asc";
  }
}
