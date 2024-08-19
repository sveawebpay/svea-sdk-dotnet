import React from "react";
import { Box } from "@mui/material";
import SelectOption from "../../../../components/form-components/SelectOption";

const CartType = () => {
  const options = [
    { value: "default", label: "Default cart" },
    { value: "zeroAmount", label: "Zero amount" },
    { value: "leasing", label: "Leasing" },
  ];

  return (
    <Box>
      <SelectOption
        id="cartType"
        width="180px"
        label="Cart type"
        options={options}
        selectFormPath="cartType"
      />
    </Box>
  );
};

export default CartType;
