import { OrderRow } from "./shared";

interface CreateOrderModel {
  countryCode: string;
  currency: string;
  locale: string;
  clientOrderNumber: string;
  merchantSettings: MerchantSettings;
  cart: Cart;
  presetValues?: PresetValue[];
  identityFlags?: IdentityFlags;
  requireElectronicIdAuthentication?: boolean;
  partnerKey?: string;
  merchantData?: string;
  shippingInformation?: ShippingInformation;
  validation: OrderValidation;
  recurring?: boolean;
  validationCallback?: ValidationCallback;
  checkoutPushData: CheckoutPushData;
  merchantId: number;
}

export interface PartPayment {
  campaignCode: string;
  description: string;
}

export interface Cart {
  items: OrderRow[];
}

export interface MerchantSettings {
  webhookUri?: string;
  checkoutUri: string;
  confirmationUri: string;
  termsUri: string;
  pushUri: string;
  checkoutValidationCallBackUri?: string;
  activePartPaymentCampaigns?: number[];
  promotedPartPaymentCampaign?: number;
}

interface IdentityFlags {
  hideNotYou: boolean;
  hideChangeAddress: boolean;
  hideAnonymous: boolean;
}

interface ShippingInformation {
  enableShipping: boolean;
  enforceFallback?: boolean;
  weight?: number;
  //   tags?: { [key: string]: boolean };
  //   fallbackOptions: FallbackOptions;
  //   shouldRejectShippingSession?: boolean;
}
export interface Country {
  name?: string;
  isoCode: string;
  vats?: [];
}
export interface PresetValue {
  typeName: string;
  value: string | boolean;
  isReadonly: boolean;
}

export interface FallbackOptions {
  id: string;
  carrier: string;
  name: string;
  price: number;
  shippingFee: number;
  description: string;
}

export interface OrderValidation {
  minAge: number;
  paymentFailureResiliency: boolean;
}

export interface ValidationCallback {
  useValidationCallback: boolean;
  validate: boolean;
  errorMessage: string;
}
export interface CheckoutPushData {
  responseDelay: number;
  responseStatusCode: number;
}
export default CreateOrderModel;
