import { Component, OnInit, OnDestroy } from '@angular/core';
import { Book, BookComment } from '../BookClasses';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { BookService } from '../book.service';
import { PageEvent, MatDialog } from '@angular/material';
import { CommentService } from '../comment.service';
import { DeleteBookDialogComponent } from '../delete-book-dialog/delete-book-dialog.component';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';
import { FormControl, Validators} from '@angular/forms';
import { ListService } from '../list.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})

export class BookDetailComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  loggedIn: boolean;
  isAdmin: boolean;
  name: string;

  book: Book;
  id: number;

  pageEvent: PageEvent;
  length: number;
  pageSize: number = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  comments: BookComment[];

  filterParams: Params;

  text: string;
  mark: number = 0;
  needMark: boolean = false;
  commentExists: boolean;

  defaultErrorMsg: string = '���� ����������� ��� ����������!';
  commentFC = new FormControl("", [Validators.required]);
  inSheduled: boolean;
  inRead: boolean;
  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private commentService: CommentService,
    private userService: UserService,
    private listService: ListService,
    public dialog: MatDialog,
    private location: Location) {
    this.subscription = this.userService.logChanged$.subscribe(
      res => {
        this.loggedIn = res;
        if (res) {
          this.isAdmin = localStorage.getItem("role") === "admin";
          this.name = localStorage.getItem("name");
          this.commentService.commentExists(this.name, this.id).subscribe(ex => this.commentExists = ex.exists);
          if (!this.isAdmin) {
            this.listService.inSheduled(this.name, this.id).subscribe(res => this.inSheduled = res);
            this.listService.inRead(this.name, this.id).subscribe(res => this.inRead = res);
          }
        }
      });
  }

  ngOnInit() {
    this.userService.checkLogged();
    this.getBook();
    this.getComments(null);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  getErrorMessage(formControl: FormControl) {
    return formControl.hasError('required') ? this.defaultErrorMsg : '';
  }

  getBook(): void {
    this.route.queryParams.subscribe(res => {
      if (res) {
        this.filterParams = res;
      }
    });
    this.route.paramMap.subscribe(res => {
      this.id = +res.get("id");
      this.bookService.getBook(this.id).subscribe(book => {
        this.book = book;
        this.length = book.commentsCount;
      });
    });
  }

  getComments($event): void {
    this.pageEvent = $event;
    this.comments = [];
    this.commentService.getComments(this.id, this.pageEvent ? this.pageEvent.pageIndex * this.pageEvent.pageSize : 0
      , this.pageEvent ? this.pageEvent.pageSize : this.pageSize, "asc").subscribe(res => this.comments = res);
  }

  addComment(): void {

    if (this.loggedIn && this.commentFC.valid && this.mark > 0) {
      this.commentService.addComment(this.text, this.mark, this.name, this.id).subscribe(res =>
      {
          this.getComments(this.pageEvent); this.commentExists = true;
       });
    } else {
      this.needMark = true;
    }
  }

  setMark(mark: number): void {
    this.mark = mark;
    this.needMark = false;
  }

  goBack(): void {
    this.location.back();
  }

  openDeleteBookDialog() {
    const dialogRef = this.dialog.open(DeleteBookDialogComponent, {
      width: '400px',
      data: {
        id: this.id,
        name: this.book.name
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        this.goBack();
      }
    });
  }

  addToSheduled(): void {
    this.listService.addToSheduled(this.name, this.id).subscribe(res => this.inSheduled = true);
  }

  addToRead(): void {
    this.listService.addToRead(this.name, this.id).subscribe(res => this.inRead = true);
  }

  removeFromSheduled(): void {
    this.listService.removeFromSheduled(this.name, this.id).subscribe(res => this.inSheduled = false);
  }

  removeFromRead(): void {
    this.listService.removeFromRead(this.name, this.id).subscribe(res => this.inRead = false);
  }
}
