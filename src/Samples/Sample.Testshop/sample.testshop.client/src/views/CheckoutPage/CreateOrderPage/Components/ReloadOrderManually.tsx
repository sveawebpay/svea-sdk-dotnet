import LockField from "../../../../components/form-components/LockField";
import { Box } from "@mui/material";

const ReloadOrderManually = () => {
  return (
    <Box>
      <LockField
        id="reloadOrder"
        label="Reload order manually"
        switchFormPath="reloadOrderManually"
      />
    </Box>
  );
};

export default ReloadOrderManually;
