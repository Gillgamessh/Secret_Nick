import type { ReactNode } from "react";

export const ICON_NAMES = {
  PRESENTS: "presents",
  COOKIE: "cookie",
  NOTE: "note",
  CAR: "car",
} as const;

type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];

export interface ModalProps {
  title: string;
  description: string;
  subdescription?: string;
  iconName: IconName;
  isOpen?: boolean;
  onClose: () => void;
  onConfirm: () => void;
  children: ReactNode;
}
