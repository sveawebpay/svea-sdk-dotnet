import React, { useEffect, useState } from "react";
import { Grid, InputLabel, Typography } from "@mui/material";
import SelectOption from "../../../../components/form-components/SelectOption";

interface PromotedPaymentCampaignProps {
  isCompany: boolean;
}

const PromotedPartPaymentCampaign: React.FC<PromotedPaymentCampaignProps> = ({
  isCompany,
}) => {
  const [options, setOptions] = useState<PartPayment[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchPaymentOptions = async () => {};

    fetchPaymentOptions();
  }, []);

  if (error) {
    return (
      <Typography color="error">
        No Promoted part payment available for merchant
      </Typography>
    );
  }

  return (
    <>
      <Grid container display="flex" alignItems="center">
        <Grid item xs={6}>
          <InputLabel>Promoted part payment campaigns</InputLabel>
        </Grid>
        <Grid item xs={6}>
          <SelectOption
            id="promoted-part-payment-input"
            width="100%"
            options={options.map((option) => ({
              value: option.campaignCode.toString(),
              label: option.description,
            }))}
            selectFormPath="merchantSettings.promotedPartPaymentCampaign"
          />
        </Grid>
      </Grid>
    </>
  );
};

export default PromotedPartPaymentCampaign;
