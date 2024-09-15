import {Inject, inject, Injectable, signal} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ApiConfig} from "../config/apiConfig";
import {PaginatedList} from "../models/paginated-list";
import {ProductModel} from "./models/product.model";
import {SearchProductsQuery} from "./requests/SearchProductsQuery";
import {Guid} from "guid-typescript";

@Injectable({
  providedIn: 'root'
})
export class BakeryService {
  private http = inject(HttpClient)
  private readonly baseUrl: string;

  defaultCurrency = signal('UAH')

  constructor(@Inject('API_CONFIG') private apiConfig: ApiConfig) {
    this.baseUrl = apiConfig.apiUrl;
  }

  searchProducts(query: SearchProductsQuery) {
    const url = this.baseUrl + '/products'
    let params = new HttpParams()
      .set('pageNumber', query.pageNumber ?? '')
      .set('pageSize', query.pageSize ?? '')
      .set('query', query.query ?? '')

    params = query.priceFrom ? params.set('priceFrom', query.priceFrom) : params;
    params = query.priceTo ? params.set('priceTo', query.priceTo) : params;

    return this.http.get<PaginatedList<ProductModel>>(url, { params })
  }

  getProduct(id: Guid) {
    const url = this.baseUrl + '/products/' + id;

    return this.http.get<ProductModel>(url)
  }
}
