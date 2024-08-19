import InputField from "../../../../components/form-components/InputField";
import { Box } from "@mui/material";

const MerchantData = () => {
  return (
    <Box>
      <InputField
        id="merchantData"
        label="Merchant data"
        textInputFormPath="merchantData"
        width="100%"
      />
    </Box>
  );
};

export default MerchantData;
