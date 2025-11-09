import type { ShowToaster } from "@components/common/toaster/types";
import config from "../../config.json";

const { protocol, host } = window.location;

export const BASE_API_URL = config?.environment?.backendApiUrl;
export const BASE_FRONTEND_URL = `${protocol}//${host}`;
export const MAX_PARTICIPANTS_NUMBER = 20;

export const generateRoomLink = (invitationCode: string) => {
  return `${BASE_FRONTEND_URL}/join/${invitationCode}`;
};

export const generateParticipantLink = (userCode: string) => {
  return `${BASE_FRONTEND_URL}/room/${userCode}`;
};

export const generateInvitationNoteContent = (
  invitationNote: string,
  invitationCode: string,
) => {
  const roomLink = generateRoomLink(invitationCode);

  return `${invitationNote}\n${roomLink}`;
};

export const removeIdFromArray = <T extends { id?: number }>(
  array: T[],
): Omit<T, "id">[] => {
  return array.map(({ id, ...rest }) => {
    void id;
    return rest;
  });
};

export const formatBudget = (budget?: number) => {
  if (budget === undefined || budget === null) return "No data";
  if (budget === 0) return "Unlimited";
  return `${budget} UAH`;
};

export const copyToClipboard = (contentToCopy: string) =>
  navigator.clipboard.writeText(contentToCopy);

export const copyToClipboardWithToaster = (
  contentToCopy: string,
  showToaster: ShowToaster,
  messageConfig: { successMessage: string; errorMessage: string },
) => {
  copyToClipboard(contentToCopy)
    .then(() => showToaster(messageConfig.successMessage, "success", "small"))
    .catch(() => showToaster(messageConfig.errorMessage, "error", "small"));
};

export const formatDate = (dateString?: string) => {
  if (dateString === undefined || dateString === null) return "No data";

  const date = new Date(dateString);
  const options: Intl.DateTimeFormatOptions = {
    day: "2-digit",
    month: "short",
    year: "numeric",
  };
  return date.toLocaleDateString("en-GB", options);
};
