export interface DeliveryInfoModel {
  city: string;
  street: string;
  houseNumber: string;
  additionalInfo: string;
  deliveryDate?: Date | undefined;
}
