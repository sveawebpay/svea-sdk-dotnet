import { cartOptions } from "../../../../shared/constants/cart.constants";
import CreateOrderModel, {
  MerchantSettings,
  PresetValue,
  RequestCart,
} from "../../../../shared/models/checkout/CreateOrderModel";

export interface CreateOrderForm {
  emailPresetValue: FormPresetValue;
  companyPresetValue: FormPresetValue;
  postalCodePresetValue: FormPresetValue;
  nationalIdPresetValue: FormPresetValue;
  phoneNumberPresetValue: FormPresetValue;
  cart: RequestCart;
  identityFlags: FormIdentityFlags;
  requireElectronicAuthentication: boolean;
  recurring: boolean;
  merchantId: number;
  reloadOrderManually: boolean;
  validationCallback: ValidationCallback;
  validation: OrderValidation;
  shippingInformation: ShippingInformation;
  merchantData: string;
  partnerKey?: string;
  clientOrderNumber: string;
  merchantSettings?: MerchantSettings;
  checkoutPushData?: CheckoutPushData;
  cartPreset: number;
  currency: string;
  locale: string;
  countryCode: string;
}

export interface FormPresetValue {
  value: string;
  isLocked: boolean;
}

interface CheckoutPushData {
  responseDelay: number;
  responseStatusCode: number;
}

interface FormIdentityFlags {
  hideNotYou: boolean;
  hideChangeAddress: boolean;
  hideAnonymous: boolean;
}

interface ValidationCallback {
  useValidationCallback: boolean;
  validate: boolean;
  errorMessage: string;
}

interface OrderValidation {
  minAge: number;
  paymentFailureResiliency: boolean;
}

interface ShippingInformation {
  enableShipping: boolean;
  enforceFallback: boolean;
  weight: number;
  //   tags: { [key: string]: boolean };
  //   fallbackOptions: FallbackOptions;
  //   shouldRejectShippingSession: boolean;
}

// interface FallbackOptions {
//   id: string;
//   carrier: string;
//   name: string;
//   price: number;
//   shippingFee: number;
//   description: string;
// }

export const FormDataToCreateOrderModel = (
  data: CreateOrderForm
): CreateOrderModel => {
  const checkoutPresetValue: PresetValue[] = [];

  // ----- PresetValues mapping -----
  if (data.emailPresetValue?.value?.trim() !== "") {
    checkoutPresetValue.push({
      typeName: "EmailAddress",
      isReadonly: data.emailPresetValue.isLocked,
      value: data.emailPresetValue.value,
    });
  }
  if (data.nationalIdPresetValue?.value?.trim() !== "") {
    checkoutPresetValue.push({
      typeName: "NationalId",
      isReadonly: data.nationalIdPresetValue.isLocked,
      value: data.nationalIdPresetValue.value,
    });
  }
  if (data.postalCodePresetValue?.value?.trim() !== "") {
    checkoutPresetValue.push({
      typeName: "PostalCode",
      isReadonly: data.postalCodePresetValue.isLocked,
      value: data.postalCodePresetValue.value,
    });
  }
  if (data.phoneNumberPresetValue?.value?.trim() !== "") {
    checkoutPresetValue.push({
      typeName: "PhoneNumber",
      isReadonly: data.phoneNumberPresetValue.isLocked,
      value: data.phoneNumberPresetValue.value,
    });
  }

  if (data.companyPresetValue?.value?.trim() !== "") {
    checkoutPresetValue.push({
      typeName: "IsCompany",
      isReadonly: data.companyPresetValue.isLocked,
      value: data.companyPresetValue.value,
    });
  }

  const createOrderModel: CreateOrderModel = {
    countryCode: data.countryCode,
    currency: data.currency,
    locale: data.locale,
    clientOrderNumber: data.clientOrderNumber,
    merchantSettings: {
      ...data.merchantSettings!,
      ...{
        checkoutUri: "https://localhost:3000",
        confirmationUri: "https://localhost:3000",
        pushUri: "https://localhost:3000",
        termsUri: "https://localhost:3000",
      },
    },
    cart: cartOptions.find((x) => x.id === data.cartPreset)?.cart ?? {
      items: [],
    },
    presetValues: checkoutPresetValue,
    identityFlags: data.identityFlags,
    requireElectronicIdAuthentication: data.requireElectronicAuthentication,
    partnerKey: data.partnerKey,
    merchantData: data.merchantData,
    shippingInformation: {
      enableShipping: data.shippingInformation.enableShipping,
      weight: data.shippingInformation.weight,
      enforceFallback: data.shippingInformation.enforceFallback,
    },
    validation: {
      minAge: data.validation.minAge,
      paymentFailureResiliency: data.validation.paymentFailureResiliency,
    },
    recurring: data.recurring,
    validationCallback: data.validationCallback,
    checkoutPushData: data.checkoutPushData ?? {
      responseDelay: 0,
      responseStatusCode: 200,
    },
    merchantId: data.merchantId,
  };
  return createOrderModel;
};
