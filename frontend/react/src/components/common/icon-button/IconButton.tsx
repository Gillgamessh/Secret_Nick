import type { IconButtonProps } from "./types.ts";
import { ICONS_PATH } from "./utils.ts";
import "./IconButton.scss";

const IconButton = ({
  iconName,
  color = "green",
  onClick,
  ...restProps
}: IconButtonProps) => {
  return (
    <button
      className={`icon-button icon-button--${color}`}
      onClick={onClick}
      type="button"
      {...restProps}
    >
      <svg className="icon-button__icon">
        <use href={`${ICONS_PATH}${iconName}`} />
      </svg>
    </button>
  );
};

export default IconButton;
