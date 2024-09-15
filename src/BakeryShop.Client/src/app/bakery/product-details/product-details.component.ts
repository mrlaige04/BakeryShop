import {Component, inject, signal} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BakeryService} from "../bakery.service";
import {toSignal} from "@angular/core/rxjs-interop";
import {ProgressSpinnerModule} from "primeng/progressspinner";
import {QuantityType} from "../models/quantity-type.enum";
import {CardModule} from "primeng/card";
import {InputNumberModule} from "primeng/inputnumber";
import {Button} from "primeng/button";
import {PanelModule} from "primeng/panel";
import {AccordionModule} from "primeng/accordion";
import {NgForOf, NgIf} from "@angular/common";
import {DividerModule} from "primeng/divider";
import {CartService} from "../../cart/cart.service";
import {FormsModule} from "@angular/forms";
import {AuthService} from "../../auth/auth.service";
import {BaseComponent} from "../../layout/base/base.component";
import {DialogService} from "primeng/dynamicdialog";
import {ConfirmationService, MessageService} from "primeng/api";
import {NotificationService} from "../../utils/services/notification.service";


@Component({
  selector: 'bs-product-details',
  standalone: true,
  imports: [
    ProgressSpinnerModule,
    CardModule,
    InputNumberModule,
    Button,
    PanelModule,
    AccordionModule,
    NgIf,
    NgForOf,
    DividerModule,
    FormsModule
  ],
  providers: [DialogService, ConfirmationService, NotificationService, MessageService],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent extends BaseComponent {
  private activatedRoute = inject(ActivatedRoute);
  private bakery = inject(BakeryService)
  private auth = inject(AuthService)

  isAuthenticated = this.auth.isAuthenticated;

  currency = this.bakery.defaultCurrency;

  private cart = inject(CartService)

  private id = this.activatedRoute.snapshot.params['id'];

  private product$ = this.bakery.getProduct(this.id);
  product = toSignal(this.product$);
  protected readonly QuantityType = QuantityType;

  cartQuantity = signal<number>(0)

  addToCart() {
    const addToCartSubscription = this.cart.addToCart(this.product()!, this.cartQuantity())
      .subscribe({
        next: () => this.notification.success('OK', 'Product added')
      })

    this.addSubscription(addToCartSubscription)
  }
}
