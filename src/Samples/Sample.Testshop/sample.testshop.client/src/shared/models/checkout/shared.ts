export interface OrderRowBase {
  articleNumber: string;
  name: string;
  unit: string;
  merchantData?: string;
  shippingInfo?: string;
  temporaryReference?: string;
  rowNumber: number;
  rowType?: string;
}

export interface OrderRowResponse extends OrderRowBase {
  quantity: MinorUnit;
  unitPrice: MinorUnit;
  discountAmount?: MinorUnit;
  discountPercent?: MinorUnit;
  discountValue?: MinorUnit;
  vatPercent: MinorUnit;
}
export interface OrderRow extends OrderRowBase {
  quantity: number;
  unitPrice: number;
  discountAmount?: number;
  discountPercent?: number;
  discountValue?: number;
  vatPercent: number;
}

export interface MinorUnit {
  inLowestMonetaryUnit: number;
}
export enum DiscountType {
  Percentage,
  Amount,
}

export interface AdmittanceDetails {
  retreiveDate: string;
  cached: string;
  merchantId: string;
  merchantName: string;
  companyAdminInvoiceClientId: string;
  companyInvoiceClientId: string;
  individualAccountClientId: string;
  individualAdminInvoiceClientId: string;
  individualPartPaymentClientId: string;
  paymentGatewayMerchantId: string;
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
/// Checkout order data without minor currency
export interface Data extends OrderData {
  gui: Gui;
}

export interface OrderData {
  orderId: number;
  clientOrderNumber: string;
  status: string;
  cart: { items: OrderRow[] };
  currency: string;
  locale: string;
  customer: Customer;
  countryCode: string;
  emailAddress: string;
  phoneNumber: string;
}

export interface RecurringToken {
  token: string;
  status: string;
  currency: string;
  paymentMethod: string;
  paymentMethodDetails: RecurringPaymentMethodDetails;
}

export interface RecurringPaymentMethodDetails {
  expiryMonth: number;
  expiryYear: number;
}
