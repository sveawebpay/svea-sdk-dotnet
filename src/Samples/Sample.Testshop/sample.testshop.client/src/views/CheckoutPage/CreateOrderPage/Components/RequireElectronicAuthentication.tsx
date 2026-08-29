import React from "react";
import LockField from "../../../../components/form-components/LockField";
import { Grid } from "@mui/material";

const RequireElectronicAuthentication: React.FC = () => {
  return (
    <Grid container sx={{ display: "flex" }}>
      <Grid xs={6} item>
        <LockField
          id="electronicAuth"
          label="Require electronic authentication"
          switchFormPath="requireElectronicAuthentication"
        />
      </Grid>
    </Grid>
  );
};

export default RequireElectronicAuthentication;
