import { useContext, useEffect } from "react";
import { useNavigate } from "react-router";

import {
  defaultRoomData,
  FormsContext,
} from "../../contexts/forms-context/FormsContext";
import RoomFormWizard from "../common/room-form-wizard/RoomFormWizard";

import useToaster from "@hooks/useToaster";
import { useFetch } from "@hooks/useFetch";
import { BASE_API_URL } from "@utils/general";
import { CREATE_ROOM_PAGE_TITLE } from "./utils";
import type { CreateRoomRequest, CreateRoomResponse } from "./types";
import "@assets/styles/common/forms-room-page.scss";

const CreateRoomPage = () => {
  const { setRoomData, getCreateRoomData } = useContext(FormsContext);
  const { showToast } = useToaster();
  const navigate = useNavigate();

  const { isLoading, fetchData } = useFetch<
    CreateRoomResponse,
    CreateRoomRequest
  >(
    {
      url: `${BASE_API_URL}/api/rooms`,
      method: "POST",
      headers: { "Content-Type": "application/json" },
      onError: () => {
        showToast("Something went wrong. Try again.", "error", "large");
      },
      onSuccess: (response: CreateRoomResponse) => {
        setRoomData(defaultRoomData);
        navigate("/create-room/success", {
          state: {
            roomAndUserData: {
              invitationCode: response?.room?.invitationCode,
              invitationNote: response?.room?.invitationNote,
              userCode: response?.userCode,
              roomName: response?.room?.name,
            },
          },
        });
      },
    },
    false,
  );

  const handleCreateRoom = () => {
    const formData = getCreateRoomData();

    if (!formData) return;

    const wantSurprise = formData?.adminUser?.wantSurprise;

    const preparedData = {
      ...formData,
      room: {
        ...formData.room,
        giftExchangeDate:
          formData?.room?.giftExchangeDate?.format("YYYY-MM-DD") ?? null,
        giftMaximumBudget: +formData?.room?.giftMaximumBudget,
      },
      adminUser: {
        ...formData.adminUser,
        phone: `+380${formData?.adminUser?.phone}`,
        interests: wantSurprise ? formData?.adminUser?.interests : undefined,
        wishList: !wantSurprise ? formData?.adminUser?.wishList : undefined,
      },
    };

    fetchData(preparedData);
  };

  useEffect(() => {
    document.title = CREATE_ROOM_PAGE_TITLE;
  }, []);

  return (
    <main className="forms-room-page">
      <div className="forms-room-page__content">
        <RoomFormWizard
          onComplete={handleCreateRoom}
          isRequestLoading={isLoading}
          isAdmin
        />
      </div>
    </main>
  );
};

export default CreateRoomPage;
