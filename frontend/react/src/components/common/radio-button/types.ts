import type { ChangeEvent } from "react";

export interface RadioButtonProps {
  text?: string;
  checked?: boolean;
  disabled?: boolean;
  name?: string;
  onChange?: (e: ChangeEvent<HTMLInputElement>) => void;
}
