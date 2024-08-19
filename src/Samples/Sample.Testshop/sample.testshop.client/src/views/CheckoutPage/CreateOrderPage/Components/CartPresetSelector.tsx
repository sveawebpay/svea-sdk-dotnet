import React from "react";
import { Divider, Grid, Typography } from "@mui/material";
import SelectOption from "../../../../components/form-components/SelectOption";
import { cartOptions } from "../../../../shared/constants/cart.constants";
import ShoppingCartOutlinedIcon from '@mui/icons-material/ShoppingCartOutlined';

const CartPresetSelector: React.FC = () => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <ShoppingCartOutlinedIcon fontSize="small" sx={{ marginRight: 1 }} />
        Cart Preset
      </Typography>
      <Divider />
      <Grid item sx={{ padding: 2, display: "flex", alignItems: "end" }}>
        <SelectOption
          id="cart-preset-selector"
          width="180px"
          options={cartOptions.map((x) => ({ label: x.label, value: x.id }))}
          selectFormPath="cartPreset"
        />
      </Grid>
    </>
  );
};

export default CartPresetSelector;
