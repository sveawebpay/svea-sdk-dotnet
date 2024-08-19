import React, { useEffect, useState } from "react";
import {
  Box,
  Checkbox,
  Chip,
  FormControl,
  Grid,
  InputLabel,
  ListItemText,
  MenuItem,
  OutlinedInput,
  Select,
  Typography,
} from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";
import { PartPayment } from "../../../../shared/models/checkout/CreateOrderModel";

interface ActivePartPaymentCampaignProps {
  isCompany: boolean;
}

const ActivePartPaymentCampaign: React.FC<ActivePartPaymentCampaignProps> = ({
  isCompany,
}) => {
  const [options, setOptions] = useState<PartPayment[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const { control } = useFormContext();

  useEffect(() => {
    const fetchPaymentOptions = async () => {};

    void fetchPaymentOptions();
  }, []);

  if (error) {
    return (
      <Typography color="error">
        No Active part payment campaign available for merchant
      </Typography>
    );
  }

  return (
    <Grid container alignItems="center">
      <Grid item xs={6}>
        <InputLabel>Active part payment campaigns</InputLabel>
      </Grid>
      <Grid item xs={6}>
        <Controller
          control={control}
          name="merchantSettings.activePartPaymentCampaigns"
          render={({ field }) => (
            <FormControl fullWidth>
              <Select
                multiple
                {...field}
                input={<OutlinedInput id="select-multiple-checkbox" />}
                renderValue={(selected: string[]) => (
                  <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                    {selected.map((value) => (
                      <Chip
                        key={value}
                        label={
                          options.find(
                            (option) => option.campaignCode === value
                          )?.description || value
                        }
                      />
                    ))}
                  </Box>
                )}
              >
                {options.map((option) => (
                  <MenuItem
                    key={option.campaignCode}
                    value={option.campaignCode}
                  >
                    <Checkbox
                      checked={field.value.includes(option.campaignCode)}
                    />
                    <ListItemText primary={option.description} />
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          )}
        />
      </Grid>
    </Grid>
  );
};

export default ActivePartPaymentCampaign;
