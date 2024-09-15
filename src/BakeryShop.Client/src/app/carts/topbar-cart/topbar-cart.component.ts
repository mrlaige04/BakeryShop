import {Component, inject} from '@angular/core';
import {CartService} from "../../cart/cart.service";
import {RouterLink} from "@angular/router";



@Component({
  selector: 'bs-topbar-cart',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './topbar-cart.component.html',
  styleUrl: './topbar-cart.component.scss'
})
export class TopbarCartComponent{
  private cart = inject(CartService)

  totalPrice = this.cart.totalPrice;

  constructor() {}
}
