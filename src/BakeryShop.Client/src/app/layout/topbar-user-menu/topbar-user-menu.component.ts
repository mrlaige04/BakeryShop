import {Component, inject} from '@angular/core';
import {TopbarCartComponent} from "../../carts/topbar-cart/topbar-cart.component";
import {AuthService} from "../../auth/auth.service";
import {RouterLink} from "@angular/router";
import {Button} from "primeng/button";

@Component({
  selector: 'bs-topbar-user-menu',
  standalone: true,
  imports: [
    TopbarCartComponent,
    RouterLink,
    Button,
  ],
  templateUrl: './topbar-user-menu.component.html',
  styleUrl: './topbar-user-menu.component.scss'
})
export class TopbarUserMenuComponent {
  private authService = inject(AuthService);
  isAuthenticated = this.authService.isAuthenticated;
  isAdmin = this.authService.isAdmin;

  logout() {
    this.authService.logout();
  }
}
