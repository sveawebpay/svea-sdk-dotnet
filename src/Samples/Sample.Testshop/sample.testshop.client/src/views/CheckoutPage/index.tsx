import { Button } from "@mui/material";
import React from "react";
import { useNavigate } from "react-router-dom";

const CheckoutPage = () => {
  const navigate = useNavigate();
  return (
    <>
      <h1>Checkout Page Template</h1>
      <Button
        onClick={() => {
          navigate({ pathname: "/checkout/create-order" });
        }}
      >
        Create an order
      </Button>
    </>
  );
};

export default CheckoutPage;
