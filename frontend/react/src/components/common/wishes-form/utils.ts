import { GiftTypeValue, type GiftIdeaType } from "./types";

export const checkValidation = (
  giftType: string,
  customText: string,
  ideas: GiftIdeaType[],
): boolean => {
  if (giftType === GiftTypeValue.SURPRISE) {
    return customText.trim().length > 0;
  } else {
    return ideas[0].name.trim() !== "";
  }
};
