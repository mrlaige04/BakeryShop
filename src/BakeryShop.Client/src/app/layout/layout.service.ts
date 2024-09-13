import {Injectable, signal} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  private _topbarVisible = signal(true)
  public topbarVisible = this._topbarVisible.asReadonly();

  constructor() { }

  hideNavbar() {
    this._topbarVisible.set(false)
  }

  showNavbar() {
    this._topbarVisible.set(true);
  }
}
