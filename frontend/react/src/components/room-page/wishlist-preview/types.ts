import type { WishList } from "@types/api";

export interface WishlistPreviewProps {
  isWantSurprise?: boolean;
  wishListData?: WishList;
  onViewWishlist: () => void;
}
