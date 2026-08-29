import { Route, Routes } from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
import React from "react";
import Home from "./views/Home";
import CreateOrderPage from "./views/CheckoutPage/CreateOrderPage";
import CheckoutPage from "./views/CheckoutPage";
import DisplayOrderPage from "./views/CheckoutPage/DIsplayOrderPage";
import CreateRecurringOrderPage from "./views/CheckoutPage/CreateRecurringOrderPage";
import TokenManagementPage from "./views/CheckoutPage/TokenManagmentPage";

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
  {
    path: "checkout/recurring/create",
    component: CreateRecurringOrderPage,
  },
  {
    path: "/checkout/recurring/management",
    component: TokenManagementPage,
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
