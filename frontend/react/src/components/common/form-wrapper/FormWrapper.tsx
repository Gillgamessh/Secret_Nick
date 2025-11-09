import Button from "../button/Button";
import { FormWrapperContent } from "./FormWrapperContent";
import type { FormWrapperProps } from "./types";
import "./FormWrapper.scss";

const FormWrapper = ({
  formKey,
  subDescription,
  iconName,
  buttonProps,
  isBackButtonVisible = false,
  onBack,
  children,
  title,
}: FormWrapperProps) => {
  const backButton = isBackButtonVisible ? (
    <Button
      variant="text"
      size="medium"
      iconName="back-arrow"
      onClick={onBack}
      type="button"
    >
      Back to the previous step
    </Button>
  ) : null;

  const welcomeGroupIcons =
    iconName === "welcome-group" ? (
      <div className="form-wrapper__form-extra-icon" />
    ) : null;

  return (
    <div className="form-wrapper">
      <div className={`form-wrapper__form form-wrapper__form--${iconName}`}>
        {welcomeGroupIcons}
        <FormWrapperContent
          title={title}
          formKey={formKey}
          subDescription={subDescription}
          iconName={iconName}
        >
          {children}
        </FormWrapperContent>
        <div className="form-wrapper__actions">
          <Button
            variant="primary"
            size="medium"
            type="button"
            {...buttonProps}
          />
          {backButton}
        </div>
      </div>
    </div>
  );
};

export default FormWrapper;
