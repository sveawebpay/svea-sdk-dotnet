export interface OrderRow {
  articleNumber: string;
  name: string;
  quantity: number;
  unitPrice: number;
  unit: string;
  discountAmount?: number;
  discountPercent?: number;
  discountType?: DiscountType;
  discountValue?: number;
  merchantData?: string;
  shippingInfo?: string;
  temporaryReference?: string;
  vatPercent: number;
  rowNumber: number;
  rowType?: string;
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
