import type { WishList } from "@types/api";

export interface ViewWishlistModalProps {
  isOpen?: boolean;
  onClose: () => void;
  budget: number;
  wantSurprise: boolean;
  wishlistData?: WishList;
  interests?: string;
}
