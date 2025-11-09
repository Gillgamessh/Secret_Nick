import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import FormWrapper from "@components/common/form-wrapper/FormWrapper";
import RoomLink from "@components/common/room-link/RoomLink";
import { generateParticipantLink } from "@utils/general";
import { type JoinRoomSuccessPageProps } from "./types";
import { JOINED_ROOM_SUCCESS_PAGE_TITLE } from "./utils";
import "@assets/styles/common/room-success-page.scss";

const JoinRoomSuccessPage = ({
  userCode: userCodeProp,
}: JoinRoomSuccessPageProps) => {
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    document.title = JOINED_ROOM_SUCCESS_PAGE_TITLE;
  }, []);

  const state = location.state as { userCode?: string };
  const userCode = userCodeProp ?? state?.userCode;

  if (!userCode) {
    return null;
  }

  const participantLink = generateParticipantLink(userCode);

  const navigateToRoom = () => {
    navigate(`/room/${userCode}`);
  };

  return (
    <div className="room-success-page">
      <FormWrapper
        formKey="JOINED_ROOM"
        iconName="wreath"
        buttonProps={{
          children: "Visit Your Room",
          onClick: navigateToRoom,
        }}
      >
        <div className="room-success-page__content">
          <RoomLink
            title="Your Personal Participant Link"
            description="This is your unique personal link to access the Secret Nick room."
            url={participantLink}
            caption="Do not share this link with other users"
          />
        </div>
      </FormWrapper>
    </div>
  );
};
export default JoinRoomSuccessPage;
