import { Customer, Gui, OrderRowResponse } from "./shared";

//Order model
export interface OrderResponse {
  orderId: number;
  clientOrderNumber: string;
  gui: Gui;
  status: string;
  cart: { items: OrderRowResponse[] };
  currency: string;
  locale: string;
  customer: Customer;
  countryCode: string;
  emailAddress: string;
  phoneNumber: string;
}

export type { OrderRowResponse };
