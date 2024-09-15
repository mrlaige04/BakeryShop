import {ProductModel} from "../../bakery/models/product.model";

export interface OrderItemModel {
  product: ProductModel;
  quantity: number;
}
