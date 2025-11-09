export interface RandomizationPanelProps {
  userCount: number;
  isRandomized?: boolean;
  isMinUsersReached?: boolean;
  fullName?: string;
  onDraw?: () => void;
  onReadUserDetails?: () => void;
}
