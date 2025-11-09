import {
  validateNonNegativeNumber,
  INVALID_NUMBER_KEYS,
} from "../../../utils/validation";

export const isValidPhoneNumber = (phoneNumberValue: string) => {
  const hasInvalidCharacters = INVALID_NUMBER_KEYS.some((invalidChar) =>
    phoneNumberValue.includes(invalidChar),
  );

  return (
    !hasInvalidCharacters &&
    validateNonNegativeNumber(phoneNumberValue) &&
    phoneNumberValue.length <= 9
  );
};
