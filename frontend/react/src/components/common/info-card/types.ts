import type { ReactNode } from "react";

export interface InfoCardProps {
  title: string;
  description?: string;
  iconName: IconName;
  width?: string;
  variant?: "white" | "green";
  children?: ReactNode;
}

export const ICON_NAMES = {
  STAR: "star",
  PRESENTS: "presents",
  NOTE: "note",
} as const;

export type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];
