import {Component, inject, OnInit} from '@angular/core';
import {DialogConfig} from "@angular/cdk/dialog";
import {BaseComponent} from "../../layout/base/base.component";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {DividerModule} from "primeng/divider";
import {Button} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputNumberModule} from "primeng/inputnumber";
import {DropdownModule} from "primeng/dropdown";
import {QuantityType} from "../../bakery/models/quantity-type.enum";
import {InputTextareaModule} from "primeng/inputtextarea";
import {StaffService} from "../staff.service";
import {Guid} from "guid-typescript";
import {toSignal} from "@angular/core/rxjs-interop";
import {tap} from "rxjs";
import {ProductModel} from "../../bakery/models/product.model";
import {JsonPipe} from "@angular/common";
import {InformationModel} from "../../bakery/models/information.model";

@Component({
  selector: 'bs-edit-product-form',
  standalone: true,
  imports: [
    DividerModule,
    Button,
    ReactiveFormsModule,
    InputTextModule,
    InputNumberModule,
    DropdownModule,
    InputTextareaModule,
    JsonPipe
  ],
  templateUrl: './edit-product-form.component.html',
  styleUrl: './edit-product-form.component.scss',
  providers: [DialogConfig]
})
export class EditProductFormComponent extends BaseComponent implements OnInit {
  private staff = inject(StaffService)
  private fb = inject(FormBuilder)
  private dialogConfig = inject(DynamicDialogConfig)
  private dialogRef = inject(DynamicDialogRef)
  private id: Guid = this.dialogConfig.data['id']

  private product$ = this.staff.getProduct(this.id)
    .pipe(
      tap(product => {
        this.initForm(product)
        this.initializeInformationArray(product.information ?? [])
      })
    )

  product = toSignal(this.product$)

  quantityTypes = this.getListFromQuantityTypeEnum()

  editForm!: FormGroup;

  private getListFromQuantityTypeEnum() {
    return Object.entries(QuantityType)
      .map(arr => {
        return { key: arr[0], value: arr[1] }
      })
      .filter(a => !isNaN(+a.key))
  }

  private initForm(product: ProductModel) {
    this.editForm = this.fb.group({
      title: this.fb.control(product.title, [Validators.required]),
      price: this.fb.control(product.price, [Validators.required]),
      quantity: this.fb.control(product.quantity, [Validators.required]),
      quantityType: this.fb.control(product.quantityType.toFixed(), [Validators.required]),
      description: this.fb.control(product.description, []),
      information: this.fb.array([])
    })
  }

  ngOnInit() {

  }

  private initializeInformationArray(information: InformationModel[]) {
    information?.forEach(item => {
      this.informationArray.push(
        this.fb.group({
          title: this.fb.control(item.title, [Validators.required]),
          description: this.fb.control(item.description, [Validators.required])
        })
      )
    })
  }

  get informationArray() {
    return this.editForm.get('information') as FormArray
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
    const value = this.editForm.value;
    const model: ProductModel = {
      title: value.title,
      price: value.price,
      quantity: value.quantity,
      quantityType: +value.quantityType,
      description: value.description,
      information: this.informationArray.value,
      id: this.id
    };

    const editSubscription = this.staff.updateProduct(this.id, model)
      .subscribe({
        next: (dto) => this.dialogRef.close()
      })

    this.addSubscription(editSubscription)
  }
}
