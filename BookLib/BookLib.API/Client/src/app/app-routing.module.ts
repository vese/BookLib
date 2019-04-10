import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books/books.component';
import { HomeComponent } from './home/home.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { AddBookComponent } from './add-book/add-book.component';

const routes: Routes =
  [{
    path: "", component: HomeComponent
  },
  {
    path: "filter", component: BooksComponent
  },
  {
    path: "book/add", component: AddBookComponent
  },
  {
    path: "book/:id", component: BookDetailComponent
  }]

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})

export class AppRoutingModule {
}
