import InputField from "../../../../components/form-components/InputField";
import { Box } from "@mui/material";

const PartnerKey = () => {
  return (
    <Box>
      <InputField
        id="partnerKey"
        label="Partner key"
        textInputFormPath="partnerKey"
        width="100%"
      />
    </Box>
  );
};

export default PartnerKey;
