import {Component, inject, OnInit} from '@angular/core';
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {Button} from "primeng/button";
import {DropdownModule} from "primeng/dropdown";
import {InputNumberModule} from "primeng/inputnumber";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {StaffService} from "../staff.service";
import {QuantityType} from "../../bakery/models/quantity-type.enum";
import {DialogModule} from "primeng/dialog";
import {FormArray, FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {CreateProduct} from "../requests/create-product";
import {JsonPipe, KeyValuePipe} from "@angular/common";
import { OverlayOptions} from "primeng/api";

import {BaseComponent} from "../../layout/base/base.component";
import {NotificationService} from "../../utils/services/notification.service";
import {ToastModule} from "primeng/toast";
import {DividerModule} from "primeng/divider";

@Component({
  selector: 'bs-create-product-form',
  standalone: true,
  imports: [
    Button,
    DropdownModule,
    InputNumberModule,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    DialogModule,
    ReactiveFormsModule,
    JsonPipe,
    KeyValuePipe,
    ToastModule,
    DividerModule
  ],
  templateUrl: './create-product-form.component.html',
  styleUrl: './create-product-form.component.scss',
  providers: [NotificationService]
})
export class CreateProductFormComponent extends BaseComponent implements OnInit{
  private staff = inject(StaffService);
  private dialogRef = inject(DynamicDialogRef<CreateProductFormComponent>)
  private fb = inject(FormBuilder)

  overlay: OverlayOptions = {
    autoZIndex: true
  }

  quantityTypes: { key: string, value: string | QuantityType }[];

  constructor(private dialogConfig: DynamicDialogConfig) {
    super();

    this.quantityTypes = Object.entries(QuantityType)
      .map(arr => {
        return { key: arr[0], value: arr[1] }
      }).filter(a => !isNaN(+a.key))
  }


  ngOnInit() {

  }

  productForm = this.fb.group({
    title: this.fb.control(null, [Validators.required]),
    price: this.fb.control(null, [Validators.required]),
    quantity: this.fb.control(null, [Validators.required]),
    quantityType: this.fb.control(null, [Validators.required]),
    description: this.fb.control(null, []),
    information: this.fb.array([])
  })

  get informationArray() {
    return this.productForm.get('information') as FormArray
  }

  addInfo() {
    const control = this.fb.group({
      title: this.fb.control(null, [Validators.required]),
      description: this.fb.control(null, [Validators.required])
    })

    this.informationArray.push(control)
  }

  removeInfo(index: number) {
    this.informationArray.removeAt(index)
  }

  submit() {
    if (!this.productForm.valid) {
      return;
    }

    const model: CreateProduct = {
      title: this.productForm.value.title!,
      price: this.productForm.value.price!,
      quantity: this.productForm.value.quantity!,
      quantityType: +this.productForm.value.quantityType!,
      description: this.productForm.value.description!,
    }

    model.information ??= this.informationArray.value;

    const createSubscription = this.staff.createProduct(model)
      .subscribe({
        next: productId => {
          this.notification.success('OK', 'Product created')
          this.dialogRef.close()
        },
        error: err => {

        }
      })

    this.addSubscription(createSubscription)
  }
}
