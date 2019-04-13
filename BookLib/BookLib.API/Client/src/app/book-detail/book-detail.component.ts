import { Component, OnInit, OnDestroy } from '@angular/core';
import { Book, BookComment } from '../BookClasses';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { BookService } from '../book.service';
import { PageEvent, MatDialog } from '@angular/material';
import { CommentService } from '../comment.service';
import { DeleteBookDialogComponent } from '../delete-book-dialog/delete-book-dialog.component';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  loggedIn: boolean;
  isAdmin: boolean;

  book: Book;
  id: number;

  pageEvent: PageEvent;
  length: number;
  pageSize: number = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  comments: BookComment[];

  filterParams: Params;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookService: BookService,
    private commentService: CommentService,
    private userService: UserService,
    public dialog: MatDialog) {
    this.subscription = this.userService.logChanged$.subscribe(
      res => {
        this.loggedIn = res;
        if (res) {
          this.isAdmin = localStorage.getItem("role") === "admin";
        }
      });
  }

  ngOnInit() {
    this.getBook();
    this.getComments(null);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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

  goBack(): void {
    this.router.navigate(['/filter'], { queryParams: this.filterParams });
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
      console.log(12345);
      if (!result) {
        this.goBack();
      }
    });
  }
}
