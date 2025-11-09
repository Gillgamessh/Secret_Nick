import { useContext } from "react";
import Button from "../button/Button";
import FormWrapper from "../form-wrapper/FormWrapper";
import GiftIdea from "../gift-idea/GiftIdea";
import Input from "../input/Input";

import type { GiftIdeaField } from "../gift-idea/types";
import { RadioButtonItem } from "../radio-button-item/RadioButtonItem";
import "./WishesForm.scss";
import { checkValidation } from "./utils";
import type { InputChangeEvent } from "../../../types/general";

import { GiftTypeValue, type GiftType, type WishesFormProps } from "./types";
import { FormsContext } from "../../../contexts/forms-context/FormsContext";

const WishesForm = ({ budget, onBack, onComplete }: WishesFormProps) => {
  const { roomData, setRoomData } = useContext(FormsContext);
  const { wantSurprise, interests, wishList } = roomData.user;

  const giftType = wantSurprise ? GiftTypeValue.SURPRISE : GiftTypeValue.IDEAS;

  const isFormValid = checkValidation(giftType, interests, wishList);

  const handleChangeValue = (
    id: number,
    field: keyof GiftIdeaField,
    value: string,
  ) => {
    setRoomData((prev) => ({
      ...prev,
      user: {
        ...prev.user,
        wishList: prev.user.wishList.map((idea) =>
          idea.id === id ? { ...idea, [field]: value } : idea,
        ),
      },
    }));
  };

  const handleFormAddWish = () => {
    const id = Date.now();

    setRoomData((prev) => ({
      ...prev,
      user: {
        ...prev.user,
        wishList: [...prev.user.wishList, { id, name: "", infoLink: "" }],
      },
    }));
  };

  const handleChangeGiftType = (giftType: GiftType) => {
    setRoomData((prev) => ({
      ...prev,
      user: {
        ...prev.user,
        wantSurprise: giftType === GiftTypeValue.SURPRISE,
      },
    }));
  };

  const handleChangeSurpriseText = (text: string) => {
    setRoomData((prev) => ({
      ...prev,
      user: {
        ...prev.user,
        interests: text,
      },
    }));
  };

  return (
    <FormWrapper
      formKey="ADD_WISHES"
      iconName="presents"
      subDescription={
        budget &&
        (budget === "0"
          ? "Gift Budget: Unlimited"
          : `Gift Budget: ${budget} UAH`)
      }
      buttonProps={{
        children: "Complete",
        disabled: !isFormValid,
        onClick: onComplete,
      }}
      isBackButtonVisible
      onBack={onBack}
    >
      <div className="wishes-form-content">
        <RadioButtonItem
          name="giftType"
          text="I have gift ideas! (add up to 5 gift ideas)"
          selected={giftType === GiftTypeValue.IDEAS}
          onChange={() => handleChangeGiftType(GiftTypeValue.IDEAS)}
        >
          {giftType === GiftTypeValue.IDEAS ? (
            <div className="wishes-form-content__gifts">
              {wishList.map((wish, index) => (
                <GiftIdea
                  key={wish.id}
                  isWishRequired={index === 0}
                  giftItem={wish}
                  onChange={(field, value) =>
                    handleChangeValue(wish.id, field, value)
                  }
                />
              ))}

              {wishList.length < 5 ? (
                <div className="wishes-form-content__button-place">
                  <Button
                    type="button"
                    variant="tertiary"
                    size="small"
                    iconName="plus-outlined"
                    onClick={handleFormAddWish}
                    width={154}
                  >
                    Add Wish
                  </Button>
                </div>
              ) : null}
            </div>
          ) : null}
        </RadioButtonItem>

        <RadioButtonItem
          name="giftType"
          text="I want a surprise gift"
          selected={giftType === GiftTypeValue.SURPRISE}
          onChange={() => handleChangeGiftType(GiftTypeValue.SURPRISE)}
        >
          {giftType === GiftTypeValue.SURPRISE ? (
            <div className="wishes-form-content__surprise">
              <Input
                value={interests}
                onChange={(e: InputChangeEvent) =>
                  handleChangeSurpriseText(e.target.value)
                }
                placeholder="e.g. reading, coffee, cozy socks"
                label="Add your interests"
                maxLength={1000}
                required
                multiline
              />
            </div>
          ) : null}
        </RadioButtonItem>
      </div>
    </FormWrapper>
  );
};

export default WishesForm;
