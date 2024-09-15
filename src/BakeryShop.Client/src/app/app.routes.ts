import { Routes } from '@angular/router';
import {HomePageComponent} from "./bakery/home-page/home-page.component";
import {ProductDetailsComponent} from "./bakery/product-details/product-details.component";
import {PanelWrapperComponent} from "./staff/panel-wrapper/panel-wrapper.component";
import {onlyAdminGuard} from "./staff/only-admin.guard";
import {CartPageComponent} from "./cart/cart-page/cart-page.component";
import {onlyAuthedGuard} from "./auth/only-authed.guard";
import {NoAccessComponent} from "./layout/no-access/no-access.component";

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'products/:id', component: ProductDetailsComponent, title: 'Product Page' },
  { path: 'auth', loadChildren: () => import('./auth/auth.routes').then(a => a.routes) },
  { path: 'cart', component: CartPageComponent, canActivate: [onlyAuthedGuard], title: 'Cart' },
  { path: 'no-access', component: NoAccessComponent },
  { path: 'admin', component: PanelWrapperComponent, canActivate: [onlyAdminGuard], loadChildren: () => import('./staff/staff.routes').then(s => s.routes) }
];
