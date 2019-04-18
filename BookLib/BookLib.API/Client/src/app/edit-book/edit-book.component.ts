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

  authorId: number;
  publisherId: number;
  hasSeries: boolean = false;
  seriesId: number;
  categoryId: number;
  categoryGenres: Param[];
  genreId: number;
  failureMessages: string[];
  failtureMessages: string[];
  disableGenre: boolean;

  defaultErrorMsg: string = 'Поле обязательно для заполнения!';

  nameFC = new FormControl("", [Validators.required]);
  authorFC = new FormControl("", [Validators.required]);
  publisherFC = new FormControl("", [Validators.required]);
  descrFC = new FormControl("", [Validators.required]);
  seriesFC = new FormControl("", [Validators.required]);
  categoryFC = new FormControl("", [Validators.required]);
  genreFC = new FormControl({ value: "", disabled: this.disableGenre }, [Validators.required]);
  yearFC = new FormControl("", [Validators.required]);

  forms: FormControl[] = [
    this.nameFC,
    this.authorFC,
    this.publisherFC,
    this.descrFC,
    this.seriesFC,
    this.categoryFC,
    this.genreFC,
    this.yearFC
  ];

  getErrorMessage(formControl: FormControl) {
    return formControl.hasError('required') ? this.defaultErrorMsg : '';
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
      this.nameFC.setValue(book.name);
      this.descrFC.setValue(book.description);
      this.yearFC.setValue(book.releaseYear);
      this.authorId = this.filterParams.authors.find(a => a.name === book.author).id;
      this.publisherId = this.filterParams.publishers.find(a => a.name === book.publisher).id;
      this.hasSeries = !!book.series;
      if (this.hasSeries) {
        this.seriesId = this.filterParams.series.find(a => a.name === book.series).id;
      }
      this.categoryId = this.filterParams.categories.find(a => a.category.name === book.category).category.id;
      if (this.categoryId) {
        this.getCategoryGenres();
        this.genreId = this.filterParams.categories.find(a => a.category.id === this.categoryId).genres.find(a => a.name === book.genre).id;
      }
    });
  }

  editBook(): void {
    this.failtureMessages = [];
    if (this.nameFC.valid && this.descrFC.valid && this.yearFC.valid &&
      this.authorFC.valid && this.publisherFC.valid &&
      this.seriesFC.valid && this.categoryFC.valid && this.genreFC.valid) { } if (true) {
        let data: ViewBook = {
          name: this.nameFC.value,
          description: this.descrFC.value,
          releaseYear: this.yearFC.value,
          authorId: this.authorId,
          author: this.authorFC.value,
          publisherId: this.publisherId,
          publisher: this.publisherFC.value,
          hasSeries: this.hasSeries,
          seriesId: this.seriesId,
          series: this.seriesFC.value,
          categoryId: this.categoryId,
          category: this.categoryFC.value,
          genreId: this.genreId,
          genre: this.genreFC.value
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
    if (this.categoryId) {
      this.categoryGenres = this.filterParams.categories.find(category => category.category.id === this.categoryId).genres;
    }
    else {
      this.genreId = null;
      this.categoryGenres = [];
    }
  }

  setDisableGenre(): void {
    this.disableGenre = !this.categoryId && (!this.categoryFC.value || this.categoryFC.invalid);
    if (this.disableGenre) {
      this.genreFC.disable();
    }
    else {
      this.genreFC.enable();
    }
  }
}
