import { Component, OnInit, OnDestroy } from '@angular/core';
import { LibService } from '../lib.service';
import { Notifications } from '../LibClasses';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit, OnDestroy {

  subscription: Subscription;

  notifications: Notifications;
  count: number;
  allCount: number;

  constructor(
    private libService: LibService,
    private userService: UserService) {
    this.subscription = this.userService.logChanged$.subscribe(
      res => {
        if (res) {
          this.refresh();
        }
      });
  }

  refresh(): void {
    if (localStorage.getItem("role") != "admin") {
      this.libService.getNotifications(localStorage.getItem("name")).subscribe(not => {
        this.notifications = not;
        this.count = not.queue.length + not.onHands.filter(o => o.notificationLevel > 1).length;
        this.allCount = not.queue.length + not.onHands.length;
      });
    }
  }

  ngOnInit() {
    this.userService.checkLogged();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
