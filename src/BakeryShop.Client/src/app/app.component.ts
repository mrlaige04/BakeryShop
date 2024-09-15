import {AfterViewInit, Component, inject, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TopbarComponent } from "./layout/topbar/topbar.component";
import {ToastModule} from "primeng/toast";
import {MessageService} from "primeng/api";
import {NotificationService} from "./utils/services/notification.service";
import {CartService} from "./cart/cart.service";
import {AuthService} from "./auth/auth.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TopbarComponent, ToastModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [MessageService, NotificationService],
})
export class AppComponent implements OnInit{
  title = 'BakeryShop.Client';
  private cart = inject(CartService)
  private auth = inject(AuthService)

  ngOnInit() {
    if (this.auth.isAuthenticated()) {
      const getItemsSubscription = this.cart.getMyItems()
        .subscribe({
          next: items => this.cart.initItems(items),
          complete: () => getItemsSubscription.unsubscribe()
        })
    }
  }
}
