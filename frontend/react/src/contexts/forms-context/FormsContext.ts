import { createContext } from "react";
import type { FormContextType } from "./types";
import type { ValidationErrors } from "../../types/general";
import {
  DetailsFormInputNames,
  type DetailsFormInputName,
} from "../../components/common/details-form/types";
import {
  CreateRoomFormInputNames,
  type CreateRoomFormInputName,
} from "@components/create-room-page/create-room-form/types";
import { OPTIONAL_FIELDS } from "./utils";

const roomData = {
  name: "",
  description: "",
  giftExchangeDate: null,
  giftMaximumBudget: "",
};

const defaultUserData = {
  firstName: "",
  lastName: "",
  phone: "",
  email: "",
  deliveryInfo: "",
  wantSurprise: false,
  interests: "",
  wishList: [
    {
      id: Date.now(),
      name: "",
      infoLink: "",
    },
  ],
};

export const defaultRoomData = {
  room: {
    ...roomData,
  },
  user: {
    ...defaultUserData,
  },
};

export const defaultCreateRoomData = {
  room: {
    ...roomData,
  },
  adminUser: {
    ...defaultUserData,
  },
};

const defaultDetailsFormFieldsErrors = Object.fromEntries(
  Object.values(DetailsFormInputNames).map((field) => [
    field,
    {
      isValid: OPTIONAL_FIELDS?.includes(field) ? true : null,
      errorMessage: "",
    },
  ]),
) as ValidationErrors<DetailsFormInputName>;

const defaultCreateRoomFormFieldsErrors = Object.fromEntries(
  Object.values(CreateRoomFormInputNames).map((field) => [
    field,
    { isValid: null, errorMessage: "" },
  ]),
) as ValidationErrors<CreateRoomFormInputName>;

export const defaultContext: FormContextType = {
  currentStep: 0,
  onNextStep: () => {},
  onPreviousStep: () => {},
  roomData: defaultRoomData,
  setRoomData: () => {},
  getCreateRoomData: () => defaultCreateRoomData,
  getJoinRoomDetailsData: () => defaultUserData,
  setFormFieldError: () => {},
  getDetailsFormFieldsErrors: () => defaultDetailsFormFieldsErrors,
  getCreateRoomFormFieldsErrors: () => defaultCreateRoomFormFieldsErrors,
};

export const FormsContext = createContext<FormContextType>(defaultContext);
