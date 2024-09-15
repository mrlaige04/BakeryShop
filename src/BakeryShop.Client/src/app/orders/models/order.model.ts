import {OrderStatus} from "./order-status.enum";
import {DeliveryInfoModel} from "./delivery-info.model";
import {OrderItemModel} from "./order-item.model";

export interface OrderModel {
  status: OrderStatus;
  additionalInfo?: string | undefined;
  items: OrderItemModel[];
  deliveryInfo: DeliveryInfoModel;
}
