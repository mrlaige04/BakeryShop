import {InformationModel} from "../../bakery/models/information.model";

export interface CreateProduct {
  title: string;
  price: number;
  quantity: number;
  quantityType: number;
  description?: string | undefined;
  information?: InformationModel[] | undefined;
}
