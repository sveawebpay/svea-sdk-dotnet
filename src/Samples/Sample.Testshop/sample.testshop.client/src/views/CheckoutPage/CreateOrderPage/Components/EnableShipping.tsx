import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Divider,
  Grid,
  Typography,
} from "@mui/material";
import LocalShippingOutlinedIcon from "@mui/icons-material/LocalShippingOutlined";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import LockField from "../../../../components/form-components/LockField";
import InputField from "../../../../components/form-components/InputField";

const EnableShipping = () => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <LocalShippingOutlinedIcon fontSize="small" sx={{ marginRight: 1 }} />
        Shipping
      </Typography>
      <Divider />

      <Grid item xs={12} sx={{ padding: 2 }}>
        <LockField
          id="enable-shipping"
          label="Enable shipping"
          switchFormPath="shippingInformation.enableShipping"
        />
        <Accordion>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1a-content"
            id="panel1a-header"
          >
            <Typography variant="body1">Additional options</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Grid container spacing={2} alignItems="center">
              <Grid item xs={12}>
                <InputField
                  width="50px"
                  id="shipping-weight"
                  textInputFormPath="shippingInformation.weight"
                  label="Weight"
                ></InputField>
                <InputField
                  id="shipping-tags"
                  textInputFormPath="shippingInformation.tags"
                  label="Tags"
                ></InputField>
                <LockField
                  id="enforce-shipping-callback"
                  label="Enforce fallback"
                  switchFormPath="shippingInformation.enforceCallback"
                />
                <LockField
                  id="enforce-shipping-reject-shipping-session"
                  label="Should reject shipping session"
                  switchFormPath="shippingInformation.shouldRejectShippingSession"
                />
              </Grid>
            </Grid>
          </AccordionDetails>
        </Accordion>
      </Grid>
    </>
  );
};

export default EnableShipping;
