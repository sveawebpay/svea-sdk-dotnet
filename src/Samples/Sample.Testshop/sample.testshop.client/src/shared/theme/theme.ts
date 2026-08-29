import { ThemeOptions } from "@mui/material"
import { paletteOptions } from "./palette"
import { typographyOptions } from './typography';
import { components } from './components';

// mui theme settings
export const themeSettings = (): ThemeOptions => {
    return {
      palette: paletteOptions,
      shape: {
        borderRadius: 6,
      },
      typography:typographyOptions,
      components
    };
};