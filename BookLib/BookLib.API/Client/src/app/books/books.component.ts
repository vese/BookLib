import { Component, OnInit } from '@angular/core';
import { Book, ListBook } from '../BookClasses';
import { BookService } from '../book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit
{
  books: ListBook[];

  constructor(private bookService: BookService)
  {
  }

  ngOnInit()
  {
    this.getBooks();
  }

  getBooks(): void
  {
    this.bookService.getBooks().subscribe(books => this.books = books);
  }
}
