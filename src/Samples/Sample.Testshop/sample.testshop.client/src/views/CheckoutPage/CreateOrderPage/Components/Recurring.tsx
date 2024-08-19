import React from "react";
import LockField from "../../../../components/form-components/LockField";
import { Box } from "@mui/material";

const Recurring: React.FC = () => {
  return (
    <Box>
      <LockField
        id="recurring"
        label="Recurring payments"
        switchFormPath="recurring"
      />
    </Box>
  );
};

export default Recurring;
