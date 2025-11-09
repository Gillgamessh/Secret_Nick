import {
  useContext,
  type ChangeEvent,
  type KeyboardEvent,
  type FocusEvent,
} from "react";
import { DatePicker, type DatePickerProps } from "antd";

import FormWrapper from "../../common/form-wrapper/FormWrapper";
import Input from "../../common/input/Input";
import { useFieldsValidation } from "@hooks/useFieldsValidation";
import {
  CreateRoomFormInputNames,
  type DateType,
  type FormData,
} from "./types";
import {
  LABEL_DATE_PICKER,
  INPUT_ID_DATE_PICKER,
  isValidInputField,
  requiredFields,
} from "./utils";
import {
  blockInvalidNumberKeys,
  giftBudgetLimitValidator,
  isRequiredFieldsFilled,
} from "../../../utils/validation";

import styles from "../../common/input/Input.module.scss";
import { FormsContext } from "../../../contexts/forms-context/FormsContext";
import "./CreateRoomForm.scss";

const CreateRoomForm = () => {
  const {
    onNextStep,
    roomData,
    setRoomData,
    getCreateRoomFormFieldsErrors,
    setFormFieldError,
  } = useContext(FormsContext);

  const { name, description, giftExchangeDate, giftMaximumBudget } =
    roomData.room;

  const createRoomFormFieldsErrors = getCreateRoomFormFieldsErrors();

  const { validateField, isFieldsValid } = useFieldsValidation(
    createRoomFormFieldsErrors,
    (field, validation) => setFormFieldError(field, validation),
  );

  const { giftMaximumBudget: giftBudgetLimitErrorField } =
    createRoomFormFieldsErrors;

  const gitfBudgetCaption =
    giftBudgetLimitErrorField.errorMessage || "0 means unlimited budget";

  const handleBlur = (
    e: FocusEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;

    if (name === CreateRoomFormInputNames.GIFT_BUDGET) {
      validateField(
        CreateRoomFormInputNames.GIFT_BUDGET,
        giftBudgetLimitValidator,
        Number(value),
      );
    }
  };

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;

    if (!isValidInputField(name, value)) return;

    setRoomData((prev) => ({
      ...prev,
      room: {
        ...prev.room,
        [name]: value,
      },
    }));
  };

  const handleDatePickerChange: DatePickerProps["onChange"] = (date) => {
    setRoomData((prev) => ({
      ...prev,
      room: {
        ...prev.room,
        giftExchangeDate: date,
      },
    }));
  };

  const handleBudgetKeyDown = (
    e: KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    blockInvalidNumberKeys(e);
  };

  const disablePastDates = (current: DateType) => {
    if (!current) {
      return false;
    }

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const currentDate = current.toDate();
    currentDate.setHours(0, 0, 0, 0);

    return currentDate < today;
  };

  const isFormValid =
    isRequiredFieldsFilled<FormData>(roomData?.room, requiredFields) &&
    isFieldsValid;

  return (
    <FormWrapper
      formKey="CREATE_ROOM"
      iconName="star"
      buttonProps={{
        children: "Continue",
        disabled: !isFormValid,
        onClick: onNextStep,
      }}
    >
      <div className="content">
        <Input
          placeholder="Enter your room name"
          label="Room name"
          name={CreateRoomFormInputNames.ROOM_NAME}
          required
          value={name}
          onChange={handleChange}
        />

        <Input
          placeholder="Enter your message here..."
          label="Room description"
          name={CreateRoomFormInputNames.ROOM_DESCRIPTION}
          multiline
          maxLength={200}
          required
          value={description}
          onChange={handleChange}
        />

        <div className="content__group">
          <div className={styles.inputWrapper}>
            <label
              htmlFor={INPUT_ID_DATE_PICKER}
              className={`${styles["inputWrapper__label"]} ${styles["inputWrapper__label--required"]}`}
            >
              {LABEL_DATE_PICKER}
            </label>
            <DatePicker
              id={INPUT_ID_DATE_PICKER}
              format="DD-MM-YYYY"
              disabledDate={disablePastDates}
              style={{
                width: "338px",
              }}
              onChange={handleDatePickerChange}
              value={giftExchangeDate}
              className={giftExchangeDate ? "ant-picker--filled" : ""}
            />
          </div>

          <Input
            type="number"
            placeholder="Type in your budget"
            caption={gitfBudgetCaption}
            label="Gift budget"
            name={CreateRoomFormInputNames.GIFT_BUDGET}
            width="338px"
            required
            withoutCounter
            withSuffix
            value={giftMaximumBudget}
            hasError={giftBudgetLimitErrorField.isValid === false}
            onChange={handleChange}
            onKeyDown={handleBudgetKeyDown}
            onBlur={handleBlur}
          />
        </div>
      </div>
    </FormWrapper>
  );
};

export default CreateRoomForm;
