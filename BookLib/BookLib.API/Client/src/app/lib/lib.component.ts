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
  selectedBookId: number;

  giveDisabled: boolean;
  returnDisabled: boolean;
  putInDisabled: boolean;

  constructor(private libService: LibService) { }

  ngOnInit() {
    this.refresh();
  }

  getUserQueues(): void {
    this.libService.getUserQueues().subscribe(res => this.userQueues = res);
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
      this.getUserQueues();
      this.libService.getBooks().subscribe(res => {
        this.books = res;
        this.selectedBookId = this.books[0].id;
        this.checkDisabled();
      });
    });

  }

  giveBook(id: number): void {
    this.libService.giveBook(this.selectedUser.name, id).subscribe(r => this.refresh());
  }

  returnBook(): void {
    this.libService.returnBook(this.selectedUser.name, this.selectedBookId).subscribe(r => this.refresh());
  }

  putInQueueBook(): void {
    this.libService.putInQueueBook(this.selectedUser.name, this.selectedBookId).subscribe(r => this.refresh());
  }

  checkDisabled(): void {
    this.libService.userHasBook(this.selectedUser.name, this.selectedBookId).subscribe(res => {
      let queue = this.userQueues.find(q => q.id === this.selectedBookId);
      this.giveDisabled = res || this.selectedUser.expired > 0 || (queue && queue.position > 1) || this.books.find(b => b.id == this.selectedBookId).free === 0;
      this.returnDisabled = !res;
      this.putInDisabled = res || this.selectedUser.expired > 0;
    });
  }
}
