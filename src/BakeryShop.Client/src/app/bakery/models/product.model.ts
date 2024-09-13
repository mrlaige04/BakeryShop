import {Guid} from "guid-typescript";
import {QuantityType} from "./quantity-type.enum";

export interface ProductModel {
  id: Guid;
  title: string;
  description?: string | undefined;
  price: number;
  quantity: number;
  quantityType: QuantityType;
}
