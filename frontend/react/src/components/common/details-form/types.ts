export const DetailsFormInputNames = {
  FIRST_NAME: "firstName",
  LAST_NAME: "lastName",
  DELIVERY_INFO: "deliveryInfo",
  EMAIL: "email",
  PHONE: "phone",
} as const;

export type DetailsFormInputName =
  (typeof DetailsFormInputNames)[keyof typeof DetailsFormInputNames];

export interface FormData {
  firstName: string;
  lastName: string;
  deliveryInfo: string;
  phone: string;
  email: string;
}

export interface DetailsFormProps {
  onBack?: () => void;
}

export const requiredFields: (keyof FormData)[] = [
  "firstName",
  "lastName",
  "deliveryInfo",
];
