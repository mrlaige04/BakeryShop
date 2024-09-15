import {Inject, inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ApiConfig} from "../config/apiConfig";
import {Guid} from "guid-typescript";
import {PaginatedList} from "../models/paginated-list";
import {ProductModel} from "../bakery/models/product.model";
import {CreateProduct} from "./requests/create-product";
import {AdminSearchProducts} from "./product-management-page/AdminSearchProducts";
import {BakeryService} from "../bakery/bakery.service";
import {SearchOrders} from "../orders/requests/search-orders";
import {OrderModel} from "../orders/models/order.model";
import {EditOrder} from "./requests/edit-order";

@Injectable({
  providedIn: 'root'
})
export class StaffService {
  private http = inject(HttpClient)
  private bakery = inject(BakeryService)
  private readonly baseUrl: string;

  constructor(@Inject('API_CONFIG') private apiConfig: ApiConfig) {
    this.baseUrl = apiConfig.apiUrl + '/staff';
  }

  getProduct(id: Guid) {
    return this.bakery.getProduct(id);
  }

  getProducts(query: AdminSearchProducts) {
    const url = this.baseUrl + '/products'

    let params = new HttpParams()
      .set('pageNumber', query.pageNumber ?? '')
      .set('pageSize', query.pageSize ?? '')
      .set('query', query.query ?? '')

    params = query.priceFrom != undefined ? params.set('priceFrom', query.priceFrom) : params;
    params = query.priceTo != undefined ? params.set('priceTo', query.priceTo) : params;
    params = query.quantityFrom != undefined ? params.set('quantityFrom', query.quantityFrom) : params;
    params = query.quantityTo != undefined ? params.set('quantityTo', query.quantityTo) : params;
    params = query.quantityType ? params.set('quantityType', query.quantityType) : params;

    return this.http.get<PaginatedList<ProductModel>>(url, { params });
  }

  createProduct(model: CreateProduct) {
    const url = this.baseUrl + '/products'
    return this.http.post<Guid>(url, model)
  }

  updateProduct(id: Guid, product: ProductModel) {
    const url = this.baseUrl + '/products/' + id;
    return this.http.put<ProductModel>(url, product)
  }

  deleteProduct(id: Guid) {
    const url = this.baseUrl + '/products/' + id;
    return this.http.delete(url)
  }

  getOrder(id: Guid) {
    const url = this.baseUrl + '/orders/' + id;
    return this.http.get<OrderModel>(url)
  }

  getOrders(query: SearchOrders) {
    const url = this.baseUrl + '/orders'

    const params = new HttpParams()
      .set('pageNumber', query.pageNumber ?? '')
      .set('pageSize', query.pageSize ?? '')

    return this.http.get<PaginatedList<OrderModel>>(url, { params });
  }

  updateOrder(id: Guid, order: EditOrder) {
    const url = this.baseUrl + '/orders/' + id;
    return this.http.put<EditOrder>(url, order)
  }
}
