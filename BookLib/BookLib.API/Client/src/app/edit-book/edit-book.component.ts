import { Component, OnInit } from '@angular/core';
import { FilterParams, Param, ViewBook, Book } from '../BookClasses';
import { BookService } from '../book.service';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent implements OnInit {
  id: number;

  filterParams: FilterParams;

  selectedAuthorId: number;
  selectedPublisherId: number;
  hasSeries: boolean = false;
  selectedSeriesId: number;
  selectedCategoryId: number;
  selectedCategoryGenres: Param[];
  selectedGenreId: number;
  failtureMessages: string[];
  disableGenre: boolean;

  nameFormControl = new FormControl("", [
    Validators.required
  ]);
  descriptionFormControl = new FormControl("", [
    Validators.required
  ]);
  releaseYearFormControl = new FormControl("", [
    Validators.required
  ]);
  authorFormControl = new FormControl("", [
    Validators.required
  ]);
  publisherFormControl = new FormControl("", [
    Validators.required
  ]);
  seriesFormControl = new FormControl("", [
    Validators.required
  ]);
  categoryFormControl = new FormControl("", [
    Validators.required
  ]);
  genreFormControl = new FormControl("", [
    Validators.required
  ]);

  getNameErrorMessage() {
    return this.nameFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getDescriptionErrorMessage() {
    return this.descriptionFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getReleaseYearErrorMessage() {
    return this.releaseYearFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getAuthorErrorMessage() {
    return this.authorFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getPublisherErrorMessage() {
    return this.publisherFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getSeriesErrorMessage() {
    return this.seriesFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getCategoryErrorMessage() {
    return this.categoryFormControl.hasError('required') ? 'Введите значение' : '';
  }
  getGenreErrorMessage() {
    return this.genreFormControl.hasError('required') ? 'Введите значение' : '';
  }
  
  constructor(
    private route: ActivatedRoute,
    private bookService: BookService) { }

  ngOnInit() {
    this.getFilterParams();
    this.getBook();
  }

  getBook(): void {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.bookService.getBook(this.id).subscribe(book => {
      this.nameFormControl.setValue(book.name);
      this.descriptionFormControl.setValue(book.description);
      this.releaseYearFormControl.setValue(book.releaseYear);
      this.selectedAuthorId = this.filterParams.authors.find(a => a.name === book.author).id;
      this.selectedPublisherId = this.filterParams.publishers.find(a => a.name === book.publisher).id;
      this.hasSeries = !!book.series;
      if (this.hasSeries) {
        this.selectedSeriesId = this.filterParams.series.find(a => a.name === book.series).id;
      }
      this.selectedCategoryId = this.filterParams.categories.find(a => a.category.name === book.category).category.id;
      if (this.selectedCategoryId) {
        this.getCategoryGenres();
        this.selectedGenreId = this.filterParams.categories.find(a => a.category.id === this.selectedCategoryId).genres.find(a => a.name === book.genre).id;
      }
    });
  }

  editBook(): void {
    this.failtureMessages = [];
    if (this.nameFormControl.valid && this.descriptionFormControl.valid && this.releaseYearFormControl.valid &&
      this.authorFormControl.valid && this.publisherFormControl.valid &&
      this.seriesFormControl.valid && this.categoryFormControl.valid && this.genreFormControl.valid) { } if (true) {
        let data: ViewBook = {
          name: this.nameFormControl.value,
          description: this.descriptionFormControl.value,
          releaseYear: this.releaseYearFormControl.value,
          authorId: this.selectedAuthorId,
          author: this.authorFormControl.value,
          publisherId: this.selectedPublisherId,
          publisher: this.publisherFormControl.value,
          hasSeries: this.hasSeries,
          seriesId: this.selectedSeriesId,
          series: this.seriesFormControl.value,
          categoryId: this.selectedCategoryId,
          category: this.categoryFormControl.value,
          genreId: this.selectedGenreId,
          genre: this.genreFormControl.value
        }
        this.bookService.editBook(this.id, data).subscribe(res => {
          this.failtureMessages.push("Книга успешно изменена!")
        }, error => {
          if (error.error) {
            if (error.error.Book) {
              error.error.Book.forEach(el => this.failtureMessages.push(el));
            }
          }
        });
      }
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

  setDisableGenre(): void {
    this.disableGenre = !this.selectedCategoryId && (!this.categoryFormControl.value || this.categoryFormControl.invalid);
    if (this.disableGenre) {
      this.genreFormControl.disable();
    }
    else {
      this.genreFormControl.enable();
    }
  }
}
