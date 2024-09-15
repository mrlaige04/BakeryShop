import {Component, inject} from '@angular/core';
import {CartService} from "../cart.service";
import {CardModule} from "primeng/card";
import {BakeryService} from "../../bakery/bakery.service";
import {Button} from "primeng/button";
import {DividerModule} from "primeng/divider";
import {Guid} from "guid-typescript";
import {ProductModel} from "../../bakery/models/product.model";
import {BaseComponent} from "../../layout/base/base.component";
import {DialogService} from "primeng/dynamicdialog";
import {NotificationService} from "../../utils/services/notification.service";
import {ConfirmationService} from "primeng/api";
import {debounceTime} from "rxjs";
import {CreateOrderFormComponent} from "../create-order-form/create-order-form.component";

@Component({
  selector: 'bs-cart-page',
  standalone: true,
  imports: [
    CardModule,
    Button,
    DividerModule
  ],
  providers: [DialogService, NotificationService, ConfirmationService],
  templateUrl: './cart-page.component.html',
  styleUrl: './cart-page.component.scss'
})
export class CartPageComponent extends BaseComponent{
  private cart = inject(CartService)
  private bakery = inject(BakeryService)

  currency = this.bakery.defaultCurrency;

  items = this.cart.items;

  totalPrice = this.cart.totalPrice;

  addItemQuantity(product: ProductModel) {
    const addToCartSubscription = this.cart.addToCart(product, 1)
      .pipe(debounceTime(2000))
      .subscribe({
        next: () => this.notification.success('OK', 'Item added')
      })

    this.addSubscription(addToCartSubscription)
  }

  minusQuantity(id: Guid) {
    const decreaseQuantitySubscription = this.cart.removeItem(id,1)
      .pipe(debounceTime(2000))
      .subscribe({
        next: () => this.notification.success('OK')
      });

    this.addSubscription(decreaseQuantitySubscription)
  }

  removeFromCart(id: Guid) {
    const removeSubscription = this.cart.removeAllItem(id)
      ?.pipe(debounceTime(2000))
      ?.subscribe({
        next: () => this.notification.success('OK', 'Item removed')
      });

    if (removeSubscription) this.addSubscription(removeSubscription)
  }

  createOrder() {
    this.dialog.open<CreateOrderFormComponent>(CreateOrderFormComponent, {
      modal: true,
      header: 'Create Order',
      style: { 'min-width': '40vw' }
    })
  }
}
