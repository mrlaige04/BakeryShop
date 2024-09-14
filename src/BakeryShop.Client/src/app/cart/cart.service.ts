import {computed, Injectable, signal} from '@angular/core';
import {ProductModel} from "../bakery/models/product.model";

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items = signal<CartItem[]>([])

  totalPrice = computed(() => {
    if (this.items().length > 0) {
      return this.items()
        .map((item: CartItem) => item.product.price * item.quantity)
        .reduce((a, b) => a + b);
    }
    return 0;
  })

  constructor() { }

  addToCart(product: ProductModel, quantity: number) {
    const existingItemIndex = this.items()
      .findIndex(item => item.product.id === product.id);

    if (existingItemIndex !== -1) {
      const updatedItems = [...this.items()];
      updatedItems[existingItemIndex].quantity += quantity;
      this.items.set(updatedItems);
    } else {
      this.items.set([...this.items(), { product, quantity }]);
    }
  }
}

export interface CartItem {
  product: ProductModel;
  quantity: number;
}
