import { Component } from '@angular/core';
import {RouterOutlet} from "@angular/router";
import {TabMenuModule} from "primeng/tabmenu";
import {MenuItem} from "primeng/api";
import {NgIf} from "@angular/common";

@Component({
  selector: 'bs-panel-wrapper',
  standalone: true,
  imports: [
    RouterOutlet,
    TabMenuModule,
    NgIf
  ],
  templateUrl: './panel-wrapper.component.html',
  styleUrl: './panel-wrapper.component.scss'
})
export class PanelWrapperComponent {
  items: MenuItem[] = [
    { label: 'Products', route: '/admin/products' },
    { label: 'Orders', route: '/admin/orders' },
  ]
}
