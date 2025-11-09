import { Navigate, useLocation, useParams } from "react-router";
import type { PrivateRouteProps } from "./types";
import { TOASTER_MESSAGES } from "./utils";

const PrivateRoute = ({ children, pageName }: PrivateRouteProps) => {
  const location = useLocation();
  const state = location.state;
  const { roomCode } = useParams();

  if (pageName === "create-room-success-page" && !state?.roomAndUserData) {
    return (
      <Navigate
        to="/home"
        replace
        state={{
          toastMessage: TOASTER_MESSAGES.CREATE_ROOM_SUCCESS_PAGE,
        }}
      />
    );
  }

  if (pageName === "join-room-success-page" && !state?.userCode) {
    return (
      <Navigate
        to={`/join/${roomCode}`}
        replace
        state={{
          toastMessage: TOASTER_MESSAGES.JOIN_ROOM_SUCCESS_PAGE,
        }}
      />
    );
  }

  return children;
};

export default PrivateRoute;
