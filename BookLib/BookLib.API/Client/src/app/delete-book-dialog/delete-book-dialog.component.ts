import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService } from '../user.service';
import { BookService } from '../book.service';

export interface DialogData {
  id: number,
  name: string
}

@Component({
  selector: 'app-delete-book-dialog',
  templateUrl: './delete-book-dialog.component.html',
  styleUrls: ['./delete-book-dialog.component.css']
})
export class DeleteBookDialogComponent implements OnInit {

  loggedIn: boolean;
  deleted: boolean;

  constructor(public dialogRef: MatDialogRef<DeleteBookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private userService: UserService,
    private bookService: BookService) { }

  ngOnInit() {
    this.loggedIn = this.userService.isLoggedIn();
    if (this.loggedIn == null) {
      this.userService.checkLogged().subscribe(res => this.loggedIn = true, error => this.loggedIn = false);
    }
  }

  onNoClick(): void {
    this.dialogRef.close({ canceled: true });
  }

  onDeleteClick(): void {
    this.bookService.deleteBook(this.data.id).subscribe(res => this.deleted = true);
  }
}
