import Button from "@components/common/button/Button";
import type { RandomizationPanelProps } from "../types";
import { MIN_USERS_COUNT } from "../utils";
import "./RandomizationPanelContent.scss";

const RandomizationPanelContent = ({
  isRandomized,
  userCount,
  fullName,
  onDraw,
  onReadUserDetails,
}: RandomizationPanelProps) => {
  const isMinUsersReached = userCount >= MIN_USERS_COUNT;
  const textForRandomization = isMinUsersReached
    ? "Don`t forget to hit the button to randomly pair everyone in the game."
    : `You need at least ${MIN_USERS_COUNT} people in the room to enable drawing.`;

  return (
    <div
      className={`random-panel-content ${isRandomized ? "random-panel-content--randomized" : ""}`}
    >
      <div className="random-panel-content__top">
        <h4 className="random-panel-content__title">
          {!isRandomized ? "Let the Magic Begin!" : "Look Who You Got!"}
        </h4>
      </div>
      <div
        className={`random-panel-content__bottom ${isRandomized ? "random-panel-content__bottom--randomized" : ""}`}
      >
        {isRandomized ? (
          <>
            <p className="random-panel-content__subtitle">
              You are a Secret Nick to
            </p>
            <h2 className="random-panel-content__name">{fullName}</h2>
          </>
        ) : null}

        {!isRandomized ? (
          <p className="random-panel-content__text">{textForRandomization}</p>
        ) : null}

        <Button
          variant="secondary"
          size="medium"
          disabled={!isMinUsersReached}
          onClick={isRandomized ? onReadUserDetails : onDraw}
        >
          {isRandomized ? "Read Details" : "Draw Names"}
        </Button>
      </div>
    </div>
  );
};

export default RandomizationPanelContent;
