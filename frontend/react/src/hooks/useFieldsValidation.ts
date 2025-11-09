import type { FieldValidation, ValidationErrors } from "../types/general";

export function useFieldsValidation<T extends string>(
  formFieldsErrors: ValidationErrors<T>,
  setFormFieldError: (field: T, validation: FieldValidation) => void,
) {
  const validateField = <ValueType>(
    field: T,
    validatorFn: (fieldValue: ValueType) => FieldValidation,
    fieldValue: ValueType,
  ) => {
    const validationResult = validatorFn(fieldValue);
    setFormFieldError(field, validationResult);
    return validationResult.isValid;
  };

  const fieldValidations = Object.values(formFieldsErrors) as FieldValidation[];

  const isFieldsValid = fieldValidations.every(
    (validation) => validation.isValid === true,
  );

  return { validateField, isFieldsValid };
}
