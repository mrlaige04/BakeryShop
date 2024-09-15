import { Component } from '@angular/core';
import {CardModule} from "primeng/card";
import {Button} from "primeng/button";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'bs-no-access',
  standalone: true,
  imports: [
    CardModule,
    Button,
    RouterLink
  ],
  templateUrl: './no-access.component.html',
  styleUrl: './no-access.component.scss'
})
export class NoAccessComponent {

}
