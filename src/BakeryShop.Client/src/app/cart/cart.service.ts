import {computed, inject, Inject, Injectable, Injector, Signal, signal, WritableSignal} from '@angular/core';
import {ProductModel} from "../bakery/models/product.model";
import {Guid} from "guid-typescript";
import {ApiConfig} from "../config/apiConfig";
import {HttpClient} from "@angular/common/http";
import {toSignal} from "@angular/core/rxjs-interop";
import {tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private http = inject(HttpClient)
  private injector = inject(Injector)

  private readonly CartStorageKey = 'cart-items'
  private readonly baseUrl: string;

  private readonly _items: WritableSignal<CartItem[]>;
  items: Signal<CartItem[]>;

  constructor(@Inject('API_CONFIG') apiConfig: ApiConfig) {
    this.baseUrl = apiConfig.apiUrl + '/carts';
    this._items = signal<CartItem[]>([])
    this.items = this._items.asReadonly()
  }

  totalPrice = computed(() => {
    const items = this._items();

    if (items && items.length > 0) {
      return items
        .map((item: CartItem) => item.product?.price * item.quantity)
        .reduce((a, b) => a + b, 0);
    }

    return 0;
  });

  getMyItems() {
    const url = this.baseUrl + '/items';
    return this.http.get<CartItem[]>(url)
  }

  initItems(items: CartItem[]) {
    this._items.set(items);
  }

  private saveToStorage() {
    const items = JSON.stringify(this._items());
    localStorage.setItem(this.CartStorageKey, JSON.stringify(items));
  }

  addToCart(product: ProductModel, quantity: number) {
    return this.sendAddToCart(product.id, quantity).pipe(
      tap(() => {
        const existingItemIndex = this._items()
          .findIndex(item => item.product.id === product.id);

        if (existingItemIndex !== -1) {
          const updatedItems = [...this._items()];
          updatedItems[existingItemIndex].quantity += quantity;
          this._items.set(updatedItems);
        } else {
          this._items.set([...this._items(), { product, quantity }]);
        }
      })
    )
  }

  private sendAddToCart(id: Guid, quantity: number) {
    const url = this.baseUrl + '/items'
    return this.http.post(url, { id, quantity })
  }

  removeItem(id: Guid, quantity: number) {
    return this.sendRemoveFromCart(id, quantity).pipe(
      tap(() => {
        const index = this._items().findIndex(i => i.product.id === id)
        if (index !== -1) {
          let updatedItems = [...this._items()];

          updatedItems[index].quantity -= quantity;

          if (updatedItems[index].quantity <= 0) {
            updatedItems = updatedItems.filter(i => i.product.id !== id)
          }

          this._items.set(updatedItems);
        }
      })
    )
  }

  removeAllItem(id: Guid) {
    const item = this._items().find(i => i.product.id === id)
    if (item) {
      return this.removeItem(id, item.quantity)
    }

    return;
  }

  private sendRemoveFromCart(id: Guid, quantity: number) {
    const url = this.baseUrl + '/items'
    return this.http.put(url, { id, quantity })
  }
}

export interface CartItem {
  product: ProductModel;
  quantity: number;
}
