export type WishListItem = {
  name?: string;
  infoLink?: string;
};

export type Participant = {
  id: number;
  createdOn: string;
  modifiedOn: string;
  roomId: number;
  isAdmin: boolean;
  userCode: string;
  firstName: string;
  lastName: string;
  phone: string;
  email?: string;
  giftToUserId?: number;
  deliveryInfo: string;
  wantSurprise: boolean;
  interests?: string;
  wishList?: WishList;
};

export type GetRoomResponse = {
  id: number;
  createdOn: string;
  modifiedOn: string;
  closedOn?: string;
  adminId: number;
  invitationCode: string;
  invitationLink: string;
  name: string;
  description: string;
  invitationNote: string;
  giftExchangeDate: string;
  giftMaximumBudget: number;
  isFull: boolean;
};

export type GetParticipantsResponse = Participant[];

export type WishList = WishListItem[];

export type DrawRoomResponse = {
  adminGiftToUser: Participant;
};

export type UpdateRoomResponse = Omit<GetRoomResponse, "isFull">;

export type UpdateRoomRequest = Partial<
  Pick<
    GetRoomResponse,
    | "name"
    | "description"
    | "invitationNote"
    | "giftExchangeDate"
    | "giftMaximumBudget"
  >
>;
