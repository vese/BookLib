import { Component, OnInit } from '@angular/core';
import { FilterParams, Param, ViewBook } from '../BookClasses';
import { BookService } from '../book.service';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})

export class AddBookComponent implements OnInit {
  filterParams: FilterParams;

  selectedAuthorId: number;
  selectedPublisherId: number;
  hasSeries: boolean = false;
  selectedSeriesId: number;
  selectedCategoryId: number;
  selectedCategoryGenres: Param[];
  selectedGenreId: number;
  failtureMessages: string[];
  disableGenre: boolean = true;

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
  genreFormControl = new FormControl({ value: "", disabled: this.disableGenre }, [
    Validators.required
  ]);
  countFormControl = new FormControl("", [
    Validators.required,
    Validators.min(1)
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
  getCountErrorMessage() {
    return this.countFormControl.hasError('required') ? 'Введите значение' :
      this.countFormControl.hasError('min') ? 'Значение должно быть больше 0' :
      '';
  }


  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.getFilterParams();
  }

  addBook(): void {
    this.failtureMessages = [];
    if (this.nameFormControl.valid && this.descriptionFormControl.valid && this.releaseYearFormControl.valid &&
      this.authorFormControl.valid && this.publisherFormControl.valid &&
      this.seriesFormControl.valid && this.categoryFormControl.valid && this.genreFormControl.valid && this.countFormControl.valid) { }if(true){
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
      this.bookService.addBook(this.countFormControl.value, data).subscribe(res => {
        this.failtureMessages.push("Книга успешно добавлена!")
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
