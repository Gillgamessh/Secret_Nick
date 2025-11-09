import RandomizationPanelContent from "./RandomizationPanelContent/RandomizationPanelContent";
import type { RandomizationPanelProps } from "./types";
import "./RandomizationPanel.scss";

const RandomizationPanel = ({
  isRandomized = false,
  userCount,
  fullName = "",
  onDraw,
  onReadUserDetails,
}: RandomizationPanelProps) => {
  return (
    <div
      className={`randomization-panel randomization-panel${isRandomized ? "--stNick" : "--presents"}`}
    >
      <RandomizationPanelContent
        isRandomized={isRandomized}
        userCount={userCount}
        fullName={fullName}
        onDraw={onDraw}
        onReadUserDetails={onReadUserDetails}
      />
    </div>
  );
};

export default RandomizationPanel;
