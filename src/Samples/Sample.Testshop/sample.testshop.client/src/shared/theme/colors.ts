import { IThemeColors } from "./types/theme.types";

const transparent = "rgba(0,0,0,0)";

const aqua = "#00aece";
const marine = "#00325c";
const turquoise = "#00699a";

const black = {
  0: "#616161",
  1: "#434343",
  2: "#898989",
  3: "rgba(0, 0, 0, 0.04)",
};

const orange = {
  0: "#fef4e5",
  1: "#f29200",
  2: "#fff2e2",
};

const yellow = "#fdc500";
const purple = "#9972c6";

const green = {
  0: "#e4f6d1",
  1: "#7acf1c",
  2: "#f5feec",
  3: "#406d0d",
  4: "#dbf2c6",
  5: "#6fa560",
};

const red = {
  0: "#f9eaea",
  1: "#c73535",
  2: "#ffe9e9",
  3: "#c6605a",
  4: "#f1a2a9",
};

const blue = {
  1: "#e4f6f9",
  2: "#cceff5",
  3: "#bbdde3",
  4: "#e5f7fa",
  5: "#0077dc",
  6: "#cdeff5",
  7: "#e8f6f9",
  8: "#a6cee3",
  9: "#00699a",
  10: "rgba(208,227,233,0.4)",
};

const solnaWhite = {
  0: "#ffffff",
  1: "#f1f0eb",
  2: "#eeede6",
  3: "#f6f6f1",
  4: "#dedad3",
  5: "#d1cdc4",
  6: "#cbcbcb",
  7: "#aba59b",
  8: "#928c82",
  9: "#787269",
  10: "#707070",
};

export const themeColors: IThemeColors = {
  transparent,
  aqua,
  turquoise,
  marine,
  black,
  orange,
  green,
  yellow,
  purple,
  red,
  blue,
  solnaWhite,
  alert: {
    error: {
      background: red[2],
      border: "#c73535",
      color: "#6f1010",
    },
    warning: {
      background: orange[2],
      border: orange[1],
      color: black[1],
    },
    info: {
      background: blue[7],
      border: marine,
      color: marine,
    },
    success: {
      background: green[4],
      border: green[3],
      color: green[3],
    },
    pending: {
      background: solnaWhite[1],
      border: black[0],
      color: black[0],
    },
  },
  buttons: {
    primary: {
      focus: "#115475",
      hover: "#146790",
      disabled: "#d4d2cb",
      label: solnaWhite[0],
      main: turquoise,
      ripple: "",
      pressed: "#105c6a",
    },
    secondary: {
      focus: solnaWhite[2],
      hover: "#146790",
      disabled: "#d4d2cb",
      label: turquoise,
      main: solnaWhite[0],
      ripple: "",
      pressed: solnaWhite[3],
    },
  },
  textButtons: (variant: string) => {
    switch (variant) {
      case "lightblue":
        return {
          focus: "#b7e8f0",
          hover: "#d2f1f6",
          disabled: "",
          label: turquoise,
          main: "",
          ripple: turquoise,
          pressed: "#bfebf2",
        };
      case "white":
        return {
          focus: "#efeeed",
          hover: "#fbfbf9",
          disabled: "",
          label: turquoise,
          main: "",
          ripple: solnaWhite[5],
          pressed: solnaWhite[2],
        };
      case "solnawhite1":
      case "solnawhite2":
      default:
        return {
          focus: "#e9e9e4",
          hover: "#f1f0ec",
          disabled: "",
          label: turquoise,
          main: "",
          ripple: solnaWhite[5],
          pressed: solnaWhite[3],
        };
    }
  },
  avatar: (variant: string) => {
    switch (variant) {
      case "lightblue":
        return {
          background: turquoise,
          border: turquoise,
          text: "white",
        };
      default:
        return {
          background: solnaWhite[0],
          border: solnaWhite[2],
          text: turquoise,
        };
    }
  },
  cards: (variant: string) => {
    switch (variant) {
      case "lightblue":
        return { background: blue[1] };
      case "solnawhite1":
        return { background: solnaWhite[1] };
      case "solnawhite2":
        return { background: solnaWhite[2] };
      default:
        return {
          background: solnaWhite[0],
        };
    }
  },
};
