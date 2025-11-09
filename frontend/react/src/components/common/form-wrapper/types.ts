import type { HTMLAttributes, ReactNode } from "react";
import type { ButtonProps } from "../button/types";

export const FORM_CONTENT_MAP = {
  CREATE_ROOM: {
    title: "Create Your Secret Nick Room",
    description:
      "Let the holiday magic begin! Set up your gift exchange in just a few steps.",
  },
  WELCOME_ROOM: {
    title: "Welcome to the Secret Squad!",
    description:
      "You've been invited to a cozy holiday gift exchange! Get ready to surprise and be surprised!",
  },
  READY_ROOM: {
    title: "Your Secret Nick Room is Ready!",
    description:
      "Share the link below with up to 20 friends to invite them — and don’t lose your personal link! Let the festive magic begin!",
  },
  JOINED_ROOM: {
    title: "You Have Joined the Room! 🎅",
    description:
      "Your holiday adventure is about to begin! Thank you for joining the Secret Nick room.",
  },
  ADD_DETAILS: {
    title: "Add Your Details",
    description: "Secret Nick needs to know where to send your present!",
  },
  ADD_WISHES: {
    title: "Add Your Wishes",
    description:
      "Let your Secret Nick know what would make you smile this season.",
  },
} as const;

export const ICON_NAMES = {
  STAR: "star",
  CAR: "car",
  PRESENTS: "presents",
  WREATH: "wreath",
  WELCOME_GROUP: "welcome-group",
} as const;

export const FORM_WRAPPER_CONTENT_PROPS = {
  FORM_KEY: "formKey",
  ICON_NAME: "iconName",
  CHILDREN: "children",
  SUB_DESCRIPTION: "subDescription",
  TITLE: "title",
} as const;

export type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];

export interface FormWrapperProps extends HTMLAttributes<HTMLDivElement> {
  title?: string;
  formKey: FormKey;
  subDescription?: ReactNode;
  iconName: IconName;
  buttonProps: ButtonProps;
  isBackButtonVisible?: boolean;
  onBack?: () => void;
  children: ReactNode;
}

export type FormWrapperContentKeys =
  (typeof FORM_WRAPPER_CONTENT_PROPS)[keyof typeof FORM_WRAPPER_CONTENT_PROPS];

export type FormWrapperContentProps = Pick<
  FormWrapperProps,
  FormWrapperContentKeys
>;

export type FormKey = keyof typeof FORM_CONTENT_MAP;
