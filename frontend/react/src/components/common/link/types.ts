import type { AnchorHTMLAttributes, ReactNode } from "react";

export type LinkColor = "green" | "white";
export type LinkSize = "small" | "large";

export interface LinkProps extends AnchorHTMLAttributes<HTMLAnchorElement> {
  color?: LinkColor;
  size?: LinkSize;
  children: ReactNode;
  disabled?: boolean;
}
