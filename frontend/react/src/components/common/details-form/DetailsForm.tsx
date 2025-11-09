import { useContext, type FocusEvent } from "react";

import PhoneNumberInput from "../phone-number-input/PhoneNumberInput";
import FormWrapper from "../form-wrapper/FormWrapper";
import Input from "../input/Input";

import {
  DetailsFormInputNames,
  type FormData,
  type DetailsFormProps,
  requiredFields,
} from "./types";
import type {
  InputChangeEvent,
  InputChangeHandler,
} from "../../../types/general";
import {
  isRequiredFieldsFilled,
  phoneValidator,
  emailValidator,
} from "../../../../src/utils/validation";
import { useFieldsValidation } from "../../../../src/hooks/useFieldsValidation";
import { FormsContext } from "../../../contexts/forms-context/FormsContext";

import "./DetailsForm.scss";

const DetailsForm = ({ onBack }: DetailsFormProps) => {
  const {
    onNextStep,
    roomData,
    setRoomData,
    getDetailsFormFieldsErrors,
    setFormFieldError,
  } = useContext(FormsContext);

  const { firstName, lastName, phone, email, deliveryInfo } = roomData.user;

  const detailsFormFieldsErrors = getDetailsFormFieldsErrors();

  const { validateField, isFieldsValid } = useFieldsValidation(
    detailsFormFieldsErrors,
    (field, validation) => setFormFieldError(field, validation),
  );

  const handleBlur = (
    e: FocusEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;

    if (name === DetailsFormInputNames.PHONE) {
      validateField(DetailsFormInputNames.PHONE, phoneValidator, value);
    }

    if (name === DetailsFormInputNames.EMAIL) {
      validateField(DetailsFormInputNames.EMAIL, emailValidator, value);
    }
  };

  const handleChange: InputChangeHandler = (e: InputChangeEvent) => {
    const { name, value } = e.target;

    setRoomData((prev) => ({
      ...prev,
      user: {
        ...prev.user,
        [name]: value,
      },
    }));
  };

  const isValidForm =
    isRequiredFieldsFilled<FormData>(roomData.user, requiredFields) &&
    isFieldsValid;

  return (
    <FormWrapper
      iconName="car"
      formKey="ADD_DETAILS"
      buttonProps={{
        children: "Continue",
        disabled: !isValidForm,
        onClick: onNextStep,
      }}
      isBackButtonVisible={!!onBack}
      onBack={onBack}
    >
      <div className="details-form-content">
        <Input
          placeholder="e.g. Nickolas"
          label="First name"
          value={firstName}
          required
          onChange={handleChange}
          width="338px"
          name={DetailsFormInputNames.FIRST_NAME}
        />

        <Input
          placeholder="e.g. Secret"
          label="Last name"
          value={lastName}
          required
          onChange={handleChange}
          width="338px"
          name={DetailsFormInputNames.LAST_NAME}
        />

        <PhoneNumberInput
          value={phone}
          required
          onChange={handleChange}
          onBlur={handleBlur}
          name={DetailsFormInputNames.PHONE}
          hasError={detailsFormFieldsErrors.phone.isValid === false}
          caption={detailsFormFieldsErrors.phone.errorMessage}
        />

        <Input
          placeholder="nickolas@example.com"
          label="Email"
          value={email}
          onChange={handleChange}
          onBlur={handleBlur}
          width="338px"
          name={DetailsFormInputNames.EMAIL}
          withoutCounter
          type="email"
          hasError={detailsFormFieldsErrors.email.isValid === false}
          caption={detailsFormFieldsErrors.email.errorMessage}
        />

        <Input
          placeholder="Where should St. Nick deliver your gift?"
          label="Your delivery address (no North Pole required!)"
          value={deliveryInfo}
          onChange={handleChange}
          multiline
          maxLength={500}
          required
          name={DetailsFormInputNames.DELIVERY_INFO}
        />
      </div>
    </FormWrapper>
  );
};

export default DetailsForm;
