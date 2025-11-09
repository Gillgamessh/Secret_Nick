export interface CreateRoomRequest {
  room: {
    name: string;
    description: string;
    giftExchangeDate: string | null;
    giftMaximumBudget: number;
  };
  adminUser: {
    firstName: string;
    lastName: string;
    phone: string;
    email?: string;
    deliveryInfo: string;
    wantSurprise: boolean;
    interests?: string;
    wishList?: {
      name: string;
      infoLink?: string | null;
    }[];
  };
}

export interface CreateRoomResponse {
  room: {
    id: number;
    createdOn: string;
    modifiedOn: string;
    adminId: number;
    invitationCode: string;
    invitationLink: string;
    name: string;
    description: string;
    invitationNote: string;
    giftExchangeDate: string;
    giftMaximumBudget: number;
  };
  userCode: string;
  userLink: string;
}
