import { Outlet } from "react-router-dom";
import { PageContainer } from "../../components/PageContainer";
import MainAppBar from "../../components/MainAppBar";

const MainLayout = () => {
  return (
    <>
      <MainAppBar />
      <PageContainer>
        <Outlet />
      </PageContainer>
    </>
  );
};

export default MainLayout;
