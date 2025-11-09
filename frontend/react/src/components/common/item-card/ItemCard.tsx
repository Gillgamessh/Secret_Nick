import type { ItemCardProps } from "./types";
import "./ItemCard.scss";

const ItemCard = ({
  title,
  children,
  width = "100%",
  isFocusable = false,
}: ItemCardProps) => {
  return (
    <div
      tabIndex={isFocusable ? 1 : undefined}
      className={`item-card ${isFocusable ? "item-card--focus-visible" : ""}`}
      style={{ width }}
    >
      {title ? <p className="item-card__title">{title}</p> : null}
      {children}
    </div>
  );
};

export default ItemCard;
