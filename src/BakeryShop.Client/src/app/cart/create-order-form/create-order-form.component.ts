import {Component, inject} from '@angular/core';
import {Button} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {OrderService} from "../../orders/order.service";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {DeliveryInfoModel} from "../../orders/models/delivery-info.model";
import {DynamicDialogRef} from "primeng/dynamicdialog";

@Component({
  selector: 'bs-create-order-form',
  standalone: true,
  imports: [
    Button,
    InputTextModule,
    InputTextareaModule,
    ReactiveFormsModule
  ],
  templateUrl: './create-order-form.component.html',
  styleUrl: './create-order-form.component.scss'
})
export class CreateOrderFormComponent {
  private order = inject(OrderService)
  private fb = inject(FormBuilder)
  private dialogRef = inject(DynamicDialogRef)
  private subscription: Subscription | undefined;

  triedToSubmit = false;

  deliveryForm = this.fb.group({
    city: this.fb.control('', [Validators.required]),
    street: this.fb.control('', [Validators.required]),
    houseNumber: this.fb.control('', [Validators.required]),
    additionalInfo: this.fb.control('')
  })

  submit() {
    this.triedToSubmit = true;
    if (!this.deliveryForm.valid) {
      return;
    }

    const model: DeliveryInfoModel = {
      city: this.deliveryForm.value.city!,
      street: this.deliveryForm.value.street!,
      houseNumber: this.deliveryForm.value.houseNumber!,
      additionalInfo: this.deliveryForm.value.additionalInfo!
    }

    this.subscription = this.order.createOrder(model)
      .subscribe({
        next: (_) => {
          this.dialogRef.close()
        }
      })
  }
}
