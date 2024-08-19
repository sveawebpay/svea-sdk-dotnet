
import { Divider, Grid, Typography } from "@mui/material";
import CreditCardOutlinedIcon from "@mui/icons-material/CreditCardOutlined";
import ClientOrderNumber from "./ClientOrderNumber";
import PromotedPartPaymentCampaign from "./PromotedPartPayment";
import ActivePartPaymentCampaign from "./ActivePartPaymentCampaign";

const PaymentOptionsPresets = () => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <CreditCardOutlinedIcon fontSize="small" sx={{ marginRight: 1 }} />
        Payment
      </Typography>
      <Divider />
      <Grid item sx={{ padding: 2 }}>
        <ClientOrderNumber />
        <PromotedPartPaymentCampaign isCompany={false} />
        <ActivePartPaymentCampaign isCompany={false} />
      </Grid>
    </>
  );
};

export default PaymentOptionsPresets;
