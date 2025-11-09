import type { ReactNode, ChangeEvent } from "react";

export interface RadioButtonItemProps {
  text: string;
  selected: boolean;
  disabled?: boolean;
  name?: string;
  onChange?: (e: ChangeEvent<HTMLInputElement>) => void;
  children?: ReactNode;
}
