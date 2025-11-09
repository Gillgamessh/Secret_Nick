import type { DatePickerProps } from "antd";

export const CreateRoomFormInputNames = {
  ROOM_NAME: "name",
  ROOM_DESCRIPTION: "description",
  GIFT_BUDGET: "giftMaximumBudget",
} as const;

export type CreateRoomFormInputName =
  (typeof CreateRoomFormInputNames)[keyof typeof CreateRoomFormInputNames];

type DatePickerOnChange = NonNullable<DatePickerProps["onChange"]>;
export type DateType = Parameters<DatePickerOnChange>[0];

export interface FormData {
  name: string;
  description: string;
  giftMaximumBudget: string;
  giftExchangeDate: DateType | null;
}
