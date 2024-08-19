import React from "react";
import { Box } from "@mui/material";
import LockField from "../../../../components/form-components/LockField";

const HideAnonymous = () => {
  return (
    <Box>
      <LockField
        id="hide-anonymous"
        label="Hide anonymous"
        switchFormPath="identityFlags.hideAnonymous"
      />
    </Box>
  );
};

export default HideAnonymous;
