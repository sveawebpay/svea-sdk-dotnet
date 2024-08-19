import { PaletteOptions } from "@mui/material/styles";

import { themeColors } from "./colors";

export const paletteOptions: PaletteOptions = {
  mode: "light",
  primary: {
    contrastText: themeColors.solnaWhite[0],
    dark: themeColors.marine,
    light: themeColors.blue[1],
    main: themeColors.turquoise,
  },
  secondary: {
    main: themeColors.turquoise,
  },
  background: {
    default: themeColors.transparent,
    paper: themeColors.solnaWhite[0],
  },
  text: {
    primary: themeColors.black[1],
    secondary: themeColors.black[2],
  },
  error: {
    main: themeColors.red[1],
  },
};
