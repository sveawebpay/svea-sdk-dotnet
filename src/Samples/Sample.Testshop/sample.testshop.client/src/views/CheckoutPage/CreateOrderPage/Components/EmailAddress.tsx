import React from "react";
import SwitchInputField from "../../../../components/form-components/SwitchInputField";
import { Box } from "@mui/material";

const EmailAddress: React.FC = () => {
  return (
    <Box>
      <SwitchInputField
        id="email"
        label="Email address"
        switchFormPath="emailPresetValue.isLocked"
        textInputFormPath="emailPresetValue.value"
        width="100%"
      />
    </Box>
  );
};

export default EmailAddress;
