import { Component, OnInit, OnDestroy } from '@angular/core';
import { LibService } from '../lib.service';
import { Notifications } from '../libclasses';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';
import { Roles } from '../roles';

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
    if (localStorage.getItem("role") != Roles.Admin) {
      this.libService.getNotifications(localStorage.getItem("name")).subscribe(not => {
        this.notifications = not;
        this.count = not.queue.length + not.onHands.filter(o => o.notificationLevel > 1).length;
        this.allCount = not.queue.length + not.onHands.length;
      });
    }
  }

  ngOnInit() {
    if (this.userService.isAuthenticated()) {
      this.refresh();
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
