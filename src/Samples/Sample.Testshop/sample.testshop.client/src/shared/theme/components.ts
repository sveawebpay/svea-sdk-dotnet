import { Components, Theme } from "@mui/material";

export const components: Components<Omit<Theme, "components">> = {
  MuiAppBar: {
    styleOverrides: {
      root: {
        boxShadow: "none",
      },
    },
  },

  MuiTypography: {
    styleOverrides: {
      root: {
        display: "flex",
        alignItems: "center",
        "& .MuiSvgIcon-root": {
          marginRight: "8px",
        },
        "& strong": {
          marginRight: "5px",
        },
      },
    },
  },

  MuiButton: {
    styleOverrides: {
      root: {
        textTransform: "none",
        borderRadius: "8px",
      },
    },
  },

  MuiGrid: {
    styleOverrides: {
      root: {
        padding: "0",
        marginBottom: 3,
      },
    },
  },
  MuiDivider: {
    styleOverrides: {
      root: {
        deviderColor: "#",
        borderBottomWidth: "1px",
        margin: 0,
      },
    },
  },

  MuiIcon: {
    styleOverrides: {
      root: {
        marginLeft: 1,
        h6: {
          marginRight: 1,
          alignItems: "center",
        },
      },
    },
  },

  MuiTextField: {
    styleOverrides: {
      root: {
        "& .MuiOutlinedInput-root": {
          borderRadius: "8px",
        },
      },
    },
  },

  MuiOutlinedInput: {
    styleOverrides: {
      root: {
        borderRadius: "8px",
      },
    },
  },
  MuiSelect: {
    styleOverrides: {
      icon: {
        color: "#0a5588",
      },
    },
  },
  MuiCheckbox: {
    styleOverrides: {
      root: {
        color: "#0a5588",
        "&.Mui-checked": {
          color: "#0a5588",
        },
      },
    },
  },
  MuiIconButton: {
    styleOverrides: {
      root: {
        color: "#0a5588",
        fontSize: "0.8rem",
        fontWeight: 700,
        fontFamily: "Calibri, Arial, Sans-serif",
      },
    },
  },
  MuiSvgIcon: {
    styleOverrides: {
      root: {
        color: "#0a5588",
      },
    },
  },

  MuiToggleButtonGroup: {
    styleOverrides: {
      root: {
        height: 35,
      },
    },
  },

  MuiToggleButton: {
    styleOverrides: {
      root: {
        borderColor: "#C4C4C4",
        borderRadius: 8,
        color: "#333333",
        "&.Mui-selected": {
          color: "#ffffff",
          backgroundColor: "#115293",
          borderColor: "#115293",
          "&:hover": {
            backgroundColor: "#115293",
          },
        },
      },
    },
  },

  MuiAccordion: {
    styleOverrides: {
      root: {
        marginTop: 10,
        border: "none",
        borderTop: "1px solid #E0E0E0",
        boxShadow: "none",
        borderRadius: "0 !important",
      },
    },
  },
  MuiAccordionSummary: {
    styleOverrides: {
      root: {
        padding: 0,
      },
    },
  },
  MuiAccordionDetails: {
    styleOverrides: {
      root: {
        padding: 0,
      },
    },
  },
};
