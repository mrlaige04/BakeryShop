import {Routes} from "@angular/router";
import {OrderManagementPageComponent} from "./order-management-page/order-management-page.component";
import {ProductManagementPageComponent} from "./product-management-page/product-management-page.component";

export const routes: Routes = [
  { path: 'products', component: ProductManagementPageComponent },
  { path: 'order', component: OrderManagementPageComponent }
]
