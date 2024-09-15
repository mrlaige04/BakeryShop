import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import {BaseComponent} from "../../layout/base/base.component";
import {StaffService} from "../staff.service";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {Guid} from "guid-typescript";
import {Subscription, tap} from "rxjs";
import {toSignal} from "@angular/core/rxjs-interop";
import {OrderStatus} from "../../orders/models/order-status.enum";
import {ProductModel} from "../../bakery/models/product.model";
import {OrderModel} from "../../orders/models/order.model";
import {InformationModel} from "../../bakery/models/information.model";
import {Button} from "primeng/button";
import {DropdownModule} from "primeng/dropdown";
import {InputTextModule} from "primeng/inputtext";
import {EditOrder} from "../requests/edit-order";

@Component({
  selector: 'bs-edit-order-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    Button,
    DropdownModule,
    InputTextModule
  ],
  templateUrl: './edit-order-form.component.html',
  styleUrl: './edit-order-form.component.scss'
})
export class EditOrderFormComponent extends BaseComponent implements OnInit, OnDestroy {
  private staff = inject(StaffService)
  private fb = inject(FormBuilder)
  private dialogConfig = inject(DynamicDialogConfig)
  private dialogRef = inject(DynamicDialogRef)
  private id: Guid = this.dialogConfig.data['id']

  private subscription: Subscription | undefined;

  triedToSubmit = false;

  private order$ = this.staff.getOrder(this.id)
    .pipe(
      tap(product => {
        this.initForm(product)
      })
    )

  order = toSignal(this.order$)

  editForm!: FormGroup;

  orderStatuses = this.getListFromOrderStatusEnum()

  private getListFromOrderStatusEnum() {
    return Object.entries(OrderStatus)
      .map(arr => {
        return { key: arr[0], value: arr[1] }
      })
      .filter(a => !isNaN(+a.key))
  }

  private initForm(order: OrderModel) {
    this.editForm = this.fb.group({
      status: this.fb.control(order.status.toFixed(), [Validators.required]),
      city: this.fb.control(order.deliveryInfo.city, [Validators.required]),
      street: this.fb.control(order.deliveryInfo.street, [Validators.required]),
      houseNumber: this.fb.control(order.deliveryInfo.houseNumber, [Validators.required]),
    })
  }


  ngOnInit() {

  }

  submit() {
    this.triedToSubmit = true;
    if (!this.editForm.valid) {
      return;
    }

    const model: EditOrder = {
      city: this.editForm.value.city,
      street: this.editForm.value.street,
      houseNumber: this.editForm.value.houseNumber,
      status: +this.editForm.value.status
    }

    const subscription = this.staff.updateOrder(this.id, model)
      .subscribe({
        next: () => {
          this.dialogRef.close()
        }
      })
  }

  override ngOnDestroy() {
    super.ngOnDestroy();

    this.subscription?.unsubscribe();
  }
}
