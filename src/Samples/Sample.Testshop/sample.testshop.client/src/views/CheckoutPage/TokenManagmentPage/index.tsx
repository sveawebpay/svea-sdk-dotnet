import { useState } from "react";
import { TextField, Button, Grid, Box, Typography, Card } from "@mui/material";
import axios from "axios";
import {
  OrderData,
  RecurringToken,
} from "../../../shared/models/checkout/shared";
import { CheckoutService } from "../../../services/checkout.service";
import { ChangePaymentMethodResponse } from "../../../shared/models/checkout/RecurringOrder";

const TokenManagementPage: React.FC = () => {
  const [recurringToken, setRecurringToken] = useState("");
  const [orderId, setOrderId] = useState("");
  const [tokenDetails, setTokenDetails] = useState<RecurringToken>();
  const [orderDetails, setOrderDetails] = useState<OrderData>();
  const [paymentMethodChangeResponse, setPaymentMethodChangeResponse] =
    useState<ChangePaymentMethodResponse>();
  const merchantId = 100001;
  // Get Recurring Token Details
  const handleGetTokenDetails = async () => {
    try {
      const response = await CheckoutService.recurring.getRecurringToken(
        recurringToken,
        merchantId
      );
      setTokenDetails(response.data);
    } catch (error) {
      console.error("Error fetching token details:", error);
    }
  };

  // Get Recurring Order Details
  const handleGetRecurringOrder = async () => {
    try {
      const response = await CheckoutService.recurring.getRecurringOrder(
        recurringToken,
        Number.parseInt(orderId),
        merchantId
      );
      setOrderDetails(response.data);
    } catch (error) {
      console.error("Error fetching recurring order:", error);
    }
  };

  // Change Payment Method
  const handleChangePaymentMethod = async () => {
    try {
      const response = await CheckoutService.recurring.changePaymentMethods(
        { termsUrl: "" },
        recurringToken,
        merchantId
      );
      setPaymentMethodChangeResponse(response.data);
    } catch (error) {
      console.error("Error changing payment method:", error);
    }
  };

  return (
    <Box p={3}>
      <Typography variant="h4">Test Recurring Token Operations</Typography>

      <Grid container spacing={2}>
        {/* Input for Recurring Token */}
        <Grid item xs={12}>
          <Card sx={{ padding: 1 }}>
            <TextField
              label="Recurring Token"
              value={recurringToken}
              onChange={(e) => setRecurringToken(e.target.value)}
              fullWidth
            />
          </Card>
        </Grid>

        {/* Input for Order ID */}
        <Grid item xs={12}>
          <Card sx={{ padding: 1 }}>
            <TextField
              label="Order ID (for getting recurring order)"
              value={orderId}
              onChange={(e) => setOrderId(e.target.value)}
              fullWidth
            />
          </Card>
        </Grid>
        {/* Buttons for Token Operations */}
        <Grid item xs={12}>
          <Card sx={{ padding: 5 }}>
            <Button
              variant="contained"
              color="primary"
              onClick={handleGetTokenDetails}
              sx={{ mr: 2 }}
            >
              Get Recurring Token Details
            </Button>
            <Button
              variant="contained"
              color="secondary"
              onClick={handleGetRecurringOrder}
              sx={{ mr: 2 }}
            >
              Get Recurring Order Details
            </Button>
            <Button
              variant="contained"
              color="success"
              onClick={handleChangePaymentMethod}
            >
              Change Payment Method
            </Button>
          </Card>
        </Grid>

        {/* Display Recurring Token Details */}
        {tokenDetails && (
          <Grid item xs={12}>
            <Card sx={{ padding: 5 }}>
              <Box mt={3}>
                <Typography variant="h6">Recurring Token Details</Typography>
                <Typography variant="body1">
                  Token: {tokenDetails.token}
                </Typography>
                <Typography variant="body1">
                  Status: {tokenDetails.status}
                </Typography>
                <Typography variant="body1">
                  Currency: {tokenDetails.currency}
                </Typography>
                <Typography variant="body1">
                  Payment Method: {tokenDetails.paymentMethod}
                </Typography>
                <Typography variant="body1">
                  Payment expiry at: Month:{" "}
                  {tokenDetails.paymentMethodDetails?.expiryMonth}; Year:{" "}
                  {tokenDetails.paymentMethodDetails?.expiryYear}
                </Typography>
                {/* Add more token fields as necessary */}
              </Box>
            </Card>
          </Grid>
        )}

        {/* Display Recurring Order Details */}
        {orderDetails && (
          <Grid item xs={12}>
            <Card sx={{ padding: 5 }}>
              <Box mt={3}>
                <Typography variant="h6">Recurring Order Details</Typography>
                <Typography variant="body1">
                  Order ID: {orderDetails.orderId}
                </Typography>
                <Typography variant="body1">
                  Status: {orderDetails.status}
                </Typography>
                <Typography variant="body1">
                  Currency: {orderDetails.currency}
                </Typography>
                {/* Add more order fields as necessary */}
              </Box>
            </Card>
          </Grid>
        )}

        {/* Display Change Payment Method Response */}
        {paymentMethodChangeResponse && (
          <Grid item xs={12}>
            <Card>
              <Box mt={3}>
                <Typography variant="h6">
                  Payment Method Change Response
                </Typography>
                <Typography variant="body1">
                  Response: {paymentMethodChangeResponse}
                </Typography>
                {/* Add more response fields as necessary */}
              </Box>
            </Card>
          </Grid>
        )}
      </Grid>
    </Box>
  );
};

export default TokenManagementPage;
