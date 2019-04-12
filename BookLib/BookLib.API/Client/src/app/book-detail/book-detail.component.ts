import { Component, OnInit, Input } from '@angular/core';
import { Book, BookComment } from '../BookClasses';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { BookService } from '../book.service';
import { PageEvent, MatDialog } from '@angular/material';
import { CommentService } from '../comment.service';
import { DeleteBookDialogComponent } from '../delete-book-dialog/delete-book-dialog.component';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {
  @Input() book: Book;
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
    public dialog: MatDialog) { }

  ngOnInit() {
    this.getBook();
    this.getComments(null);
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
