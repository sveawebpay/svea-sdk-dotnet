import LockField from "../../../../components/form-components/LockField";
import { Box } from "@mui/material";

const HideChangeAddress = () => {
  return (
    <Box>
      <Box>
        <LockField
          id="HideChangeAddress"
          label="Hide change address"
          switchFormPath="identityFlags.hideChangeAddress"
        />
      </Box>
    </Box>
  );
};

export default HideChangeAddress;
