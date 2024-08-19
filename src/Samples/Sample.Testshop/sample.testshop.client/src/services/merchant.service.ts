import axios, { AxiosResponse } from "axios";
import { GetMerchantsResponse } from "../shared/models/merchant/Merchant";

export const MerchantService = {
  getMerchants: async (): Promise<AxiosResponse<GetMerchantsResponse[]>> => {
    return axios.get<GetMerchantsResponse[]>("/api/utils/merchants");
  },
};
