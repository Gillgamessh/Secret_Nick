export interface RoomDetailsProps {
  name: string;
  description: string;
  exchangeDate: string;
  giftBudget: number;
  invitationNote: string;
  invitationLink: string;
  roomLink: string;
  withoutInvitationCard?: boolean;
}
