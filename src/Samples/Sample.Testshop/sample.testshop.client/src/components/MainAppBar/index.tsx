import { AppBar, Box, Toolbar } from "@mui/material";
import LogoLight from "../../assets/logolight.svg?react";
import { useNavigate } from "react-router";

const MainAppBar = () => {
  const navigate = useNavigate();
  return (
    <Box sx={{ flexGrow: 1, position: "relative" }}>
      <AppBar
        elevation={1}
        sx={(theme) => ({
          position: "relative",
          background: theme.palette.common.white,
          color: theme.palette.primary.dark,
        })}
      >
        <Toolbar>
          <LogoLight onClick={() => navigate("home")} cursor="pointer" />
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default MainAppBar;
