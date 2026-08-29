import { Box, BoxProps } from "@mui/material";
import { themeColors } from "../../shared/theme/colors";

export const PageContainer = (props: BoxProps) => (
  <Box
    flexGrow={1}
    {...props}
    sx={{
      top: 0,
      left: 0,
      right: 0,
      bottom: 0,
      height: "100%",
      display: "flex",
      overflow: "auto",
      position: "relative",
      alignItems: "center",
      justifyContent: "center",
      backgroundColor: themeColors.solnaWhite[3],
    }}
  />
);
