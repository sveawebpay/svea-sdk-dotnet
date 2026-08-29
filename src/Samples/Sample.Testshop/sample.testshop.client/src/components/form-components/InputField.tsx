import React, { FC } from "react";
import { TextField, Grid, Typography } from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";

interface InputFieldProps {
  id?: string;
  label?: string;
  width?: string | number;
  textInputFormPath: string;
}

const InputField: FC<InputFieldProps> = ({
  id,
  label,
  width,
  textInputFormPath,
}) => {
  const { control } = useFormContext();
  const inputId = `${id}`;
  return (
    <Grid container alignItems="center">
      {label && (
        <Grid item xs={6}>
          <Typography>{label}</Typography>
        </Grid>
      )}
      <Grid item xs={6}>
        <Controller
          control={control}
          name={textInputFormPath}
          render={({ field }) => (
            <TextField
              fullWidth
              sx={{ width: width ? width : "auto" }}
              id={inputId}
              size="small"
              {...field}
            />
          )}
        />
      </Grid>
    </Grid>
  );
};

export default InputField;
