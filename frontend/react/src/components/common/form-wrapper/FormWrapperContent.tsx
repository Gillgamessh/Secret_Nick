import type { FormWrapperContentProps } from "./types";
import { getDescriptionWrapperClass, getFormContent } from "./utils";

export const FormWrapperContent = ({
  formKey,
  subDescription,
  iconName,
  children,
  title,
}: FormWrapperContentProps) => {
  const descriptionWrapperClass = `form-wrapper__description-wrapper ${getDescriptionWrapperClass(iconName)}`;
  const { title: defaultTitle, description } = getFormContent(formKey);

  return (
    <div
      className={`form-wrapper__content ${
        iconName === "welcome-group" ? "form-wrapper__content--welcome" : ""
      }`}
    >
      <h3 className="form-wrapper__title">{title || defaultTitle}</h3>
      {description ? (
        <div className={descriptionWrapperClass}>
          <p className="form-wrapper__description">{description}</p>
          {subDescription ? (
            <p className="form-wrapper__description--bold">{subDescription}</p>
          ) : null}
        </div>
      ) : null}
      {children}
    </div>
  );
};
