import LockField from "../../../../components/form-components/LockField";
import { Box } from "@mui/material";

const ValidationCallbackToggle = () => {
  return (
    <Box>
      <LockField
        id="validationCallback"
        label="Use validation callback"
        switchFormPath="validationCallback.useValidationCallback"
      />
    </Box>
  );
};

export default ValidationCallbackToggle;
