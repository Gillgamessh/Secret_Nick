export const STATUS_TYPES = {
  SUCCESS: "success",
  ERROR: "error",
  INFO: "info",
};

export type StatusTypes = (typeof STATUS_TYPES)[keyof typeof STATUS_TYPES];
