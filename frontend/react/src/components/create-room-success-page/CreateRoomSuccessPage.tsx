import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router";

import FormWrapper from "../common/form-wrapper/FormWrapper";
import RoomLink from "../common/room-link/RoomLink";
import InvitationNote from "../common/invitation-note/InvitationNote";
import type { CreateRoomSuccessPageProps, RoomAndUserData } from "./types";
import { CREATE_ROOM_SUCCESS_PAGE_TITLE } from "./utils";
import { generateRoomLink, generateParticipantLink } from "../../utils/general";
import "@assets/styles/common/room-success-page.scss";

const CreateRoomSuccessPage = ({
  roomAndUserData: roomAndUserDataProp,
}: CreateRoomSuccessPageProps) => {
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    document.title = CREATE_ROOM_SUCCESS_PAGE_TITLE;
  }, []);

  const state = location.state as { roomAndUserData?: RoomAndUserData };
  const roomAndUserData = roomAndUserDataProp ?? state?.roomAndUserData;
  const userCode = roomAndUserData?.userCode;
  const title = `Your ${roomAndUserData?.roomName || "Secret Nick"} Room is Ready!`;

  if (!roomAndUserData) {
    return null;
  }

  const { roomLink, participantLink } = {
    roomLink: generateRoomLink(roomAndUserData.invitationCode),
    participantLink: generateParticipantLink(roomAndUserData.userCode),
  };

  const handleVisitRoom = () => {
    if (userCode) {
      navigate(`/room/${userCode}`);
    }
  };

  return (
    <main className="room-success-page">
      <FormWrapper
        title={title}
        formKey="READY_ROOM"
        iconName="wreath"
        buttonProps={{
          children: "Visit Your Room",
          onClick: handleVisitRoom,
        }}
      >
        <div className="room-success-page__content">
          <RoomLink title="Your Room Link" url={roomLink} />
          <InvitationNote
            value={roomAndUserData.invitationNote}
            invitationLink={roomLink}
            userCode={userCode}
          />
          <RoomLink
            title="Your Personal Participant Link"
            description="This is your unique personal link to access the Secret Nick room."
            url={participantLink}
            caption="Do not share this link with other users"
          />
        </div>
      </FormWrapper>
    </main>
  );
};

export default CreateRoomSuccessPage;
