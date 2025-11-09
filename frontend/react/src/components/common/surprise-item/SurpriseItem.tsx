import type { SurpriseItemProps } from "./types";
import "./SurpriseItem.scss";

const SurpriseItem = ({ text }: SurpriseItemProps) => {
  return (
    <div className="surprise-item">
      <p className="surprise-item__title">Surprise me!</p>
      {text ? <p className="surprise-item__description">{text}</p> : null}
    </div>
  );
};

export default SurpriseItem;
