import {
  forwardRef,
  useEffect,
  useImperativeHandle,
  useState,
  useRef,
} from "react";
import { ICONS_PATH } from "../icon-button/utils";
import type { ToasterProps, ToasterHandler } from "./types";
import "./Toaster.scss";

const Toaster = forwardRef<ToasterHandler, ToasterProps>(
  ({ duration = 3000, isGlobal = false, isSticky = false }, ref) => {
    const [isVisible, setIsVisible] = useState(false);
    const [type, setType] = useState("");
    const [size, setSize] = useState("small");
    const [contentText, setContentText] = useState("");
    const iconPath = `${ICONS_PATH}${type}`;
    const toasterRef = useRef<HTMLDivElement | null>(null);

    const onClose = () => setIsVisible(false);

    useImperativeHandle(ref, () => ({
      show(text, type, size) {
        setType(type);
        setContentText(text);
        setIsVisible(true);
        setSize(size);
      },
      close() {
        onClose();
      },
    }));

    useEffect(() => {
      if (!isVisible || isSticky) return;

      const timerId = setTimeout(onClose, duration);
      return () => clearTimeout(timerId);
    }, [isVisible, isSticky, duration]);

    useEffect(() => {
      if (!isVisible || !isSticky) return;

      const handleClickOutside = (event: MouseEvent) => {
        if (
          toasterRef.current &&
          !toasterRef.current.contains(event.target as Node)
        ) {
          onClose();
        }
      };

      document.addEventListener("mousedown", handleClickOutside);

      return () => {
        document.removeEventListener("mousedown", handleClickOutside);
      };
    }, [isVisible, isSticky]);

    return isVisible ? (
      <div
        ref={toasterRef}
        className={`toaster toaster--${size} ${
          isGlobal ? "toaster--global" : null
        }`}
      >
        <svg
          className={`toaster__icon ${type === "info" ? "toaster__icon--info" : ""}`}
        >
          <use href={iconPath} />
        </svg>
        <p className="toaster__text">{contentText}</p>
      </div>
    ) : null;
  },
);

Toaster.displayName = "Toaster";

export default Toaster;
