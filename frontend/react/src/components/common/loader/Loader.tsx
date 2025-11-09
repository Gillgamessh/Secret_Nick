import { createPortal } from "react-dom";
import "./Loader.scss";

const Loader = () => {
  const loaderElement = (
    <div className="loader-container">
      <div className="loader" />
    </div>
  );

  return createPortal(loaderElement, document.body);
};

export default Loader;
