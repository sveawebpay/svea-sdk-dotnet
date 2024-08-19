import React from "react";
import { Grid } from "@mui/material";
import SelectOption from "../../../../components/form-components/SelectOption";
import { GetMerchantsResponse } from "../../../../shared/models/merchant/Merchant";

interface MerchantSelectorProps {
  merchants: GetMerchantsResponse[];
}

const MerchantSelector: React.FC<MerchantSelectorProps> = ({ merchants }) => {
  return (
    <Grid alignItems="left" spacing={1}>
      <Grid item sx={{ width: "180px" }}>
        <SelectOption
          id="merchant-select"
          label="Merchant"
          options={merchants.map((x) => ({
            value: x.merchantId,
            label: x.market,
          }))}
          selectFormPath="merchantId"
          width="100%"
        />
      </Grid>
    </Grid>
  );
};

export default MerchantSelector;
