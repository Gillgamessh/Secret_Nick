export const BUTTON_COLORS = {
  GREEN: "green",
  WHITE: "white",
} as const;

export type ButtonColor = (typeof BUTTON_COLORS)[keyof typeof BUTTON_COLORS];

export type IconName = "copy" | "link";

export interface CopyButtonProps {
  textToCopy: string;
  iconName?: IconName;
  buttonColor?: ButtonColor;
  successMessage?: string;
  errorMessage?: string;
}
