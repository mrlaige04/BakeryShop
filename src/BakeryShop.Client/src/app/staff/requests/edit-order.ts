import {OrderStatus} from "../../orders/models/order-status.enum";

export interface EditOrder {
  status: OrderStatus;
  city: string;
  street: string;
  houseNumber: string;
}
