import PersonalInformation from "@components/common/personal-information/PersonalInformation";
import Modal from "../modal/Modal";
import type { PersonalInformationModalProps } from "./types";

const PersonalInformationModal = ({
  isOpen = false,
  onClose,
  personalInfoData,
}: PersonalInformationModalProps) => {
  return (
    <Modal
      title="Personal Information"
      description="Secret Nick needs to know where to send your present!"
      iconName="car"
      onClose={onClose}
      onConfirm={onClose}
      isOpen={isOpen}
    >
      <PersonalInformation
        firstName={personalInfoData.firstName}
        lastName={personalInfoData.lastName}
        phone={personalInfoData.phone}
        email={personalInfoData.email}
        deliveryInfo={personalInfoData.deliveryInfo}
        withoutHeader
      />
    </Modal>
  );
};

export default PersonalInformationModal;
