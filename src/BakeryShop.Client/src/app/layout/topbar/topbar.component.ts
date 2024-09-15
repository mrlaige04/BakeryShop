import {Component, inject} from '@angular/core';
import {TopbarUserMenuComponent} from "../topbar-user-menu/topbar-user-menu.component";
import {RouterLink} from "@angular/router";
import {LayoutService} from "../layout.service";


@Component({
  selector: 'bs-topbar',
  standalone: true,
  imports: [
    TopbarUserMenuComponent,
    RouterLink
  ],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.scss'
})
export class TopbarComponent {
  private layout = inject(LayoutService);

  topbarVisible = this.layout.topbarVisible;
}
