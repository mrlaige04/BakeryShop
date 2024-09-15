import {Component, computed, inject, OnDestroy, OnInit, signal} from '@angular/core';
import {BakeryService} from "../bakery.service";
import { NgIf} from "@angular/common";
import {InputTextModule} from "primeng/inputtext";
import {QuantityType} from "../models/quantity-type.enum";
import {debounceTime, Subscription} from "rxjs";
import {PaginatedList} from "../../models/paginated-list";
import {ProductModel} from "../models/product.model";
import {CardModule} from "primeng/card";
import {TruncatePipe} from "../../utils/pipes/truncate.pipe";
import {RouterLink} from "@angular/router";
import {Button} from "primeng/button";
import {PaginatorModule, PaginatorState} from "primeng/paginator";
import {ProgressSpinnerModule} from "primeng/progressspinner";

@Component({
  selector: 'bs-home-page',
  standalone: true,
  imports: [
    NgIf,
    InputTextModule,
    CardModule,
    TruncatePipe,
    RouterLink,
    Button,
    PaginatorModule,
    ProgressSpinnerModule,
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent implements OnInit, OnDestroy{
  private bakery = inject(BakeryService)

  private subscriptions: Subscription[] = []

  currency = this.bakery.defaultCurrency;

  filterState = signal<SearchFilter>({})

  pageSize = signal<number>(10)
  pageNumber = signal<number>(1)
  skipRows = computed(() => (this.pageNumber() - 1) * this.pageSize())

  products = signal<PaginatedList<ProductModel> | null>(null)

  ngOnInit() {
    this.getData()
  }

  onPageChange(event: PaginatorState) {
    this.pageSize.set(event.rows!)
    this.pageNumber.set(event.page! + 1)

    this.getData()
  }

  onFilter() {
    this.getData()
  }

  private getData() {
    const getSubscription = this.bakery
      .searchProducts({
        ...this.filterState(),
        pageNumber: this.pageNumber(),
        pageSize: this.pageSize()
      })
      .pipe(debounceTime(500))
      .subscribe({
        next: (list) => {
          this.products.set(list)
        },
        error: (err) => {}
      })

    this.subscriptions.push(getSubscription)
  }

  protected readonly QuantityType = QuantityType;

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }
}


export interface SearchFilter {
  query?: string | undefined;
  priceFrom?: number | undefined;
  priceTo?: number | undefined;
}
