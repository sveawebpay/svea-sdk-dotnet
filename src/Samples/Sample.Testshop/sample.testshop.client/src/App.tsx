import { useMemo } from "react";
import { BrowserRouter } from "react-router-dom";
import {
  createTheme,
  CssBaseline,
  StyledEngineProvider,
  ThemeProvider,
} from "@mui/material";
import { themeSettings } from "./shared/theme/theme";
import "./App.css";
import AppRouter from "./AppRouter";
import { CookiesProvider } from "react-cookie";

function App() {
  const theme = useMemo(() => createTheme(themeSettings()), []);

  return (
    <CookiesProvider defaultSetOptions={{ path: "/" }}>
      <BrowserRouter>
        <StyledEngineProvider injectFirst>
          <ThemeProvider theme={theme}>
            <CssBaseline />
            <AppRouter />
          </ThemeProvider>
        </StyledEngineProvider>
      </BrowserRouter>
    </CookiesProvider>
  );
}

export default App;
