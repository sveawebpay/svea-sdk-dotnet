import React from "react";
import SwitchInputField from "../../../../components/form-components/SwitchInputField";
import { Box } from "@mui/material";

const PostalCode: React.FC = () => {
  return (
    <Box>
      <SwitchInputField
        id="postal-code"
        label="Postal code"
        switchFormPath="postalCodePresetValue.isLocked"
        textInputFormPath="postalCodePresetValue.value"
        width="100%"
      />
    </Box>
  );
};

export default PostalCode;
