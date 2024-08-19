import React from "react";
import LockField from "../../../../components/form-components/LockField";
import { Box } from "@mui/material";

const HideNotYou: React.FC = () => {
  return (
    <Box>
      <LockField
        id="hide-not-you"
        label="Hide not you"
        switchFormPath="identityFlags.hideNotYou"
      />
    </Box>
  );
};

export default HideNotYou;
