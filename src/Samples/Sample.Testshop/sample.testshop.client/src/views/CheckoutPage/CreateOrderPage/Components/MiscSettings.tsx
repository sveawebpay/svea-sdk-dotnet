import { Divider, Grid, Typography } from "@mui/material";
import ListAltOutlinedIcon from "@mui/icons-material/ListAltOutlined";
import MerchantData from "./MerchantData";
import PartnerKey from "./PartnerKey";
import PushResponse from "./PushResponse";

const MiscSettings = () => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <ListAltOutlinedIcon fontSize="small" sx={{ marginRight: 1 }} />
        Miscellaneous
      </Typography>
      <Divider />

      <Grid item xs={12} sx={{ padding: 2 }}>
        <MerchantData />
        <PartnerKey />
        <PushResponse />
      </Grid>
    </>
  );
};

export default MiscSettings;
