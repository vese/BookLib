import { Component, OnInit } from '@angular/core';
import { LibUser, QueueOnBook, LibBook } from '../LibClasses';
import { LibService } from '../lib.service';

@Component({
  selector: 'app-lib',
  templateUrl: './lib.component.html',
  styleUrls: ['./lib.component.css']
})
export class LibComponent implements OnInit {

  users: LibUser[];
  selectedUser: LibUser;
  userQueues: QueueOnBook[];

  books: LibBook[];
  selectedBook: LibBook;

  giveDisabled: boolean;
  returnDisabled: boolean;
  putInDisabled: boolean;
  removeFromDisabled: boolean;

  constructor(private libService: LibService) { }

  ngOnInit() {
    this.refresh();
  }

  getUserQueues(): void {
    this.libService.getUserQueues(this.selectedUser.name).subscribe(res => {
      this.userQueues = res;
      if (this.books) {
        this.checkDisabled();
      }
    });
  }

  refresh(): void {
    this.libService.getUsers().subscribe(res => {
      this.users = res;
      if (this.selectedUser) {
        this.selectedUser = this.users.find(u => u.name === this.selectedUser.name);
      }
      else {
        this.selectedUser = this.users[0];
      }

      this.libService.getUserQueues(this.selectedUser.name).subscribe(res => {
        this.userQueues = res;
        this.libService.getBooks().subscribe(res => {
          this.books = res;
          if (this.selectedBook) {
            this.selectedBook = this.books.find(b => b.id === this.selectedBook.id);
          }
          else {
            this.selectedBook = this.books[0];
          }
          this.checkDisabled();
        });
      });
    });
  }

  giveBook(id: number): void {
    this.libService.giveBook(this.selectedUser.name, id).subscribe(r => this.refresh());
  }

  returnBook(): void {
    this.libService.returnBook(this.selectedUser.name, this.selectedBook.id).subscribe(r => this.refresh());
  }

  putInQueue(): void {
    this.libService.putInQueue(this.selectedUser.name, this.selectedBook.id).subscribe(r => this.refresh());
  }

  removeFromQueue(): void {
    this.libService.removeFromQueue(this.selectedUser.name, this.selectedBook.id).subscribe(r => this.refresh());
  }

  checkDisabled(): void {
    this.libService.userHasBook(this.selectedUser.name, this.selectedBook.id).subscribe(res => {
      let queue = this.userQueues.find(q => q.id === this.selectedBook.id);
      this.giveDisabled = res || this.selectedUser.notReturned > 0 || (queue && queue.position > this.selectedBook.free) || this.books.find(b => b.id == this.selectedBook.id).free === 0;
      this.returnDisabled = !res;
      this.libService.userInQueue(this.selectedUser.name, this.selectedBook.id).subscribe(q => {
        this.putInDisabled = res || q.inQueue || this.selectedUser.notReturned > 0;
        this.removeFromDisabled = !q.inQueue;
      });
    });
  }
}
