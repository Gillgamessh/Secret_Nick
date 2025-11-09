import { type ChangeEvent } from "react";
import Input from "../input/Input";
import type { GiftIdeaField, GiftIdeaProps } from "./types";
import "./GiftIdea.scss";

const initialGiftIdea = {
  name: "",
  infoLink: "",
} as GiftIdeaField;

const GiftIdea = ({
  giftItem = initialGiftIdea,
  isWishRequired = false,
  onChange,
}: GiftIdeaProps) => {
  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;
    onChange?.(name as keyof GiftIdeaField, value);
  };

  return (
    <div className="gift-idea">
      <Input
        placeholder="Enter your wish name"
        value={giftItem.name}
        onChange={handleChange}
        label="I wish for"
        required={isWishRequired}
        name="name"
      />

      <Input
        placeholder="E.g. https://example.com/item"
        value={giftItem.infoLink}
        onChange={handleChange}
        label="Add link"
        maxLength={undefined}
        withoutCounter
        name="infoLink"
      />
    </div>
  );
};

export default GiftIdea;
