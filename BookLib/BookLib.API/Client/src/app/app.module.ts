import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { BooksComponent } from './books/books.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { NotificationComponent } from './notification/notification.component';
import { HomeComponent } from './home/home.component';
import { AddBookComponent } from './add-book/add-book.component';
import { EditBookComponent } from './edit-book/edit-book.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './token.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatSelectModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule, MatDialogModule, MatListModule,
  MatPaginatorModule, MatExpansionModule, MatMenuModule, MatPaginatorIntl
} from '@angular/material';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { RegisterDialogComponent } from './register-dialog/register-dialog.component';
import { UserComponent } from './user/user.component';
import { DeleteBookDialogComponent } from './delete-book-dialog/delete-book-dialog.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { UserService } from './user.service';
import { LibComponent } from './lib/lib.component';
import { ListComponent } from './list/list.component';

export class CustomPaginator extends MatPaginatorIntl {
  constructor() {
    super();
    this.nextPageLabel = 'Следующая';
    this.previousPageLabel = 'Предыдущая';
    this.itemsPerPageLabel = 'Книг на странице';
  }
}
@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    BookDetailComponent,
    NotificationComponent,
    HomeComponent,
    AddBookComponent,
    EditBookComponent,
    LoginDialogComponent,
    RegisterDialogComponent,
    UserComponent,
    LoginDialogComponent,
    RegisterDialogComponent,
    DeleteBookDialogComponent,
    PageNotFoundComponent,
    LibComponent,
    ListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDialogModule,
    MatListModule,
    MatPaginatorModule,
    MatExpansionModule,
    MatMenuModule
  ],
  exports: [
    LoginDialogComponent,
    RegisterDialogComponent
  ],
  entryComponents: [
    LoginDialogComponent,
    RegisterDialogComponent,
    DeleteBookDialogComponent
  ],
  providers: [UserService,
    {
      provide: MatPaginatorIntl,
      useClass: CustomPaginator
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
