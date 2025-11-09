import RoomLink from "../room-link/RoomLink";
import Button from "../button/Button";
import type { ParticipantInfoProps } from "./types";
import "./ParticipantInfo.scss";

const ParticipantInfo = ({
  participantName,
  roomName,
  participantLink,
  onViewInformation,
}: ParticipantInfoProps) => {
  const roomDescription = `Get ready for happy gift exchange and fun in ${roomName || "Secret Nick"} game!`;

  return (
    <div className="participant-info">
      <div>
        <h3 className="participant-info__title">
          Hi, {participantName || "Participant"}
        </h3>
        <p className="participant-info__description">{roomDescription}</p>
      </div>

      <div>
        {participantLink ? (
          <RoomLink
            description="Your Participant Link"
            caption="Do not share this link with other users"
            url={participantLink}
            small
            white
          />
        ) : null}

        <div className="participant-info__button">
          <Button variant="secondary" size="medium" onClick={onViewInformation}>
            View Information
          </Button>
        </div>
      </div>
    </div>
  );
};

export default ParticipantInfo;
