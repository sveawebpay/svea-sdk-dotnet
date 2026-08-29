import CreateOrderModel from "../shared/models/checkout/CreateOrderModel";
import { OrderResponse } from "../shared/models/checkout/OrderResponse";
import axios, { AxiosResponse } from "axios";
import UpdateOrderModel from "../shared/models/checkout/UpdateOrderModel";
import { Data, OrderData, OrderRow, RecurringToken } from "../shared/models/checkout/shared";
import {
  ChangePaymentMethodRequest,
  ChangePaymentMethodResponse,
  CreateRecurringOrderRequest,
} from "../shared/models/checkout/RecurringOrder";
export const CheckoutService = {
  createOrder: async (
    data: CreateOrderModel,
    merchantId: number
  ): Promise<AxiosResponse<OrderResponse>> => {
    return axios.post<OrderResponse>("/api/orders", data, {
      headers: {
        "Content-Type": "application/json",
        merchantId: `${merchantId}`,
      },
    });
  },
  updateOrder: async (
    data: UpdateOrderModel,
    orderId: number,
    merchantId: number
  ): Promise<AxiosResponse<OrderResponse>> => {
    return axios.patch<OrderResponse>(`/api/orders/${orderId}`, data, {
      headers: {
        "Content-Type": "application/json",
        merchantId: `${merchantId}`,
      },
    });
  },

  getOrder: async (orderId: number, merchantId: number): Promise<Data> => {
    const response = await axios.get<OrderResponse>(`/api/orders/${orderId}`, {
      headers: {
        merchantId: `${merchantId}`,
      },
    });

    const orderResponse: Data = {
      ...response.data,
      cart: {
        items: response.data.cart.items.map<OrderRow>((x) => ({
          ...x,
          discountAmount: x.discountAmount?.inLowestMonetaryUnit,
          discountPercent: x.discountPercent?.inLowestMonetaryUnit,
          discountValue: x.discountValue?.inLowestMonetaryUnit,
          vatPercent: x.vatPercent.inLowestMonetaryUnit,
          quantity: x.quantity?.inLowestMonetaryUnit,
          unitPrice: x.unitPrice.inLowestMonetaryUnit,
        })),
      },
    };
    return orderResponse;
  },

  recurring: {
    createRecurringOrder: async (
      body: CreateRecurringOrderRequest,
      token: string,
      merchantId: number
    ): Promise<AxiosResponse<OrderData>> => {
      const response = await axios.post<OrderData>(
        `/api/recurring/${token}/orders`,
        body,
        {
          headers: {
            "Content-Type": "application/json",
            merchantId: `${merchantId}`,
          },
        }
      );
      return response;
    },
    changePaymentMethods: async (
      body: ChangePaymentMethodRequest,
      token: string,
      merchantId: number
    ) => {
      const response = await axios.post<ChangePaymentMethodResponse>(
        `/api/recurring/${token}/payment-method`,
        body,
        {
          headers: {
            "Content-Type": "application/json",
            merchantId: `${merchantId}`,
          },
        }
      );
      return response;
    },
    getRecurringOrder: async (
      token: string,
      orderId: number,
      merchantId: number
    ) => {
      const response = await axios.get<OrderData>(
        `/api/recurring/${token}/orders/${orderId}`,
        {
          headers: {
            "Content-Type": "application/json",
            merchantId: `${merchantId}`,
          },
        }
      );
      return response;
    },
    getRecurringToken: async (token: string, merchantId: number) => {
      const response = await axios.get<RecurringToken>(
        `/api/recurring/${token}`,
        {
          headers: {
            "Content-Type": "application/json",
            merchantId: `${merchantId}`,
          },
        }
      );
      return response;
    },
  },
};
