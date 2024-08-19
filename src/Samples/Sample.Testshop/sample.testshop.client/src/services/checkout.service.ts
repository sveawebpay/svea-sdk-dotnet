import CreateOrderModel from "../shared/models/checkout/CreateOrderModel";
import { OrderResponse } from "../shared/models/checkout/OrderResponse";
import axios, { AxiosResponse } from "axios";
import UpdateOrderModel from "../shared/models/checkout/UpdateOrderModel";
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

  getOrder: async (
    orderId: number,
    merchantId: number
  ): Promise<AxiosResponse<OrderResponse>> => {
    return axios.get(`/api/orders/${orderId}`, {
      headers: {
        merchantId: `${merchantId}`,
      },
    });
  },
};
