import { useState } from "react";
import { useParams } from "react-router";
import type { UpdateRoomResponse, UpdateRoomRequest } from "@types/api.ts";
import Input from "../input/Input";
import CopyButton from "../copy-button/CopyButton";
import IconButton from "../icon-button/IconButton";
import Loader from "../loader/Loader";
import { NOTE_MAX_LENGTH } from "./utils";
import { useFetch } from "@hooks/useFetch";
import useToaster from "@hooks/useToaster.ts";
import { BASE_API_URL } from "@utils/general.ts";
import type { InputChangeEvent, InputChangeHandler } from "@types/general";
import type { InvitationNoteProps } from "./types";
import "./InvitationNote.scss";

const InvitationNote = ({
  value,
  invitationLink,
  userCode: userCodeProp,
  width,
  ...restProps
}: InvitationNoteProps) => {
  const [noteValue, setNoteValue] = useState(value);
  const [isEditingMode, setIsEditingMode] = useState(false);
  const { showToast } = useToaster();
  const { userCode: userCodeFromParams } = useParams();

  const userCode = userCodeFromParams || userCodeProp;

  const invitationNoteContent = `${noteValue}\n${invitationLink}`;
  const extraSymbolsCount =
    invitationNoteContent.length - (noteValue.length + invitationLink.length);
  const noteValueMaxLength =
    NOTE_MAX_LENGTH - invitationLink.length - extraSymbolsCount;

  const handleChange: InputChangeHandler = (e: InputChangeEvent) => {
    setNoteValue(e.target.value);
  };

  const handleSaveNote = () => {
    fetchUpdateRoom({
      invitationNote: noteValue,
    });
  };

  const handleEditClick = () => {
    setIsEditingMode(true);
  };

  const { fetchData: fetchUpdateRoom, isLoading: isLoadingUpdateRoom } =
    useFetch<UpdateRoomResponse, UpdateRoomRequest>(
      {
        url: `${BASE_API_URL}/api/rooms?userCode=${userCode}`,
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        onSuccess: () => {
          setIsEditingMode(false);
        },
        onError: () => {
          showToast("Something went wrong. Try again.", "error", "large");
        },
      },
      false,
    );

  return (
    <>
      {isLoadingUpdateRoom ? <Loader /> : null}

      <div style={{ width }}>
        <h2 className="note-title">Invitation Note</h2>
        <div className="note-textarea">
          <Input
            value={noteValue}
            multiline
            readOnly={!isEditingMode}
            onChange={handleChange}
            withoutCounter
            width={width}
            variant="invitation-note"
            maxLength={noteValueMaxLength}
            rows={1}
            {...restProps}
          />

          <p className="note-invitation-link">{invitationLink}</p>

          <div className="note-buttons">
            <IconButton
              iconName={isEditingMode ? "save" : "edit"}
              onClick={isEditingMode ? handleSaveNote : handleEditClick}
            />

            <CopyButton
              textToCopy={invitationNoteContent}
              successMessage="Invitation note is copied"
              errorMessage="Invitation note was not copied. Try again."
            />
          </div>
        </div>

        <div className="note-info-container">
          {isEditingMode ? (
            <p className="note-caption">Make sure you save changes</p>
          ) : null}

          <p className="note-counter">
            {invitationNoteContent.length} / {NOTE_MAX_LENGTH}
          </p>
        </div>
      </div>
    </>
  );
};

export default InvitationNote;
