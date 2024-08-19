import SwitchInputField from "../../../../components/form-components/SwitchInputField";
import { Box } from "@mui/material";

const PhoneNumber = () => {
  return (
    <Box>
      <SwitchInputField
        id="PhoneNumber"
        label="Phone number"
        switchFormPath="phoneNumberPresetValue.isLocked"
        textInputFormPath="phoneNumberPresetValue.value"
        width="100%"
      />
    </Box>
  );
};

export default PhoneNumber;
