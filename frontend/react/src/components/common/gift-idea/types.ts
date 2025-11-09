export interface GiftIdeaProps {
  isWishRequired?: boolean;
  giftItem: GiftIdeaField;
  onChange: (field: keyof GiftIdeaField, value: string) => void;
}

export interface GiftIdeaField {
  name: string;
  infoLink: string;
}
