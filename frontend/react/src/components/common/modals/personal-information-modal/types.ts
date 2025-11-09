import type { PersonalInformationProps } from "@components/common/personal-information/types";

export interface PersonalInformationModalProps {
  isOpen?: boolean;
  onClose: () => void;
  personalInfoData: PersonalInformationProps;
}
