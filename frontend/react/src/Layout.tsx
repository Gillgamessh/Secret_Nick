import { Outlet } from "react-router";
import Footer from "./components/common/footer/Footer";
import Header from "./components/common/header/Header";
import { ToasterProvider } from "./contexts/toaster-context/ToasterContext";

const Layout = () => {
  return (
    <>
      <ToasterProvider>
        <Header />
        <Outlet />
        <Footer />
      </ToasterProvider>
    </>
  );
};

export default Layout;
