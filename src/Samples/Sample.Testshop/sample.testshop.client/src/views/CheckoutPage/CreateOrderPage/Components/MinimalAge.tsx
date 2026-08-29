import InputField from "../../../../components/form-components/InputField";
import { Box } from "@mui/material";
import LockField from "../../../../components/form-components/LockField";

const MinimalAge = () => {
  return (
    <Box>
      <InputField
        id="minAgeInput"
        label="Minimal age"
        width="100%"
        textInputFormPath="validation.minAge"
      />
      <LockField
        id="paymentFailureResiliency"
        label="Use resiliance (snapshot)"
        switchFormPath="validation.paymentFailureResiliency"
      />
    </Box>
  );
};

export default MinimalAge;
