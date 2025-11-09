import Wishlist from "@components/common/wishlist/Wishlist";
import Button from "@components/common/button/Button";
import type { WishlistPreviewProps } from "./types";
import "./WishlistPreview.scss";

const WishlistPreview = ({
  isWantSurprise = false,
  wishListData = [],
  onViewWishlist,
}: WishlistPreviewProps) => {
  return (
    <div className="wishlist-preview">
      <h4 className="wishlist-preview__title">My Wishlist</h4>
      <div className="wishlist-preview__content">
        {isWantSurprise ? <Wishlist variant="surprise" withoutHeader /> : null}

        {!isWantSurprise ? (
          <Wishlist variant="wishlist" wishList={wishListData} withoutHeader />
        ) : null}

        <Button variant="secondary" size="medium" onClick={onViewWishlist}>
          View Wishlist
        </Button>
      </div>
    </div>
  );
};

export default WishlistPreview;
