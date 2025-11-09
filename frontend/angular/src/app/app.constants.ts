import { StepLabel } from './app.enum';

import type {
  CreateRoomSuccessPageData,
  JoinRoomWelcomePageData,
} from './app.models';

export const ICONS_SPRITE_PATH = '/assets/icons/icons-sprite.svg';

export const IMAGES_SPRITE_PATH = '/assets/images/images-sprite.svg';

export const PRIVACY_POLICY_PATH = '/assets/pdfs/privacy-policy.pdf';

export const PRIVACY_NOTICE_PATH = '/assets/pdfs/privacy-notice.pdf';

export const CONFETTI_ANIMATION_PATH = '/assets/animations/confetti.json';

export const MESSAGE_DURATION_MS = 3000;

export const DEFAULT_ROOM_NAME = 'Secret Nick';

export const JOIN_ROOM_STEPPER_LABELS: StepLabel[] = [
  StepLabel.AddPersonalInfo,
  StepLabel.AddWishlist,
];

export const CREATE_ROOM_STEPPER_LABELS: StepLabel[] = [
  StepLabel.CreateRoom,
  ...JOIN_ROOM_STEPPER_LABELS,
];

export const SUCCESS_PAGE_DATA_DEFAULT: CreateRoomSuccessPageData = {
  userCode: '',
  invitationCode: '',
  invitationNote: '',
  name: '',
};

export const JOIN_ROOM_DATA_DEFAULT: JoinRoomWelcomePageData = {
  giftMaximumBudget: 0,
  invitationCode: '',
  giftExchangeDate: '',
  name: '',
};

export const PHONE_CODE_UA = '+380';

export const ROOM_PAGE_DATA_DEFAULT = {
  name: '',
  description: '',
  giftExchangeDate: '',
  giftMaximumBudget: 0,
  adminId: 0,
  invitationNote: '',
  invitationCode: '',
  isFull: false,
  createdOn: '',
  modifiedOn: '',
  id: 0,
};

export const MAX_BUDGET = 100000;

export const MIN_USERS_NUMBER = 3;

export const FADE_IN_ANIMATION_DURATION_MS = 500;
