import type { ReactNode } from "react";

export interface ItemCardProps {
  title?: string;
  children?: ReactNode;
  width?: string;
  isFocusable?: boolean;
}
