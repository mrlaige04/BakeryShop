import {Component, inject, OnDestroy, OnInit, signal} from '@angular/core';
import {StaffService} from "../staff.service";
import {OrderModel} from "../../orders/models/order.model";
import {PaginatedList} from "../../models/paginated-list";
import {OrderStatus} from "../../orders/models/order-status.enum";
import {BaseComponent} from "../../layout/base/base.component";
import {DialogService} from "primeng/dynamicdialog";
import {ConfirmationService} from "primeng/api";
import {debounceTime} from "rxjs";
import {PaginatorModule, PaginatorState} from "primeng/paginator";
import {NgIf} from "@angular/common";
import {TableModule} from "primeng/table";
import {Button} from "primeng/button";
import {OrderItemModel} from "../../orders/models/order-item.model";
import {EditOrderFormComponent} from "../edit-order-form/edit-order-form.component";
import {Guid} from "guid-typescript";

@Component({
  selector: 'bs-order-management-page',
  standalone: true,
  imports: [
    NgIf,
    TableModule,
    PaginatorModule,
    Button
  ],
  providers: [DialogService, ConfirmationService],
  templateUrl: './order-management-page.component.html',
  styleUrl: './order-management-page.component.scss'
})
export class OrderManagementPageComponent extends BaseComponent implements OnInit, OnDestroy {
  private staff = inject(StaffService);

  orders = signal<PaginatedList<OrderModel> | undefined>(
    undefined
  )

  orderStatuses =
    Object.entries(OrderStatus)
      .map(arr => {
        return { key: arr[0], value: arr[1] }
      })
      .filter(a => !isNaN(+a.key))

  pageSize = signal<number>(10)
  pageNumber = signal<number>(1)

  ngOnInit() {
    this.getData()
  }

  private getData() {
    const getSubscription = this.staff
      .getOrders({
        pageNumber: this.pageNumber(),
        pageSize: this.pageSize()
      })
      .pipe(debounceTime(2000))
      .subscribe({
        next: (list) => {
          this.orders.set(list)
        },
        error: (err) => {}
      })

    this.addSubscription(getSubscription)
  }

  onPageChange(event: PaginatorState) {
    this.pageSize.set(event.rows!)
    this.pageNumber.set(event.page! + 1);

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

  openEditOrder(id: Guid) {
    this.dialog.open<EditOrderFormComponent>(EditOrderFormComponent, {
      modal: true,
      header: 'Edit order',
      data: { id }
    })
  }

  override ngOnDestroy() {
    super.ngOnDestroy();
  }

  protected readonly OrderStatus = OrderStatus;
}
