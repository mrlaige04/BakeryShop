import {environment} from "../../../environments/environment";
import {ApiConfig} from "../../config/apiConfig";
import {Provider} from "@angular/core";

export const apiConfigFactory = (): ApiConfig => environment.apiConfig;

export const apiConfigProvider: Provider = {
  provide: 'API_CONFIG',
  useFactory: apiConfigFactory
}
