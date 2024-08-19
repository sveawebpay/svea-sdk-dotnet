import { useEffect, useState } from "react";
import { OrderResponse } from "../../../../shared/models/checkout/OrderResponse";
import { Cookies } from "react-cookie";
import { Divider, Typography } from "@mui/material";
import InfoIcon from "@mui/icons-material/Info";

interface OrderDetailsProps {
  orderDetails: OrderResponse;
}
const OrderDetails: React.FC<OrderDetailsProps> = (props) => {
  const [error, setError] = useState<string | null>(null);
  const { orderId, status, locale, currency, countryCode, clientOrderNumber } =
    props.orderDetails;
  useEffect(() => {
    const fetchCountryIdentificationData = async () => {
      try {
        setError(null);
        const cookies = new Cookies();
        const merchantId = cookies.get(orderId.toString());
        if (!merchantId) {
          throw new Error("No active merchant selected.");
        }
      } catch (error: unknown) {
        if (error instanceof Error) {
          setError(error.message);
        } else {
          setError("Unknown error occurred!");
        }
      }
    };
    fetchCountryIdentificationData();
  });
  useEffect(() => {
    console.log(error);
  }, [error]);

  if (error) {
    <Typography variant="h6" sx={{ color: "Red" }}>
      Error has occurred, please check the console: {error}
    </Typography>;
  }
  return (
    <>
      <Typography variant="h6" sx={{ paddingLeft: 0 }}>
        <InfoIcon />
        Order Information
      </Typography>
      <Divider sx={{ marginBottom: "10px" }} />
      <Typography variant="body1">
        <strong>Order ID:</strong> {orderId}
      </Typography>
      <Typography variant="body1">
        <strong>Status:</strong> {status}
      </Typography>
      <Typography variant="body1">
        <strong>Locale:</strong> {locale}
      </Typography>
      <Typography variant="body1">
        <strong>Currency:</strong> {currency}
      </Typography>
      <Typography variant="body1">
        <strong>Market:</strong> {countryCode}
      </Typography>
      <Typography variant="body1">
        <strong>Client Order Number:</strong> {clientOrderNumber}
      </Typography>
    </>
  );
};

export default OrderDetails;
