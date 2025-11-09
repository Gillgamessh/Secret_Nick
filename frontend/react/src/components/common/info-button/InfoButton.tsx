import { useRef } from "react";
import IconButton from "../icon-button/IconButton";
import Toaster from "../toaster/Toaster";
import type { ToasterHandler } from "../toaster/types";
import type { InfoButtonProps } from "./types";
import "./InfoButton.scss";

const InfoButton = ({
  infoMessage,
  onClick,
  withoutToaster = false,
}: InfoButtonProps) => {
  const toasterRef = useRef<ToasterHandler>(null);

  const handleClick = () => {
    if (!withoutToaster && infoMessage) {
      toasterRef.current?.show(infoMessage, "info", "small");
    }
    onClick?.();
  };

  return (
    <div className="info-button">
      <IconButton iconName="info" color="green" onClick={handleClick} />
      <Toaster ref={toasterRef} isSticky />
    </div>
  );
};

export default InfoButton;
