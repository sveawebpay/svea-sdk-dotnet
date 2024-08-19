import React from "react";
import SelectOption from "../../../../components/form-components/SelectOption";
import { Grid } from "@mui/material";
import countries from "../../../../shared/constants/countries.constants";

const InternationalMarketSelection: React.FC = () => {
  return (
    <Grid container alignItems="center" spacing={1}>
      <Grid item xl={6} xs={6} md={6}>
        <p>Selected market for the current merchant</p>
      </Grid>
      <Grid item xl={6} xs={6} md={6} sx={{ width: "100%", marginTop: "2px" }}>
        <SelectOption
          id="market"
          options={countries.map((x) => ({
            value: x.isoCode,
            label: `${x.isoCode}-${x.name}`,
          }))}
          selectFormPath="countryCode"
          width="100%"
        />
      </Grid>
    </Grid>
  );
};

export default InternationalMarketSelection;
