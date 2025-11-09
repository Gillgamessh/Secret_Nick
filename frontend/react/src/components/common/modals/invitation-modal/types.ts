export interface InvitationModalProps {
  invitationNote: string;
  roomLink: string;
  invitationLink: string;
  isOpen?: boolean;
  onClose: () => void;
}
