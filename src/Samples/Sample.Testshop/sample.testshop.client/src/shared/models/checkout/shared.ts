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
export interface OrderRowRequest extends OrderRowBase {
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
