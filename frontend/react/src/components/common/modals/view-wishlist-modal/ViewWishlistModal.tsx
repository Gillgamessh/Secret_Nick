import Wishlist from "@components/common/wishlist/Wishlist";
import Modal from "../modal/Modal";
import type { ViewWishlistModalProps } from "./types";
import { formatBudget } from "@utils/general";

const ViewWishlistModal = ({
  isOpen = false,
  onClose,
  budget,
  wantSurprise,
  wishlistData = [],
  interests,
}: ViewWishlistModalProps) => {
  return (
    <Modal
      title="Your Wishlist"
      description="Let your Secret Nick know what would make you smile this season."
      subdescription={`Gift Budget: ${formatBudget(budget)}`}
      iconName="presents"
      isOpen={isOpen}
      onClose={onClose}
      onConfirm={onClose}
    >
      {wantSurprise ? (
        <Wishlist variant="surprise" interests={interests} withoutHeader />
      ) : null}

      {!wantSurprise ? (
        <Wishlist variant="wishlist" wishList={wishlistData} withoutHeader />
      ) : null}
    </Modal>
  );
};

export default ViewWishlistModal;
