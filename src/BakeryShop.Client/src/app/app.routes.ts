import { Routes } from '@angular/router';
import {HomePageComponent} from "./bakery/home-page/home-page.component";
import {ProductDetailsComponent} from "./bakery/product-details/product-details.component";

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'products/:id', component: ProductDetailsComponent },
  { path: 'auth', loadChildren: () => import('./auth/auth.routes').then(a => a.routes) }
];
