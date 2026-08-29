import React from "react";
import { Box, Divider, Typography } from "@mui/material";
import SelectOption from "../../../../components/form-components/SelectOption";
import { GetMerchantsResponse } from "../../../../shared/models/merchant/Merchant";
import { StoreOutlined } from "@mui/icons-material";

interface MerchantSelectorProps {
  merchants: GetMerchantsResponse[];
}

const MerchantSelector: React.FC<MerchantSelectorProps> = ({ merchants }) => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <StoreOutlined fontSize="medium" sx={{ marginRight: -1 }} />
        Select a merchant
      </Typography>
      <Box p={1}>
        <SelectOption
          id="merchant-select"
          options={merchants.map((x) => ({
            value: x.merchantId,
            label: x.market,
          }))}
          selectFormPath="merchantId"
          width="50%"
        />
      </Box>
      <Divider />
    </>
  );
};

export default MerchantSelector;
