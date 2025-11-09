import type { PersonalInformationProps } from "@components/common/personal-information/types";

export interface ParticipantDetailsModalProps {
  isOpen?: boolean;
  onClose: () => void;
  personalInfoData: PersonalInformationProps;
}
