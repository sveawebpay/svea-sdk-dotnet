import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import { CheckoutService } from "../../../services/checkout.service";
import { Cookies } from "react-cookie";
import { OrderResponse } from "../../../shared/models/checkout/OrderResponse";
import { Box, Card, CardContent, Container, Grid } from "@mui/material";
import OrderDetails from "./components/OrderDetails";

const DisplayOrderPage: React.FC = () => {
  const [orderData, setOrderData] = useState<OrderResponse | null>(null);
  const { id: orderId } = useParams();
  const divRef = useRef<HTMLDivElement>(null);
  const [checkoutSnippet, setCheckoutOrderSnippet] = useState<string>("");
  useEffect(() => {
    if (!orderId) {
      // Log something
      return;
    }
    const cookies = new Cookies();
    const merchantId = cookies.get<number>(orderId);
    const fetchOrder = async () => {
      const response = await CheckoutService.getOrder(
        Number.parseInt(orderId!),
        merchantId
      );
      setOrderData(response.data);
    };
    void fetchOrder();
  }, [orderId]);

  useEffect(() => {
    if (orderData) {
      setCheckoutOrderSnippet(orderData.gui.snippet);
    }
  }, [orderData, setCheckoutOrderSnippet]);

  useEffect(() => {
    console.log("HELP");
    const fragment = document
      .createRange()
      .createContextualFragment(checkoutSnippet);
    divRef.current?.replaceChildren(fragment);
  }, [checkoutSnippet]);

  if (!orderData) {
    return <>Loading checkout order</>;
  }

  return (
    <Container
      style={{
        flexGrow: 1,
        maxWidth: "100%",
        position: "relative",
      }}
    >
      <Grid container spacing={2} style={{ flexGrow: 1 }} direction={"row"}>
        <Grid item>
          <Card sx={{ marginBottom: 0 }}>
            <CardContent>
              <Grid container spacing={2}>
                <Grid item>
                  <OrderDetails orderDetails={orderData} />
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item style={{ flex: 1 }}>
          <Box style={{ flexGrow: 1 }} ref={divRef}></Box>
        </Grid>
      </Grid>
    </Container>
  );
};

export default DisplayOrderPage;
