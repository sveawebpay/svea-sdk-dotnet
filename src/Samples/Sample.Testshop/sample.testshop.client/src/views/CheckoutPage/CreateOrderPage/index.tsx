import { FormProvider, useForm } from "react-hook-form";
import { Box, Button, Container, Grid, Paper, Typography } from "@mui/material";
import {
  CreateOrderForm,
  FormDataToCreateOrderModel,
} from "./forms/CreateOrderForm";
import BasicPresets from "./Components/BasicPresets";
import ValidationFields from "./Components/ValidationFields";
import EnableShipping from "./Components/EnableShipping";
import MiscSettings from "./Components/MiscSettings";
import PaymentOptionsPresets from "./Components/PaymentOptionsPresets";
import ValidationCallback from "./Components/ValidationCallback";
import { CheckoutService } from "../../../services/checkout.service";
import MerchantSelector from "./Components/MerchantSelector";
import { useEffect, useState } from "react";
import { GetMerchantsResponse } from "../../../shared/models/merchant/Merchant";
import { MerchantService } from "../../../services/merchant.service";
import CartPresetSelector from "./Components/CartPresetSelector";
import { Cookies } from "react-cookie";
import { useNavigate } from "react-router-dom";

const CreateOrderPage: React.FC = () => {
  const [merchantOptions, setMerchantOptions] = useState<
    GetMerchantsResponse[]
  >([]);

  useEffect(() => {
    const loadMerchants = async () => {
      const response = await MerchantService.getMerchants();
      console.log(response, response.data);
      if (response.status == 200) {
        setMerchantOptions(response?.data ?? []);
      }
    };
    void loadMerchants();
  }, [setMerchantOptions]);

  const methods = useForm<CreateOrderForm>({
    defaultValues: {
      emailPresetValue: { isLocked: false, value: "" },
      companyPresetValue: { isLocked: false, value: "" },
      postalCodePresetValue: { isLocked: false, value: "" },
      nationalIdPresetValue: { isLocked: false, value: "" },
      phoneNumberPresetValue: { isLocked: false, value: "" },
      identityFlags: {
        hideAnonymous: false,
        hideChangeAddress: false,
        hideNotYou: false,
      },
      requireElectronicAuthentication: false,
      recurring: false,
      reloadOrderManually: false,
      validationCallback: {
        useValidationCallback: false,
        validate: false,
        errorMessage: "",
      },
      validation: { minAge: undefined, paymentFailureResiliency: false },
      merchantData: "",
      partnerKey: undefined,
      checkoutPushData: {
        responseDelay: 0,
        responseStatusCode: undefined,
      },
      shippingInformation: {
        enableShipping: false,
        weight: undefined,
      },
      clientOrderNumber: "",
      merchantSettings: {
        activePartPaymentCampaigns: [],
        promotedPartPaymentCampaign: 0,
        checkoutUri: "https://localhost:3000",
        confirmationUri: "https://localhost:3000",
        pushUri: "https://localhost:3000",
        termsUri: "https://localhost:3000",
      },
      locale: "sv-SE",
      currency: "",
      countryCode: "",
      merchantId: 0,
    },
  });

  const navigate = useNavigate();

  const merchantIdWatch = methods.watch("merchantId");

  useEffect(() => {
    console.log(merchantIdWatch);
    methods.setValue(
      "countryCode",
      merchantOptions.find((x) => x.merchantId === merchantIdWatch)?.market ??
        ""
    );
  }, [merchantIdWatch, methods, merchantOptions]);

  const onSubmit = async (formData: CreateOrderForm) => {
    console.log(formData);
    const request = FormDataToCreateOrderModel(formData);
    console.log(request);
    const response = await CheckoutService.createOrder(
      request,
      formData.merchantId
    );
    const cookies = new Cookies();
    cookies.set(response.data.orderId.toString(), formData.merchantId);
    navigate({
      pathname: `/checkout/display-order/${response.data.orderId.toString()}`,
    });
  };
  return (
    <FormProvider {...methods}>
      <form onSubmit={methods.handleSubmit(onSubmit)}>
        <MerchantSelector merchants={merchantOptions} />
        <Typography variant="h5">Preset</Typography>
        <Paper sx={{ padding: 0 }}>
          <Grid container>
            <Grid
              item
              xl={4}
              xs={12}
              md={4}
              sx={{ borderRight: "2px solid #f4f4ee" }}
            >
              <BasicPresets />
            </Grid>

            <Grid
              item
              xl={4}
              xs={12}
              md={4}
              sx={{ borderRight: "2px solid #f4f4ee" }}
            >
              <ValidationCallback />
              <ValidationFields />
              <EnableShipping />
              <MiscSettings />
            </Grid>

            <Grid item xl={4} xs={12} md={4}>
              <PaymentOptionsPresets />
              <CartPresetSelector />
            </Grid>
          </Grid>
        </Paper>
        <Box sx={{ display: "flex", justifyContent: "flex-end", marginTop: 2 }}>
          <Button
            disableElevation
            variant="contained"
            color="primary"
            type="submit"
          >
            Create Order
          </Button>
        </Box>
      </form>
    </FormProvider>
  );
};

export default CreateOrderPage;
