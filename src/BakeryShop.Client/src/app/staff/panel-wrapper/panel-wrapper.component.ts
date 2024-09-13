import { Component } from '@angular/core';
import {RouterOutlet} from "@angular/router";

@Component({
  selector: 'bs-panel-wrapper',
  standalone: true,
  imports: [
    RouterOutlet
  ],
  templateUrl: './panel-wrapper.component.html',
  styleUrl: './panel-wrapper.component.scss'
})
export class PanelWrapperComponent {

}
