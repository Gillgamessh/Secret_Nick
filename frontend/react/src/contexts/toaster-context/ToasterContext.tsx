import { createContext, useRef, type ReactNode } from "react";
import Toaster from "../../components/common/toaster/Toaster";
import type { ToasterHandler } from "../../components/common/toaster/types";
import type { ToasterContextValue } from "./types";

const ToasterContext = createContext<ToasterContextValue | null>(null);

export const ToasterProvider = ({ children }: { children: ReactNode }) => {
  const toaster = useRef<ToasterHandler>(null);

  const showToast: ToasterContextValue["showToast"] = (message, type, size) => {
    toaster.current?.show(message, type, size);
  };
  const closeToast: ToasterContextValue["closeToast"] = () => {
    toaster.current?.close();
  };

  return (
    <ToasterContext.Provider value={{ showToast, closeToast }}>
      {children}
      <Toaster ref={toaster} isGlobal />
    </ToasterContext.Provider>
  );
};

export default ToasterContext;
