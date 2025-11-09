import CopyButton from "../copy-button/CopyButton";
import type { RoomLinkProps } from "./types";
import styles from "../input/Input.module.scss";
import "./RoomLink.scss";

const RoomLink = ({
  title,
  description,
  url,
  caption,
  white = false,
  small = false,
}: RoomLinkProps) => {
  return (
    <div className="room-link">
      {title ? (
        <h2
          className={`room-link__title ${white ? "room-link__title--white" : ""}`}
        >
          {title}
        </h2>
      ) : null}

      {description ? (
        <p
          className={`room-link__description ${small ? "room-link__description--small" : ""} ${white ? "room-link__description--white" : ""}`}
        >
          {description}
        </p>
      ) : null}

      <div className="room-link__link-container">
        <input
          className={`${styles.inputWrapper__input} ${white ? styles["inputWrapper__input--white"] : ""}`}
          type="url"
          value={url}
          readOnly
        />
        <CopyButton
          textToCopy={url}
          buttonColor={`${white ? "white" : "green"}`}
        />
      </div>

      {caption ? (
        <p
          className={`room-link__caption ${white ? "room-link__caption--white" : ""} ${styles.inputWrapper__caption}`}
        >
          {caption}
        </p>
      ) : null}
    </div>
  );
};

export default RoomLink;
