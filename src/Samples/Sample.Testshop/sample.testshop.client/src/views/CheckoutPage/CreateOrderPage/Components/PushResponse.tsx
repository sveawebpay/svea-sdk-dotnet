import InputField from "../../../../components/form-components/InputField";
import { Box } from "@mui/material";

const PushResponse = () => {
  return (
    <Box>
      <InputField
        id="push-response-delay"
        label="Push response delay"
        textInputFormPath="checkoutPushData.responseDelay"
        width="100%"
      />
      <InputField
        id="push-response-status-code-input"
        label="Push response status code"
        textInputFormPath="checkoutPushData.responseStatusCode"
        width="100"
      />
    </Box>
  );
};

export default PushResponse;
