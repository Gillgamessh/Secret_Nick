import type { ShowToasterProps } from "../../components/common/toaster/types";
export type ToasterContextValue = {
  showToast: ShowToasterProps;
  closeToast: () => void;
};
