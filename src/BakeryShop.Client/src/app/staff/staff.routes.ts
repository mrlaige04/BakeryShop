import {Routes} from "@angular/router";
import {OrderManagementPageComponent} from "./order-management-page/order-management-page.component";
import {ProductManagementPageComponent} from "./product-management-page/product-management-page.component";
import {UserManagementPageComponent} from "./user-management-page/user-management-page.component";

export const routes: Routes = [
  { path: 'products', component: ProductManagementPageComponent },
  { path: 'orders', component: OrderManagementPageComponent },
  { path: 'users', component: UserManagementPageComponent },
]
