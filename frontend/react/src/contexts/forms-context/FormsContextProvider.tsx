import { useState, type ReactNode } from "react";
import { defaultRoomData, FormsContext } from "./FormsContext";
import { removeIdFromArray } from "@utils/general";
import { type RoomData } from "./types";
import {
  InputNames,
  type FieldValidation,
  type InputName,
  type ValidationErrors,
} from "@types/general";
import type { DetailsFormInputName } from "../../components/common/details-form/types";
import {
  CREATE_ROOM_FORM_VALIDATION_KEYS,
  DETAILS_FORM_VALIDATION_KEYS,
  OPTIONAL_FIELDS,
} from "./utils";
import type { CreateRoomFormInputName } from "@components/create-room-page/create-room-form/types";

export const FormsContextProvider = ({ children }: { children: ReactNode }) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [roomData, setRoomData] = useState<RoomData>(defaultRoomData);

  const [formFieldsErrors, setFormFieldsErrors] = useState<
    ValidationErrors<InputName>
  >(
    Object.fromEntries(
      Object.values(InputNames).map((field) => [
        field,
        {
          isValid: OPTIONAL_FIELDS?.includes(field) ? true : null,
          errorMessage: "",
        },
      ]),
    ) as ValidationErrors<InputName>,
  );

  const setFormFieldError = (
    field: (typeof InputNames)[keyof typeof InputNames],
    validation: FieldValidation,
  ) => {
    setFormFieldsErrors((prev: typeof formFieldsErrors) => ({
      ...prev,
      [field]: validation,
    }));
  };

  const getDetailsFormFieldsErrors = () => {
    return Object.fromEntries(
      DETAILS_FORM_VALIDATION_KEYS.map((field) => [
        field,
        formFieldsErrors[field],
      ]),
    ) as ValidationErrors<DetailsFormInputName>;
  };

  const getCreateRoomFormFieldsErrors = () => {
    return Object.fromEntries(
      CREATE_ROOM_FORM_VALIDATION_KEYS.map((field) => [
        field,
        formFieldsErrors[field],
      ]),
    ) as ValidationErrors<CreateRoomFormInputName>;
  };

  const getCreateRoomData = () => {
    return {
      room: {
        ...roomData.room,
      },
      adminUser: {
        ...roomData.user,
        wishList: removeIdFromArray(roomData.user.wishList),
      },
    };
  };

  const getJoinRoomDetailsData = () => {
    return {
      ...roomData.user,
      wishList: removeIdFromArray(roomData.user.wishList),
    };
  };

  const onNextStep = () => {
    setCurrentStep((prevCurrentStep) => prevCurrentStep + 1);
  };

  const onPreviousStep = () => {
    setCurrentStep((prevCurrentStep) => prevCurrentStep - 1);
  };

  return (
    <FormsContext.Provider
      value={{
        currentStep,
        onNextStep,
        onPreviousStep,
        roomData,
        setRoomData,
        getCreateRoomData,
        getJoinRoomDetailsData,
        setFormFieldError,
        getDetailsFormFieldsErrors,
        getCreateRoomFormFieldsErrors,
      }}
    >
      {children}
    </FormsContext.Provider>
  );
};
