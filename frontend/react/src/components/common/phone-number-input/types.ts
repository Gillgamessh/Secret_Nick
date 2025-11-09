import type { ChangeEvent } from "react";
import type { InputProps } from "../input/types";

export type PhoneNumberInputProps = Omit<
  InputProps,
  "type" | "label" | "placeholder"
> & {
  value: string;
  onChange: (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void;
};
