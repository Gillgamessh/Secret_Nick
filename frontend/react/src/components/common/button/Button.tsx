import type { ButtonProps } from "./types.ts";
import "./Button.scss";

const Button = ({
  variant = "primary",
  size = "large",
  iconName,
  width,
  children,
  ...restProps
}: ButtonProps) => {
  const buttonClassName = [
    "button",
    `button--${variant}`,
    `button--${size}`,
    iconName && `button--icon-${iconName}`,
    variant === "secondary" && size === "small" && "button--secondary-small",
  ]
    .filter(Boolean)
    .join(" ");

  return (
    <button className={buttonClassName} style={{ width }} {...restProps}>
      {children}
    </button>
  );
};

export default Button;
