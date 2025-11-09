import type { PersonalInformationProps } from "../../../common/personal-information/types";
import type { WishlistProps } from "../../../common/wishlist/types";

export interface RandomizationModalProps {
  isOpen?: boolean;
  onClose: () => void;
  personalInfoData: PersonalInformationProps;
  wishlistData: WishlistProps;
}
