import { OrderRow } from "./shared";

export interface CreateRecurringOrderRequest {
  currency: string;
  clientOrderNumber: string;
  cart: { items: OrderRow[] };
  merchantSettings: RecurringMerchantSettings;
}

export interface RecurringMerchantSettings {
  pushUri: string;
  checkoutValidationCallBackUri?: string;
}

export interface ChangePaymentMethodRequest {
  termsUrl: string;
}
export interface ChangePaymentMethodResponse {
  snippet: string;
  expiration: string;
}