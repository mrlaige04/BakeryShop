import {Component, computed, inject, OnDestroy, OnInit, signal} from '@angular/core';
import {StaffService} from "../staff.service";
import {PaginatedList} from "../../models/paginated-list";
import {ProductModel} from "../../bakery/models/product.model";
import {SearchFilter} from "../../bakery/home-page/home-page.component";
import {debounceTime, map, Subscription} from "rxjs";
import {TableModule, TablePageEvent} from "primeng/table";
import {JsonPipe, NgIf} from "@angular/common";
import {Button} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {InputTextModule} from "primeng/inputtext";
import {DropdownModule} from "primeng/dropdown";
import {toSignal} from "@angular/core/rxjs-interop";
import {InputNumberModule} from "primeng/inputnumber";
import {QuantityType} from "../../bakery/models/quantity-type.enum";
import {InputTextareaModule} from "primeng/inputtextarea";
import {DialogService} from "primeng/dynamicdialog";
import {CreateProductFormComponent} from "../create-product-form/create-product-form.component";
import {ConfirmationService} from "primeng/api";
import { ConfirmDialogModule} from "primeng/confirmdialog";
import {BaseComponent} from "../../layout/base/base.component";
import {FormsModule} from "@angular/forms";
import {AdminSearchProducts} from "./AdminSearchProducts";
import {PaginatorModule, PaginatorState} from "primeng/paginator";
import {EditProductFormComponent} from "../edit-product-form/edit-product-form.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'bs-product-management-page',
  standalone: true,
  imports: [
    TableModule,
    NgIf,
    Button,
    DialogModule,
    InputTextModule,
    DropdownModule,
    InputNumberModule,
    InputTextareaModule,
    ConfirmDialogModule,
    FormsModule,
    JsonPipe,
    PaginatorModule,
    RouterLink
  ],
  providers: [DialogService, ConfirmationService],
  templateUrl: './product-management-page.component.html',
  styleUrl: './product-management-page.component.scss'
})
export class ProductManagementPageComponent extends BaseComponent implements OnInit, OnDestroy {
  private staff = inject(StaffService);

  products = signal<PaginatedList<ProductModel> | undefined>(undefined)
  filterState = signal<AdminSearchProducts>({})

  quantityTypes =
    Object.entries(QuantityType)
      .map(arr => {
        return { key: arr[0], value: arr[1] }
      })
      .filter(a => !isNaN(+a.key))

  pageSize = signal<number>(10)
  pageNumber = signal<number>(1)

  ngOnInit(): void {
    this.getData()
  }

  clearFilters() {
    this.filterState.set({
      query: undefined,
      priceFrom: undefined,
      priceTo: undefined,
      quantityType: undefined,
      quantityFrom: undefined,
      quantityTo: undefined
    })

    this.getData()
  }

  search() {
    this.getData()
  }

  private getData() {
    const getSubscription = this.staff
      .getProducts({
        ...this.filterState(),
        pageNumber: this.pageNumber(),
        pageSize: this.pageSize()
      })
      .pipe(debounceTime(2000))
      .subscribe({
        next: (list) => {
          this.products.set(list)
        },
        error: (err) => {}
      })

    this.addSubscription(getSubscription)
  }

  onPageChange(event: PaginatorState) {
    this.pageSize.set(event.rows!)
    this.pageNumber.set(event.page! + 1);

    this.getData()
  }

  openCreateDialog() {
    this.dialog.open<CreateProductFormComponent>(CreateProductFormComponent, {
      modal: true,
      header: 'Create Product'
    })
  }

  editProduct(product: ProductModel) {
    this.dialog.open<EditProductFormComponent>(EditProductFormComponent, {
      modal: true,
      header: 'Edit Product',
      data: { id: product.id }
    })
  }

  deleteProduct(product: ProductModel) {
    this.confirmation.confirm({
      message: 'Are you sure that you want to delete?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon:"none",
      acceptButtonStyleClass: "p-button-danger",
      rejectIcon:"none",
      rejectButtonStyleClass:"p-button-text",
      accept: () => {
        const deleteSubscription = this.staff.deleteProduct(product.id)
          .subscribe({
            next: () => {
              this.products.update(list => {
                if (list?.items) list.items = this.removeItem(list.items, product)
                return list;
              })
            }
          })

        this.addSubscription(deleteSubscription)
      }
    });
  }

  private removeItem(list: ProductModel[], item: ProductModel) {
    return list.filter(v => v.id !== item.id)
  }

  override ngOnDestroy() {
    super.ngOnDestroy();
  }

  protected readonly QuantityType = QuantityType;
}
