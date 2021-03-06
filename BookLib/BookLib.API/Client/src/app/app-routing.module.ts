import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books/books.component';
import { HomeComponent } from './home/home.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { AddBookComponent } from './add-book/add-book.component';
import { EditBookComponent } from './edit-book/edit-book.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LibComponent } from './lib/lib.component';
import { ListComponent } from './list/list.component';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { RegisterDialogComponent } from './register-dialog/register-dialog.component';

const routes: Routes =
  [{
    path: "home", component: HomeComponent
  },
  {
    path: "books", component: BooksComponent
  },
  {
    path: "book/add", component: AddBookComponent
  },
  {
    path: "book/edit/:id", component: EditBookComponent
  },
  {
    path: "book/:id", component: BookDetailComponent
  },
  {
    path: "lib", component: LibComponent
  },
  {
    path: "list", component: ListComponent
  },
  {
    path: '', redirectTo: '/home', pathMatch: 'full'
  },
  {
    path: '**', component: PageNotFoundComponent
  }]

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})

export class AppRoutingModule {
}
