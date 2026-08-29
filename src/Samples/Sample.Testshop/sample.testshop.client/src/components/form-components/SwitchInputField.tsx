import React, { FC } from "react";
import { FormControlLabel, Switch, TextField, Grid } from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";

interface SwitchInputFieldProps {
  label: string;
  width?: string | number;
  switchFormPath: string;
  textInputFormPath: string;
  id?: string;
}

const SwitchInputField: FC<SwitchInputFieldProps> = ({
  label,
  width,
  id,
  switchFormPath,
  textInputFormPath,
}) => {
  const switchId = `${id}-switch`;
  const textFieldId = `${id}-textField`;
  const { control } = useFormContext();

  return (
    <Grid container spacing={1} alignItems="center">
      <Grid item xs={6}>
        <Controller
          control={control}
          name={switchFormPath}
          render={({ field }) => (
            <FormControlLabel
              control={<Switch id={switchId} {...field} />}
              label={label}
            />
          )}
        />
      </Grid>
      <Grid item xs={6}>
        <Controller
          control={control}
          name={textInputFormPath}
          render={({ field }) => (
            <TextField
              sx={{ width: width ? width : "auto" }}
              id={textFieldId}
              fullWidth
              size="small"
              label="Value"
              {...field}
            />
          )}
        />
      </Grid>
    </Grid>
  );
};

export default SwitchInputField;
