import styles from "./Input.module.scss";
import type { InputProps } from "./types";
import Textarea from "../textarea/Textarea";

const Input = ({
  type = "text",
  placeholder = "",
  value,
  onChange,
  label = "",
  required = false,
  width = "100%",
  caption,
  hasError = false,
  maxLength = 40,
  withoutCounter = false,
  withSuffix = false,
  withPrefix = false,
  multiline = false,
  variant = "default",
  ...restProps
}: InputProps) => {
  const inputId = `input-${label.replace(" ", "-").toLowerCase()}`;
  const Element = multiline ? Textarea : "input";
  const counterPosition = multiline ? "outside" : "inside";

  const baseClass = styles.inputWrapper__input;

  const multilineClass = multiline
    ? styles["inputWrapper__input--textarea"]
    : "";

  const counterClass = !withoutCounter
    ? styles[`inputWrapper__input--counter-${counterPosition}`]
    : "";

  const variantClass =
    variant && variant !== "default"
      ? styles[`inputWrapper__input--${variant}`]
      : "";

  const inputClassName = [baseClass, multilineClass, counterClass, variantClass]
    .filter(Boolean)
    .join(" ");

  return (
    <div
      className={`${styles.inputWrapper} ${hasError ? styles["inputWrapper--error"] : ""}`}
      style={{ width }}
    >
      {label ? (
        <label
          htmlFor={inputId}
          className={`${styles.inputWrapper__label} ${
            required ? styles["inputWrapper__label--required"] : ""
          }`}
        >
          {label}
        </label>
      ) : null}

      <div className={styles.inputWrapper__inputContainer}>
        <Element
          id={inputId}
          value={value}
          onChange={onChange}
          label={label}
          maxLength={maxLength}
          type={!multiline ? type : undefined}
          placeholder={placeholder}
          className={inputClassName}
          {...restProps}
        />

        {!withoutCounter && maxLength > 0 ? (
          <div
            className={`${styles.inputWrapper__counter} ${styles[`inputWrapper__counter--${counterPosition}`]}`}
          >
            {value.toString().length} / {maxLength}
          </div>
        ) : null}

        {withSuffix ? (
          <div className={styles.inputWrapper__suffix}>UAH</div>
        ) : null}

        {withPrefix ? (
          <div className={styles.inputWrapper__prefix}>+380</div>
        ) : null}
      </div>

      {caption ? (
        <span className={styles.inputWrapper__caption}>{caption}</span>
      ) : null}
    </div>
  );
};

export default Input;
