import { useEffect } from "react";
import { useNavigate, useLocation } from "react-router";
import useToaster from "@hooks/useToaster";
import Button from "../common/button/Button";
import type { HomePageLocationState } from "./types";
import { HOME_PAGE_TITLE } from "./utils";
import "./HomePage.scss";

const HomePage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const toaster = useToaster();

  const handleClick = () => navigate("/create-room");

  useEffect(() => {
    document.title = HOME_PAGE_TITLE;

    const state = location.state as HomePageLocationState | undefined;

    if (state?.toastMessage) {
      toaster.showToast(state.toastMessage, "error", "large");

      navigate(".", { replace: true, state: {} });
    }
  }, [location, navigate, toaster]);

  return (
    <main className="home-page">
      <div className="home-page__container">
        <h1 className="home-page__title">
          Make This Holiday Magical with Secret Nick
        </h1>
        <p className="home-page__description">
          It’s a secret — don’t tell who you’re matched with!
        </p>
        <p className="home-page__description">
          Use the wishlist or preferences to pick the perfect gift.
        </p>
        <p className="home-page__description">
          Be ready for the big gift exchange!
        </p>

        <Button width={281} onClick={handleClick}>
          Create Your Room
        </Button>
      </div>
    </main>
  );
};

export default HomePage;
