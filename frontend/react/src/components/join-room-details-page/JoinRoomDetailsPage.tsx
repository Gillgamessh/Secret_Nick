import { useContext, useEffect } from "react";
import { useNavigate, useParams } from "react-router";

import {
  defaultRoomData,
  FormsContext,
} from "../../contexts/forms-context/FormsContext";
import RoomFormWizard from "@components/common/room-form-wizard/RoomFormWizard";

import useToaster from "@hooks/useToaster";
import { useFetch } from "@hooks/useFetch";
import { BASE_API_URL } from "@utils/general";
import { JOIN_ROOM_DETAILS_PAGE_TITLE } from "./utils";
import type { JoinRoomDetailsRequest, JoinRoomDetailsResponse } from "./types";
import "@assets/styles/common/forms-room-page.scss";

const JoinRoomDetailsPage = () => {
  const { setRoomData, getJoinRoomDetailsData } = useContext(FormsContext);
  const { showToast } = useToaster();
  const { roomCode } = useParams();
  const navigate = useNavigate();

  const { isLoading, fetchData } = useFetch<
    JoinRoomDetailsResponse,
    JoinRoomDetailsRequest
  >(
    {
      url: `${BASE_API_URL}/api/users?roomCode=${roomCode}`,
      method: "POST",
      headers: { "Content-Type": "application/json" },
      onError: () => {
        showToast("Something went wrong. Try again.", "error", "large");
      },
      onSuccess: (response: JoinRoomDetailsResponse) => {
        setRoomData(defaultRoomData);
        navigate(`/join/${roomCode}/success`, {
          state: {
            userCode: response?.userCode,
          },
        });
      },
    },
    false,
  );

  const handleJoinRoom = () => {
    const formData = getJoinRoomDetailsData();

    if (!formData) return;

    const wantSurprise = formData?.wantSurprise;

    const preparedData = {
      ...formData,
      phone: `+380${formData?.phone}`,
      interests: wantSurprise ? formData?.interests : undefined,
      wishList: !wantSurprise ? formData?.wishList : undefined,
    };

    fetchData(preparedData);
  };

  useEffect(() => {
    document.title = JOIN_ROOM_DETAILS_PAGE_TITLE;
  }, []);

  return (
    <main className="forms-room-page">
      <div className="forms-room-page__content">
        <RoomFormWizard
          onComplete={handleJoinRoom}
          isRequestLoading={isLoading}
        />
      </div>
    </main>
  );
};

export default JoinRoomDetailsPage;
