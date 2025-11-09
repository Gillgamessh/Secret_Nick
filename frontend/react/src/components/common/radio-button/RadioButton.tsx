import type { RadioButtonProps } from "./types";
import "./RadioButton.scss";

const RadioButton = ({
  text,
  name,
  disabled,
  checked,
  onChange,
  ...restProps
}: RadioButtonProps) => {
  return (
    <label className="radio-button">
      <input
        type="radio"
        name={name}
        disabled={disabled}
        checked={checked}
        onChange={onChange}
        {...restProps}
      />
      <span className="radio-button__circle" />
      {text ? <span className="radio-button__text">{text}</span> : null}
    </label>
  );
};
export default RadioButton;
