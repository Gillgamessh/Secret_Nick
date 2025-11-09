import type { InfoCardProps } from "./types";
import "./InfoCard.scss";

const InfoCard = ({
  title,
  description,
  iconName,
  width = "100%",
  variant = "green",
  children,
}: InfoCardProps) => {
  return (
    <div
      className={`info-card ${variant === "white" ? "info-card--white" : ""}`}
      style={{ width }}
    >
      <p className={`info-card__title info-card__title--${iconName}`}>
        {title}
      </p>

      {description ? (
        <p className="info-card__description">{description}</p>
      ) : null}

      {children}
    </div>
  );
};

export default InfoCard;
