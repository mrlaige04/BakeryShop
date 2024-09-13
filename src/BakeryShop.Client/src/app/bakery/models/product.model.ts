import {Guid} from "guid-typescript";
import {QuantityType} from "./quantity-type.enum";
import {CurrencyModel} from "./currency.model";

export interface ProductModel {
  id: Guid;
  title: string;
  description?: string | undefined;
  price: number;
  quantity: number;
  currency: CurrencyModel;
  quantityType: QuantityType;
}
