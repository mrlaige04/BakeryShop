import {Component, inject, OnDestroy} from '@angular/core';
import {Router} from "@angular/router";
import {DialogService} from "primeng/dynamicdialog";
import {ConfirmationService, MessageService} from "primeng/api";
import {Subscription} from "rxjs";
import { ConfirmDialogModule} from "primeng/confirmdialog";
import {NotificationService} from "../../utils/services/notification.service";
import {ToastModule} from "primeng/toast";

@Component({
  selector: 'bs-base',
  standalone: true,
  imports: [ConfirmDialogModule, ToastModule],
  providers: [ConfirmationService, DialogService, NotificationService],
  template: ''
})
export class BaseComponent implements OnDestroy {
  protected router = inject(Router)
  protected dialog = inject(DialogService);
  protected confirmation = inject(ConfirmationService);
  protected notification = inject(NotificationService);

  protected subscriptions: Subscription[] = [];

  addSubscription(subscription: Subscription) {
    this.subscriptions.push(subscription);
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }
}
