import LockField from "../../../../components/form-components/LockField";
import { Box } from "@mui/material";

const Validate = () => {
  return (
    <Box>
      <LockField
        id="validate"
        label="Validate"
        switchFormPath="validationCallback.validate"
      />
    </Box>
  );
};

export default Validate;
