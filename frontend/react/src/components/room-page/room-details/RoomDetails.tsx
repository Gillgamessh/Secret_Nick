import { useState } from "react";
import InfoCard from "@components/common/info-card/InfoCard";
import Button from "@components/common/button/Button";
import InvitationModal from "@components/common/modals/invitation-modal/InvitationModal";
import { formatBudget, formatDate } from "@utils/general";
import type { RoomDetailsProps } from "./types";
import "./RoomDetails.scss";

const RoomDetails = ({
  name,
  description,
  exchangeDate,
  giftBudget,
  invitationNote,
  invitationLink,
  roomLink,
  withoutInvitationCard = false,
}: RoomDetailsProps) => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <div className="room-details">
      <div className="room-details__content">
        <h2>{name}</h2>
        <p className="room-details__description">{description}</p>
      </div>

      <div className="room-details__cards-container">
        <InfoCard
          title="Exchange Date"
          description={formatDate(exchangeDate)}
          iconName="star"
          variant="white"
        />

        <InfoCard
          title="Gift Budget"
          description={formatBudget(giftBudget)}
          iconName="presents"
          variant="white"
        />

        {!withoutInvitationCard ? (
          <InfoCard title="Invitation Note" iconName="note" variant="white">
            <Button
              size="small"
              variant="secondary"
              onClick={() => setIsModalOpen(true)}
            >
              Invite New Members
            </Button>

            <InvitationModal
              isOpen={isModalOpen}
              onClose={() => setIsModalOpen(false)}
              invitationNote={invitationNote}
              roomLink={roomLink}
              invitationLink={invitationLink}
            />
          </InfoCard>
        ) : null}
      </div>
    </div>
  );
};

export default RoomDetails;
