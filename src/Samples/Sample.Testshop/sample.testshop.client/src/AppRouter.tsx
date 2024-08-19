import { Route, Routes } from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
import React from "react";
import Home from "./views/Home";
import CreateOrderPage from "./views/CheckoutPage/CreateOrderPage";
import CheckoutPage from "./views/CheckoutPage";
import DisplayOrderPage from "./views/CheckoutPage/DIsplayOrderPage";

const routes = [
  {
    path: "/",
    component: Home,
  },
  {
    path: "checkout",
    component: CheckoutPage,
  },
  {
    path: "checkout/create-order",
    component: CreateOrderPage,
  },
  {
    path: "checkout/display-order/:id",
    component: DisplayOrderPage,
  },
];

const AppRouter: React.FC = () => {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        {routes.map((route, i: number) => {
          const { component: Component, path } = route;
          return (
            <Route path={path} element={<Component />} key={`route-${i}`} />
          );
        })}
      </Route>
    </Routes>
  );
};

export default AppRouter;
