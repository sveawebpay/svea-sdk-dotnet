import React from "react";
import SwitchInputField from "../../../../components/form-components/SwitchInputField";
import { Box } from "@mui/material";

const NationalId: React.FC = () => {
  return (
    <Box>
      <SwitchInputField
        id="nationalId"
        label="National Id"
        switchFormPath="nationalIdPresetValue.isLocked"
        textInputFormPath="nationalIdPresetValue.value"
        width="100%"
      />
    </Box>
  );
};

export default NationalId;
