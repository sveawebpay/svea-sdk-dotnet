import React from "react";
import {
  Box,
  FormControlLabel,
  Grid,
  Switch,
  ToggleButton,
  ToggleButtonGroup,
} from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";

const IsCompanySwitch: React.FC = () => {
  const switchId = `isCompany-switch`;
  const textFieldId = `isCompant-textField`;
  const { control } = useFormContext();
  return (
    <Box>
      <Grid container spacing={1} alignItems="center">
        <Grid item xs={6}>
          <Controller
            control={control}
            name={"companyPresetValue.isLocked"}
            render={({ field }) => (
              <FormControlLabel
                control={<Switch id={switchId} {...field} />}
                label={"Is company"}
              />
            )}
          />
        </Grid>
        <Grid item xs={6}>
          <Controller
            control={control}
            name={"companyPresetValue.value"}
            render={({ field }) => (
              <ToggleButtonGroup
                size="small"
                id={textFieldId}
                color="primary"
                exclusive
                aria-label="Platform"
                {...field}
              >
                <ToggleButton id="" value="false">
                  B2C
                </ToggleButton>
                <ToggleButton value={""}>None</ToggleButton>
                <ToggleButton value="true">B2B</ToggleButton>
              </ToggleButtonGroup>
            )}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default IsCompanySwitch;
