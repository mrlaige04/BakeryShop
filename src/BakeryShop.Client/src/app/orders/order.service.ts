import {inject, Inject, Injectable} from '@angular/core';
import {ApiConfig} from "../config/apiConfig";
import {DeliveryInfoModel} from "./models/delivery-info.model";
import {HttpClient, HttpParams} from "@angular/common/http";
import {OrderModel} from "./models/order.model";
import {PaginatedList} from "../models/paginated-list";
import {SearchOrders} from "./requests/search-orders";
import {Guid} from "guid-typescript";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly baseUrl: string;
  private http = inject(HttpClient)

  constructor(@Inject('API_CONFIG') private config: ApiConfig) {
    this.baseUrl = config.apiUrl + '/orders';
  }

  getMyOrders(query: SearchOrders) {
    const url = this.baseUrl;
    let params = new HttpParams()
      .set('pageNumber', query.pageNumber ?? 1)
      .set('pageSize', query.pageSize ?? 10)

    return this.http.get<PaginatedList<OrderModel>>(url, { params })
  }

  createOrder(deliveryInfo: DeliveryInfoModel) {
    const url = this.baseUrl;
    return this.http.post<OrderModel>(url, deliveryInfo)
  }

  cancelOrder(id: Guid) {
    const url = this.baseUrl + '/' + id;
    return this.http.delete(url)
  }
}
