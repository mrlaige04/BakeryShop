import {Guid} from "guid-typescript";
import {QuantityType} from "../../bakery/models/quantity-type.enum";

export interface AdminSearchProducts {
  query?: string | undefined;
  priceFrom?: number | undefined;
  priceTo?: number | undefined;
  quantityFrom?: number | undefined;
  quantityTo?: number | undefined;
  quantityType?: QuantityType | undefined;
  pageNumber?: number | undefined;
  pageSize?: number | undefined;
}
