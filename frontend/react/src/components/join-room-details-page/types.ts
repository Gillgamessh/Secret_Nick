export interface JoinRoomDetailsRequest {
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
}

export interface JoinRoomDetailsResponse {
  id: number;
  createdOn: string;
  modifiedOn: string;
  roomId: number;
  isAdmin: boolean;
  userCode: string;
  userLink: string;
  firstName: string;
  lastName: string;
  phone: string;
  email: string;
  deliveryInfo: string;
  wantSurprise: boolean;
  interests: string;
  wishList: {
    name: string;
    infoLink: string | null;
  }[];
}
