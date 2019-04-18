import { Component, OnInit } from '@angular/core';
import { ListService } from '../list.service';
import { Param } from '../BookClasses';
import { QueueOnBook, BookOnHands } from '../LibClasses';
import { LibService } from '../lib.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  username: string;
  favouriteList: Param[];
  readList: Param[];
  queuesList: QueueOnBook[];
  booksOnHands: BookOnHands[];

  constructor(
    private listService: ListService,
    private libService: LibService) { }

  ngOnInit() {
    this.username = localStorage.getItem("name");
    this.listService.getScheduledList(this.username).subscribe(res => this.favouriteList = res);
    this.listService.getReadList(this.username).subscribe(res => this.readList = res);
    this.libService.getUserQueues(this.username).subscribe(res => this.queuesList = res);
    this.libService.getBookOnHands(this.username).subscribe(res => this.booksOnHands = res);
  }

}
