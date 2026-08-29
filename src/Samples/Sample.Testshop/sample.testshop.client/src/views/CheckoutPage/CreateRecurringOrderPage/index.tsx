import { useEffect, useState } from "react";
import { useForm, Controller } from "react-hook-form";
import {
  TextField,
  Button,
  Grid,
  Box,
  Typography,
  Divider,
  Select,
  MenuItem,
  Card,
} from "@mui/material";
import CartEditor from "../components/CartEditor";
import { v4 as uuidv4 } from "uuid";
import { CreateRecurringOrderRequest } from "../../../shared/models/checkout/RecurringOrder";
import { CheckoutService } from "../../../services/checkout.service";
import { OrderData } from "../../../shared/models/checkout/shared";
import { KeyOutlined, StoreOutlined } from "@mui/icons-material";
import { GetMerchantsResponse } from "../../../shared/models/merchant/Merchant";
import { MerchantService } from "../../../services/merchant.service";
import { defaultCart } from "../../../shared/constants/cart.constants";

const CreateRecurringOrderPage: React.FC = () => {
  const { control, handleSubmit } = useForm<CreateRecurringOrderRequest>({
    defaultValues: {
      currency: "SEK",
      clientOrderNumber: uuidv4().replaceAll("-", ""),
      merchantSettings: {
        pushUri: "",
        checkoutValidationCallBackUri: "",
      },
      cart: {
        items: defaultCart.items, // Cart items from CartEditor
      },
    },
  });
  const [token, setToken] = useState<string>("");
  const [orderResponse, setOrderResponse] = useState<OrderData>();
  const [merchants, setMerchants] = useState<GetMerchantsResponse[]>([]);
  const [merchantId, setMerchantId] = useState<number>();
  useEffect(() => {
    const loadMerchants = async () => {
      const response = await MerchantService.getMerchants();
      console.log(response, response.data);
      if (response.status == 200) {
        setMerchants(response?.data ?? []);
      }
    };
    void loadMerchants();
  }, [setMerchants]);

  const onSubmit = async (data: CreateRecurringOrderRequest) => {
    try {
      const response = await CheckoutService.recurring.createRecurringOrder(
        data,
        token,
        merchantId!
      );
      setOrderResponse(response.data);
      alert("Order submitted successfully");
    } catch (error) {
      console.error("Error submitting order:", error);
    }
  };

  return (
    <>
      <Grid container spacing={2} direction={"row"} sx={{ margin: 1 }}>
        <Grid item xl={6}>
          <Card sx={{ padding: 1 }}>
            <Grid container spacing={2} direction={"row"} sx={{ margin: 1 }}>
              <Grid item>
                <Typography variant="h6">
                  <KeyOutlined fontSize="medium" sx={{ marginRight: -1 }} />
                  Recurring Token
                </Typography>
                <TextField
                  value={token}
                  onChange={(e) => {
                    setToken(e.target.value);
                  }}
                />
              </Grid>
              <Grid item>
                <Typography variant="h6" display="flex" alignItems="center">
                  <StoreOutlined fontSize="medium" sx={{ marginRight: -1 }} />
                  Select a merchant
                </Typography>
                <Box>
                  {merchants.length == 0 ? (
                    <Box>
                      <Typography variant="caption">
                        Merchant is loading...
                      </Typography>
                    </Box>
                  ) : (
                    <Box sx={{ width: "100%" }}>
                      <Select
                        onChange={(e) =>
                          setMerchantId(e.target.value as number)
                        }
                        sx={{ width: "100%" }}
                      >
                        {merchants.map((x) => {
                          return (
                            <MenuItem value={x.merchantId}>
                              {x.market}-{x.merchantId}{" "}
                            </MenuItem>
                          );
                        })}
                      </Select>
                    </Box>
                  )}
                </Box>
              </Grid>
              <Divider />
            </Grid>
            <Box
              component="form"
              onSubmit={handleSubmit(onSubmit)}
              sx={{ marginTop: 1 }}
            >
              <Grid container spacing={2}>
                <Grid item xs={12}>
                  <Controller
                    name="currency"
                    control={control}
                    render={({ field }) => (
                      <TextField {...field} label="Currency" fullWidth />
                    )}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Controller
                    name="clientOrderNumber"
                    control={control}
                    render={({ field }) => (
                      <TextField
                        {...field}
                        label="Client Order Number"
                        fullWidth
                      />
                    )}
                  />
                </Grid>
                <Grid item xs={12}>
                  <CartEditor initialItems={defaultCart.items} />
                </Grid>
                <Grid item xs={12}>
                  <Controller
                    name="merchantSettings.pushUri"
                    control={control}
                    render={({ field }) => (
                      <TextField {...field} label="Push URI" fullWidth />
                    )}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Controller
                    name="merchantSettings.checkoutValidationCallBackUri"
                    control={control}
                    render={({ field }) => (
                      <TextField
                        {...field}
                        label="Validation Callback URI"
                        fullWidth
                      />
                    )}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Button
                    variant="contained"
                    color="primary"
                    type="submit"
                    disabled={!merchantId || (merchants?.length == 0)}
                  >
                    Submit Recurring Order
                  </Button>
                </Grid>
              </Grid>
            </Box>
          </Card>
        </Grid>
        <Grid item xs={6}>
          <Card>
            <Box p={2} border={1} borderRadius={1} borderColor="grey.400">
              <Typography variant="h6">Order Response</Typography>
              {orderResponse ? (
                <div>
                  <Typography>
                    <strong>Order ID:</strong> {orderResponse.orderId}
                  </Typography>
                  <Typography>
                    <strong>Client Order Number:</strong>{" "}
                    {orderResponse.clientOrderNumber}
                  </Typography>
                  <Typography>
                    <strong>Status:</strong> {orderResponse.status}
                  </Typography>
                  <Typography>
                    <strong>Currency:</strong> {orderResponse.currency}
                  </Typography>
                  <Typography>
                    <strong>Locale:</strong> {orderResponse.locale}
                  </Typography>
                  <Typography>
                    <strong>Country Code:</strong> {orderResponse.countryCode}
                  </Typography>
                  <Typography>
                    <strong>Email:</strong> {orderResponse.emailAddress}
                  </Typography>
                  <Typography>
                    <strong>Phone:</strong> {orderResponse.phoneNumber}
                  </Typography>
                  <Typography variant="h6" mt={2}>
                    Cart Items:
                  </Typography>
                  {orderResponse.cart.items.map((item, index) => (
                    <div key={index}>
                      <Typography>
                        <strong>Item {index + 1}</strong>
                      </Typography>
                      <Typography>
                        Article Number: {item.articleNumber}
                      </Typography>
                      <Typography>Name: {item.name}</Typography>
                      <Typography>Quantity: {item.quantity}</Typography>
                      <Typography>Unit Price: {item.unitPrice}</Typography>
                    </div>
                  ))}
                </div>
              ) : (
                <Typography>No order response yet.</Typography>
              )}
            </Box>
          </Card>
        </Grid>
      </Grid>
    </>
  );
};

export default CreateRecurringOrderPage;
