import { useNavigate } from "react-router";
import Button from "../common/button/Button";
import "./NotFoundPage.scss";

const NotFoundPage = () => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate("/");
  };

  return (
    <main className="not-found-page">
      <div className="not-found-page__content">
        <h2>Oops! Page Not Found</h2>

        <div className="not-found-page__bottom">
          <p className="not-found-page__bottom-text">
            This page doesn’t exist or was moved (Maybe, somebody has stolen
            it!)
          </p>

          <Button iconName="home" width={224} onClick={handleClick}>
            Go Home
          </Button>
        </div>
      </div>
    </main>
  );
};

export default NotFoundPage;
