import {Component, inject, OnInit, signal} from '@angular/core';
import {OrderService} from "../order.service";
import {OrderModel} from "../models/order.model";
import {BaseComponent} from "../../layout/base/base.component";
import {DialogService} from "primeng/dynamicdialog";
import {ConfirmationService} from "primeng/api";
import {NotificationService} from "../../utils/services/notification.service";
import {PaginatedList} from "../../models/paginated-list";
import {SearchOrders} from "../requests/search-orders";
import {PaginatorModule, PaginatorState} from "primeng/paginator";
import {TableModule} from "primeng/table";
import {JsonPipe, NgIf} from "@angular/common";
import {InputTextModule} from "primeng/inputtext";
import {Button} from "primeng/button";
import {OrderStatus} from "../models/order-status.enum";
import {OrderItemModel} from "../models/order-item.model";
import {Ripple} from "primeng/ripple";
import {DividerModule} from "primeng/divider";
import {Guid} from "guid-typescript";
import {ConfirmDialogModule} from "primeng/confirmdialog";

@Component({
  selector: 'bs-order-tracking-page',
  standalone: true,
  imports: [
    PaginatorModule,
    TableModule,
    NgIf,
    InputTextModule,
    Button,
    JsonPipe,
    Ripple,
    DividerModule,
    ConfirmDialogModule
  ],
  providers: [DialogService, ConfirmationService, NotificationService],
  templateUrl: './order-tracking-page.component.html',
  styleUrl: './order-tracking-page.component.scss'
})
export class OrderTrackingPageComponent extends BaseComponent implements OnInit {
  private order = inject(OrderService)
  private _orders = signal<PaginatedList<OrderModel> | undefined>(undefined)

  orders = this._orders.asReadonly()

  pageNumber = signal<number>(1)
  pageSize = signal<number>(10)

  filterState = signal<SearchOrders>({})

  ngOnInit() {
    this.getData()
  }

  onPageChange(page: PaginatorState) {
    this.pageSize.set(page.rows!)
    this.pageNumber.set(page.page! + 1)

    this.getData()
  }

  calculateTotalOrderPrice(order: OrderModel) {
    return order.items
      .map(i => i.product.price * i.quantity)
      .reduce((a, b) => a + b)
  }

  calculateOrderItemTotalPrice(orderItem: OrderItemModel) {
    return orderItem.product.price * orderItem.quantity;
  }

  cancelOrder(id: Guid) {
    this.confirmation.confirm({
      icon: 'pi pi-exclamation-triangle',
      acceptIcon:"none",
      acceptButtonStyleClass: "p-button-danger",
      rejectIcon:"none",
      rejectButtonStyleClass:"p-button-text",
      header: 'Order cancellation',
      message: 'Are you sure you want to cancel the order?',
      accept: () => {
        const cancelOrderSubscription = this.order.cancelOrder(id)
          .subscribe()

        this.addSubscription(cancelOrderSubscription)
      }
    })
  }

  private getData() {
    const getMyOrdersSubscription = this.order.getMyOrders({
      ...this.filterState(),
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize()
    })
      .subscribe({
        next: (orders) => this._orders.set(orders)
      })

    this.addSubscription(getMyOrdersSubscription)
  }

  protected readonly OrderStatus = OrderStatus;
}
