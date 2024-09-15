import {AfterViewInit, Component, inject, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TopbarComponent } from "./layout/topbar/topbar.component";
import {ToastModule} from "primeng/toast";
import {MessageService} from "primeng/api";
import {NotificationService} from "./utils/services/notification.service";
import {CartService} from "./cart/cart.service";

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

  ngOnInit() {
    const getItemsSubscription = this.cart.getMyItems().subscribe({
      next: items => this.cart.initItems(items),
    })
  }
}
