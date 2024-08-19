import { OrderRow } from "./shared";

//Order model
export interface OrderResponse {
  orderId: number;
  clientOrderNumber: string;
  gui: Gui;
  status: string;
  cart: { items: OrderRow[] };
  currency: string;
  locale: string;
  customer: Customer;
  countryCode: string;
  emailAddress: string;
  phoneNumber: string;
}

export interface Gui {
  snippet: string;
}

export interface Customer {
  Id: number;
  NationalId: string;
  Country: string;
  IsCompany: boolean;
  VatNumber: string;
  IsVerified: boolean;
}

export interface Address {
  FullName: string;
  FirstName: string;
  LastName: string;
  StreetAddress: string;
  StreetAddress2: string;
  StreetAddress3: string;
  CoAddress: string;
  PostalCode: string;
  CountryCode: string;
}
export type { OrderRow };
