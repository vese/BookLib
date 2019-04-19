import { Component, OnInit, OnDestroy } from '@angular/core';
import { ListService } from '../list.service';
import { Param } from '../bookclasses';
import { QueueOnBook, BookOnHands } from '../libclasses';
import { LibService } from '../lib.service';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy {

  subscription: Subscription;

  username: string;
  favouriteList: Param[];
  readList: Param[];
  queuesList: QueueOnBook[];
  booksOnHands: BookOnHands[];

  constructor(
    private listService: ListService,
    private libService: LibService,
    private userService: UserService) {
    this.subscription = this.userService.logChanged$.subscribe(
      res => {
        if (!res || this.userService.isAdmin()) {
          this.userService.redirectToHome();
        }
      });
  }

  ngOnInit() {
    if (!this.userService.isAuthenticated() || this.userService.isAdmin()) {
      this.userService.redirectToHome();
    }
    this.username = localStorage.getItem("name");
    this.listService.getScheduledList(this.username).subscribe(res => this.favouriteList = res);
    this.listService.getReadList(this.username).subscribe(res => this.readList = res);
    this.libService.getUserQueues(this.username).subscribe(res => this.queuesList = res);
    this.libService.getBookOnHands(this.username).subscribe(res => this.booksOnHands = res);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
