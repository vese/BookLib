import { Component, OnInit, Input } from '@angular/core';
import { Book, BookComment } from '../BookClasses';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
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

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private commentService: CommentService,
    private location: Location,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.getBook();
    this.getComments(null);
  }

  getBook(): void {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.bookService.getBook(this.id).subscribe(book => {
      this.book = book;
      this.length = book.commentsCount;
    });
  }

  getComments($event): void {
    this.pageEvent = $event;
    this.comments = [];
    this.commentService.getComments(this.id, this.pageEvent ? this.pageEvent.pageIndex * this.pageEvent.pageSize : 0
      , this.pageEvent ? this.pageEvent.pageSize : this.pageSize, "asc").subscribe(res => this.comments = res);
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
      console.log(12345);
      if (!result) {
        this.goBack();
      }
    });
  }
}
