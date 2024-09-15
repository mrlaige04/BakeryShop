import {inject, Injectable} from '@angular/core';
import {MessageService} from "primeng/api";

@Injectable({
  providedIn: 'root',
  deps: [MessageService]
})
export class NotificationService {
  private message = inject(MessageService)

  success(title: string, message?: string) {
    this.addMessage(title, 'success', message)
  }

  error(title: string, message?: string) {
    this.addMessage(title, 'error', message)
  }

  warning(title: string, message?: string) {
    this.addMessage(title, 'warning', message)
  }

  private addMessage(title: string, severity: string, message?: string) {
    this.message.add({
      closable: true,
      severity,
      summary: title,
      detail: message,
      life: 3000
    })
  }
}
