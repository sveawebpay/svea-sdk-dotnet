import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import { CheckoutService } from "../../../services/checkout.service";
import { Cookies } from "react-cookie";
import { OrderResponse } from "../../../shared/models/checkout/OrderResponse";
import { Box, Card, CardContent, Container, Grid } from "@mui/material";
import OrderDetails from "./components/OrderDetails";
import CartEditor from "../components/CartEditor";

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
      <Grid
        container
        spacing={2}
        style={{ flexGrow: 1 }}
        direction={"row"}
        columns={4}
      >
        <Grid item sm={3}>
          <Card sx={{ marginBottom: 0 }}>
            <CardContent>
              <Grid container spacing={1} direction={"column"}>
                <Grid item>
                  <OrderDetails orderDetails={orderData} />
                </Grid>
                <Grid item>
                  {orderData && (
                    <CartEditor initialItems={orderData?.cart.items} />
                  )}
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item style={{ flex: 1 }} sm={1}>
          <Box style={{ flexGrow: 1 }} ref={divRef}></Box>
        </Grid>
      </Grid>
    </Container>
  );
};

export default DisplayOrderPage;
