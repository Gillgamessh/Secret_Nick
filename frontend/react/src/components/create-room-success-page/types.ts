export type RoomAndUserData = {
  invitationCode: string;
  invitationNote: string;
  userCode: string;
  roomName: string;
};

export interface CreateRoomSuccessPageProps {
  roomAndUserData?: RoomAndUserData;
}
