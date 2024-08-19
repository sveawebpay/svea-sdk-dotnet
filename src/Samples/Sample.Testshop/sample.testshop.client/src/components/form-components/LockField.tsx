import React, { FC } from "react";
import { FormControlLabel, Switch, Grid } from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";

interface LockFieldProps {
  id?: string;
  label?: string;
  switchFormPath: string;
}

const LockField: React.FC<LockFieldProps> = ({ id, label, switchFormPath }) => {
  const switchId = `${id}-switch`;
  const { control } = useFormContext();

  return (
    <Grid container spacing={1} alignItems="center">
      <Grid item>
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
    </Grid>
  );
};

export default LockField;
