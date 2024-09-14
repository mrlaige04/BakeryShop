import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TopbarComponent } from "./layout/topbar/topbar.component";
import {ToastModule} from "primeng/toast";
import {MessageService} from "primeng/api";
import {NotificationService} from "./utils/services/notification.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TopbarComponent, ToastModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [MessageService, NotificationService],
})
export class AppComponent {
  title = 'BakeryShop.Client';
}
