import RoomLink from "../room-link/RoomLink";
import type { PersonalInformationProps } from "./types";
import { formatPhoneNumber } from "./utils";
import "./PersonalInformation.scss";

const PersonalInformation = ({
  firstName,
  lastName,
  phone,
  email = "-",
  deliveryInfo,
  width = "100%",
  withBackground = false,
  link,
  isOneColumn,
  withoutHeader = false,
}: PersonalInformationProps) => {
  return (
    <div style={{ width }}>
      {!withoutHeader ? (
        <h4 className="personal-info-title">Personal Information</h4>
      ) : null}

      <div
        className={`personal-info-content ${withBackground ? "personal-info-content--with-background" : ""} ${isOneColumn ? "personal-info-content--is-one-column" : ""}`}
      >
        <div
          className={`personal-info-content__container ${isOneColumn ? "personal-info-content__container--is-one-column" : ""}`}
        >
          <div
            className={`personal-info-content__field ${isOneColumn ? "personal-info-content__field--is-one-column" : ""}`}
          >
            <p className="personal-info-content__label">First name</p>
            <p className="personal-info-content__value">{firstName}</p>
          </div>

          <div
            className={`personal-info-content__field ${isOneColumn ? "personal-info-content__field--is-one-column" : ""}`}
          >
            <p className="personal-info-content__label">Last name</p>
            <p className="personal-info-content__value">{lastName}</p>
          </div>

          <div
            className={`personal-info-content__field ${isOneColumn ? "personal-info-content__field--is-one-column" : ""}`}
          >
            <p className="personal-info-content__label">Phone number</p>
            <p className="personal-info-content__value">
              {formatPhoneNumber(phone)}
            </p>
          </div>

          <div
            className={`personal-info-content__field ${isOneColumn ? "personal-info-content__field--is-one-column" : ""}`}
          >
            <p className="personal-info-content__label">Email</p>
            <p className="personal-info-content__value">{email}</p>
          </div>

          <div
            className={`personal-info-content__field ${isOneColumn ? "personal-info-content__field--is-one-column" : ""}`}
          >
            <p className="personal-info-content__label">Delivery address</p>
            <p className="personal-info-content__value">{deliveryInfo}</p>
          </div>
        </div>
        {link ? (
          <RoomLink
            url={link}
            description="Participant`s Personal Room Link"
            caption="Share this link only with the participant"
            small
          />
        ) : null}
      </div>
    </div>
  );
};

export default PersonalInformation;
