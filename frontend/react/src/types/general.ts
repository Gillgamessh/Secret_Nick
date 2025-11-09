import type { ChangeEvent } from "react";
import { DetailsFormInputNames } from "../components/common/details-form/types";
import { CreateRoomFormInputNames } from "../components/create-room-page/create-room-form/types";

export type InputChangeEvent = ChangeEvent<
  HTMLInputElement | HTMLTextAreaElement
>;

export type InputChangeHandler = (e: InputChangeEvent) => void;

export type FieldValidation = {
  isValid: boolean | null;
  errorMessage: string;
};

export type ValidationErrors<T extends string> = Record<T, FieldValidation>;

export const InputNames = {
  ...DetailsFormInputNames,
  ...CreateRoomFormInputNames,
} as const;

export type InputName = (typeof InputNames)[keyof typeof InputNames];
