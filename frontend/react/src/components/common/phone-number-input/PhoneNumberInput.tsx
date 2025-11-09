import type { KeyboardEvent } from "react";
import Input from "../input/Input";
import type { InputChangeHandler } from "../../../types/general";
import type { PhoneNumberInputProps } from "./types";
import { isValidPhoneNumber } from "./utils";
import { blockInvalidNumberKeys } from "../../../utils/validation";

const PhoneNumberInput = ({
  value,
  onChange,
  ...restProps
}: PhoneNumberInputProps) => {
  const handlePhoneNumberChange: InputChangeHandler = (e) => {
    const phoneNumberValue = e.target.value;

    if (isValidPhoneNumber(phoneNumberValue)) {
      onChange?.(e);
    }
  };

  const handlePhoneNumberKeyDown = (
    e: KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    blockInvalidNumberKeys(e);
  };

  return (
    <Input
      placeholder="777777777"
      label="Phone number"
      width="338px"
      withoutCounter
      withPrefix
      required
      value={value}
      onChange={handlePhoneNumberChange}
      onKeyDown={handlePhoneNumberKeyDown}
      {...restProps}
    />
  );
};

export default PhoneNumberInput;
