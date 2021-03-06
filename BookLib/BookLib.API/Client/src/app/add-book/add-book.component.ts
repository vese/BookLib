import { Component, OnInit, OnDestroy } from '@angular/core';
import { FilterParams, Param, ViewBook } from '../bookclasses';
import { BookService } from '../book.service';
import { FormControl, Validators, } from '@angular/forms';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})

export class AddBookComponent implements OnInit, OnDestroy {

  subscription: Subscription;

  filterParams: FilterParams;

  authorId: number;
  publisherId: number;
  hasSeries: boolean = false;
  seriesId: number;
  categoryId: number;
  categoryGenres: Param[];
  genreId: number;
  failureMessages: string[];
  disableGenre: boolean = true;
  
  defaultErrorMsg: string = 'Поле обязательно для заполнения!';

  nameFC = new FormControl("", [Validators.required]);
  authorFC = new FormControl("", [Validators.required]);
  publisherFC = new FormControl("", [Validators.required]);
  descrFC = new FormControl("", [Validators.required]);
  seriesFC = new FormControl("", [Validators.required]);
  categoryFC = new FormControl("", [Validators.required]);
  genreFC = new FormControl({ value: "", disabled: this.disableGenre }, [Validators.required]);
  yearFC = new FormControl("", [Validators.required]);
  countFC = new FormControl("", [Validators.required, Validators.min(1)]);

  forms: FormControl[] = [
    this.nameFC,
    this.authorFC,
    this.publisherFC,
    this.descrFC,
    this.seriesFC,
    this.categoryFC,
    this.genreFC,
    this.yearFC,
    this.countFC
  ];


  getErrorMessage(formControl: FormControl) {
    return formControl.hasError('required') ? this.defaultErrorMsg : '';
  }

  getCountErrorMessage() {
    return this.countFC.hasError('required') ? this.defaultErrorMsg :
      this.countFC.hasError('min') ? 'Значение должно быть больше 0' :
      '';
  }

  constructor(
    private bookService: BookService,
    private userService: UserService) {
    this.subscription = this.userService.logChanged$.subscribe(
      res => {
        if (!res || !this.userService.isAdmin()) {
          this.userService.redirectToHome();
        }
      });
  }

  ngOnInit() {
    if (!this.userService.isAuthenticated() || !this.userService.isAdmin()) {
      this.userService.redirectToHome();
    }
    this.getFilterParams();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  formsIsValid(): boolean {
    for (let i = 0; i < this.forms.length; i++) {
      if (!this.forms[i].valid) return false;
    }
  }

  addBook(): void {
    this.failureMessages = [];
    if (this.formsIsValid) {
      
      let data: ViewBook = {
        name: this.nameFC.value,
        authorId: this.authorId,
        author: this.authorFC.value,
        publisherId: this.publisherId,
        publisher: this.publisherFC.value,
        description: this.descrFC.value,
        releaseYear: this.yearFC.value,
        hasSeries: this.hasSeries,
        seriesId: this.seriesId,
        series: this.seriesFC.value,
        categoryId: this.categoryId,
        category: this.categoryFC.value,
        genreId: this.genreId,
        genre: this.genreFC.value
      }

      this.bookService.addBook(this.countFC.value, data).subscribe(res => {
        this.failureMessages.push("Книга успешно добавлена!")
      }, error => {
        if (error.error) {
          if (error.error.Book) {
            error.error.Book.forEach(el => this.failureMessages.push(el));
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
