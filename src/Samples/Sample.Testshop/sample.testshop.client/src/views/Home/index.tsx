import { Button } from "@mui/material";
import React from "react";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const navigate = useNavigate();
  return (
    <>
      <h1>Home page template</h1>
      <Button
        onClick={() => {
          navigate({ pathname: "/checkout" });
        }}
      >
        Create an order
      </Button>
    </>
  );
};

export default Home;
