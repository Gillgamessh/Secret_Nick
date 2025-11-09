import Modal from "../modal/Modal";
import RoomLink from "@components/common/room-link/RoomLink";
import InvitationNote from "@components/common/invitation-note/InvitationNote";
import type { InvitationModalProps } from "./types";
import "./InvitationModal.scss";

const InvitationModal = ({
  invitationNote,
  roomLink,
  invitationLink,
  onClose,
  isOpen,
}: InvitationModalProps) => {
  return (
    <Modal
      title="Invite New Members"
      description="Share the link below with 20 friends to invite them"
      iconName="note"
      onClose={onClose}
      onConfirm={onClose}
      isOpen={isOpen}
    >
      <div className="invitation-modal-content">
        <RoomLink title="Your Room Link" url={roomLink} />
        <InvitationNote
          value={invitationNote}
          invitationLink={invitationLink}
        />
      </div>
    </Modal>
  );
};

export default InvitationModal;
