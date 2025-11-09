import styles from "../input/Input.module.scss";
import type { InputProps } from "../input/types";
import { useAutoResizeTextarea } from "../../../hooks/useAutoResizeTextarea";

const Textarea = ({
  placeholder,
  value,
  onChange,
  maxLength = 500,
  label,
  width = "100%",
  hasError = false,
  ...restProps
}: InputProps) => {
  const inputId = `input-${label?.replace(" ", "-").toLowerCase()}`;
  const textareaRef = useAutoResizeTextarea(String(value));

  return (
    <div
      className={`${styles.inputWrapper} ${hasError ? styles["inputWrapper--error"] : ""}`}
      style={{ width }}
    >
      <textarea
        id={inputId}
        value={value}
        ref={textareaRef}
        onChange={onChange}
        maxLength={maxLength}
        placeholder={placeholder}
        className={styles.inputWrapper__input}
        {...restProps}
      />
    </div>
  );
};

export default Textarea;
