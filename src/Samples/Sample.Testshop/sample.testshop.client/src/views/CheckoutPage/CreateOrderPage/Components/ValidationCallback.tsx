import { Grid, Typography } from "@mui/material";
import Divider from "@mui/material/Divider";
import VerifiedUserOutlinedIcon from "@mui/icons-material/VerifiedUserOutlined";
import ValidationCallbackToggle from "./ValidationCallbackToggle";
import Validate from "./Validate";
import ValidateCallbackErrorMessage from "./ValidateCallbackErrorMessage";
import MinimalAge from "./MinimalAge";

const ValidationCallback = () => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <VerifiedUserOutlinedIcon fontSize="small" sx={{ marginRight: 1 }} />
        Validation
      </Typography>

      <Divider />

      <Grid item xs={12} sx={{ padding: 2 }}>
        <ValidationCallbackToggle />
        <Validate />
        <ValidateCallbackErrorMessage />
        <MinimalAge />
      </Grid>
    </>
  );
};

export default ValidationCallback;
