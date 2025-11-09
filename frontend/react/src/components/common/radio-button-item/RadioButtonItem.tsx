import RadioButton from "../radio-button/RadioButton";
import type { RadioButtonItemProps } from "./types.ts";
import "./RadioButtonItem.scss";

export const RadioButtonItem = ({
  text,
  selected,
  disabled,
  name,
  onChange,
  children,
  ...restProps
}: RadioButtonItemProps) => {
  return (
    <div
      className={`radio-button-item ${selected ? "radio-button-item--selected" : ""}`}
    >
      <RadioButton
        text={text}
        name={name}
        checked={selected}
        disabled={disabled}
        onChange={onChange}
        {...restProps}
      />

      {children ? (
        <div className="radio-button-item__content">{children}</div>
      ) : null}
    </div>
  );
};
