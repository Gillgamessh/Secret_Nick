import type { InputProps } from "../input/types";

export type InvitationNoteProps = Omit<
  InputProps,
  "type" | "label" | "placeholder"
> & {
  value: string;
  invitationLink: string;
  userCode?: string;
};
