import { Component, OnInit } from '@angular/core';
import { ListService } from '../list.service';
import { Param } from '../BookClasses';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  username: string;
  sheduledList: Param[];
  readList: Param[];

  constructor(private listService: ListService) { }

  ngOnInit() {
    this.username = localStorage.getItem("name");
    this.listService.getSheduledList(this.username).subscribe(res => this.sheduledList = res);
    this.listService.getReadList(this.username).subscribe(res => this.readList = res);
  }

}
