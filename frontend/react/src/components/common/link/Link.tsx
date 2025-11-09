import type { LinkProps } from "./types";
import "./Link.scss";

const Link = ({
  color = "green",
  size = "large",
  disabled,
  children,
  ...restProps
}: LinkProps) => {
  return (
    <a
      className={`link link--${color} link--${size} ${disabled ? "link--disabled" : ""}`}
      aria-disabled={disabled}
      tabIndex={disabled ? -1 : 0}
      {...restProps}
    >
      {children}
    </a>
  );
};

export default Link;
