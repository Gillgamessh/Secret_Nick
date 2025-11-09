export const getStepperItemClass = (
  isCompleted: boolean,
  isCurrent: boolean,
) => {
  if (isCompleted) {
    return "stepper__item--completed";
  } else if (isCurrent) {
    return "stepper__item--current";
  }

  return "";
};
