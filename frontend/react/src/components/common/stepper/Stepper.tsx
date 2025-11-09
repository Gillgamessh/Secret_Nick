import type { StepperProps } from "./types";
import { getStepperItemClass } from "./utils";
import { ICONS_PATH } from "../icon-button/utils";
import "./Stepper.scss";

const Stepper = ({ steps, currentStepIndex, width }: StepperProps) => {
  return (
    <div className="stepper" style={{ width }}>
      {steps.map((step, index) => {
        const isCompletedStep = currentStepIndex > index;
        const isCurrentStep = currentStepIndex === index;
        const stepperItemClass = getStepperItemClass(
          isCompletedStep,
          isCurrentStep,
        );

        return (
          <div key={step} className={`stepper__item ${stepperItemClass}`}>
            {isCompletedStep ? (
              <svg className="stepper__icon">
                <use href={`${ICONS_PATH}check-mark`} />
              </svg>
            ) : null}

            {!isCompletedStep ? (
              <span className="stepper__icon">{index + 1}</span>
            ) : null}

            <span className="stepper__text">{step}</span>
          </div>
        );
      })}
    </div>
  );
};

export default Stepper;
