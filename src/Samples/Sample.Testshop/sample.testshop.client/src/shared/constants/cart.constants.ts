import { RequestCart } from "../models/checkout/CreateOrderModel";

export const defaultCart: RequestCart = {
  items: [
    {
      articleNumber: "1234",
      name: "Article 1",
      quantity: 100,
      unitPrice: 10000,
      discountPercent: 0,
      vatPercent: 0,
      unit: "st",
      rowNumber: 1,
      discountValue: 0,
      shippingInfo: "",
    },
    {
      articleNumber: "1235",
      name: "Article 2",
      quantity: 200,
      unitPrice: 20000,
      discountPercent: 0,
      vatPercent: 0,
      unit: "st",
      rowNumber: 2,
      discountValue: 0,
      shippingInfo: "",
    },
  ],
};

export const leasingCart: RequestCart = {
  items: [
    {
      articleNumber: "L-1001",
      name: "Leasing Item 1",
      quantity: 100,
      unitPrice: 6000000,
      discountPercent: 0,
      vatPercent: 2500,
      unit: "st",
      rowNumber: 1,
      discountValue: 0,
      shippingInfo: "",
    },
  ],
};
export const zeroSumCart: RequestCart = {
  items: [
    {
      articleNumber: "Z-1001",
      name: "Item",
      quantity: 1,
      unitPrice: 1,
      discountPercent: 0,
      vatPercent: 0,
      unit: "st",
      rowNumber: 1,
      discountValue: 0,
      shippingInfo: "",
    },
    {
      articleNumber: "Z-1002",
      name: "Presentkort",
      quantity: 1,
      unitPrice: -1,
      discountPercent: 0,
      vatPercent: 0,
      unit: "st",
      rowNumber: 2,
      discountValue: 0,
      shippingInfo: "",
    },
  ],
};

export interface CartOption {
  label: string;
  id: number;
  cart: RequestCart;
}
export const cartOptions = [
  { label: "Default Cart", id: 1, cart: defaultCart },
  { label: "Leasing Cart", id: 2, cart: leasingCart },
  { label: "ZeroSum Cart", id: 3, cart: zeroSumCart },
];
