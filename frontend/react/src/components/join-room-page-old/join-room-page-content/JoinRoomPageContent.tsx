import JoinRoomSprite from "@assets/images/icons/join-room-sprite.svg";
import FormWrapper from "@components/common/form-wrapper/FormWrapper";
import InfoCard from "@components/common/info-card/InfoCard";
import { formatBudget, formatDate } from "@utils/general";
import type { JoinRoomContentProps } from "./types";
import "./JoinRoomPageContent.scss";

const JoinRoomPageContent = ({
  exchangeDate,
  giftBudget,
  handleNavigate,
  title,
}: JoinRoomContentProps) => {
  return (
    <FormWrapper
      title={title}
      formKey="WELCOME_ROOM"
      iconName="welcome-group"
      buttonProps={{
        children: "Join the Room",
        onClick: handleNavigate,
      }}
    >
      <div className="join-room-content">
        <div className="join-room-content__cards">
          <InfoCard
            title="Exchange Date"
            description={formatDate(exchangeDate)}
            iconName="star"
            width="244px"
          />
          <InfoCard
            title="Gift Budget"
            description={formatBudget(giftBudget)}
            iconName="presents"
            width="244px"
          />
        </div>

        <h4 className="join-room-content__header">
          To join the fun, you&apos;ll just need to:
        </h4>

        <ul className="join-room-content__list">
          <li>
            <svg width={24} height={24}>
              <use href={`${JoinRoomSprite}#fugar`} />
            </svg>
            Fill in your details
          </li>

          <li>
            <svg width={24} height={24}>
              <use href={`${JoinRoomSprite}#gift`} />
            </svg>
            Add your wishlist (or let the magic decide!)
          </li>
        </ul>

        <p>It only takes a minute — and the joy will last all season long!</p>
      </div>
    </FormWrapper>
  );
};

export default JoinRoomPageContent;
