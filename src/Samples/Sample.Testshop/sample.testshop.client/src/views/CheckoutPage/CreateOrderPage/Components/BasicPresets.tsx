import React, { useState } from "react";
import { Divider, Grid, Typography } from "@mui/material";
import ToggleOffOutlinedIcon from "@mui/icons-material/ToggleOffOutlined";
import IsCompanySwitch from "./IsCompanySwitch";
import EmailAddress from "./EmailAddress";
import HideAnonymous from "./HideAnonymous";
import PostalCode from "./PostalCode";
import NationalId from "./NationalId";
import PhoneNumber from "./PhoneNumber";
import InternationalMarketSelection from "./InternationalMarketSelection";
import HideNotYou from "./HideNotYou";
import HideChangeAddress from "./HideChangeAddress";
import RequireElectronicAuthentication from "./RequireElectronicAuthentication";
import Recurring from "./Recurring";
import ReloadOrderManually from "./ReloadOrderManually";

interface BasicPresetsProps {}

const BasicPresets: React.FC<BasicPresetsProps> = () => {
  return (
    <>
      <Typography variant="h6" display="flex" alignItems="center">
        <ToggleOffOutlinedIcon fontSize="medium" sx={{ marginRight: -1 }} />
        Locks & Toggles
      </Typography>

      <Divider />

      <Grid item sx={{ padding: 2 }}>
        <Typography
          variant="h6"
          display="flex"
          alignItems="center"
          sx={{ marginLeft: -1 }}
        >
          Lock
        </Typography>
        <IsCompanySwitch />
        <EmailAddress />
        <PostalCode />
        <NationalId />
        <PhoneNumber />

        <InternationalMarketSelection />
        <Divider sx={{ margin: "15px 0" }}></Divider>

        <HideAnonymous />
        <HideNotYou />
        <HideChangeAddress />
        <RequireElectronicAuthentication />
        <Recurring />
        <ReloadOrderManually />
        {/* <IframeWidth /> */}
      </Grid>
    </>
  );
};

export default BasicPresets;
