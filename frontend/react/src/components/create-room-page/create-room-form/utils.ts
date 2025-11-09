import { CreateRoomFormInputNames } from "./types";
import type { FormData, CreateRoomFormInputName } from "./types";
import { validateNonNegativeNumber } from "../../../utils/validation";

export const LABEL_DATE_PICKER = "Gift Exchange date";
export const INPUT_ID_DATE_PICKER = "input-gift-exchange-date";
export const GIFT_BUDGET_LIMIT = 100_000;

export const requiredFields: (keyof FormData)[] = [
  ...Object.values(CreateRoomFormInputNames),
  "giftExchangeDate",
];

export const FieldValidators: Record<
  CreateRoomFormInputName,
  (value: string) => boolean
> = {
  [CreateRoomFormInputNames.ROOM_NAME]: () => true,
  [CreateRoomFormInputNames.ROOM_DESCRIPTION]: () => true,
  [CreateRoomFormInputNames.GIFT_BUDGET]: validateNonNegativeNumber,
};

export const isValidInputField = (name: string, value: string): boolean => {
  if (
    !Object.values(CreateRoomFormInputNames).includes(
      name as CreateRoomFormInputName,
    )
  )
    return false;

  if (name === CreateRoomFormInputNames.GIFT_BUDGET) {
    return validateNonNegativeNumber(value);
  }

  return true;
};
