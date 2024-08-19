import React, { FC } from "react";
import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  Grid,
} from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

interface SelectOptionProps {
  id?: string;
  width?: string;
  label?: string;
  value?: string;
  options: { value: string | number; label: string }[];
  selectFormPath: string;
  onChange?: (event: SelectChangeEvent) => void;
}

const SelectOption: FC<SelectOptionProps> = ({
  id,
  width,
  label,
  value,
  options,
  selectFormPath,
  onChange,
}) => {
  const { control } = useFormContext();
  const selectId = `${id}-select`;

  return (
    <Grid container alignItems="center" spacing={1}>
      <Grid item sx={{ width: width }}>
        <FormControl fullWidth>
          <InputLabel shrink>{label}</InputLabel>
          <Controller
            name={selectFormPath}
            control={control}
            defaultValue={value || ""}
            render={({ field }) => (
              <Select
                id={selectId}
                {...field}
                label={label}
                value={field.value || ""}
                onChange={(event) => {
                  field.onChange(event);
                  if (onChange) {
                    onChange(event);
                  }
                }}
                size="small"
                IconComponent={ExpandMoreIcon}
              >
                {options.map((option) => (
                  <MenuItem key={option.value} value={option.value}>
                    {option.label}
                  </MenuItem>
                ))}
              </Select>
            )}
          />
        </FormControl>
      </Grid>
    </Grid>
  );
};

export default SelectOption;
