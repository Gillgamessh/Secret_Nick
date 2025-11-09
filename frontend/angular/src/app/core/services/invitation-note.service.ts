import { Injectable, inject } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiService } from './api';
import { RoomDetails } from '../../app.models';

@Injectable()
export class InvitationNoteService {
  private api = inject(ApiService);

  updateInvitationNote(userCode: string, note: string): Observable<string> {
    return this.api
      .patchRoom(userCode, { invitationNote: note })
      .pipe(
        map(
          (resp) =>
            (resp.body as RoomDetails | undefined)?.invitationNote ?? note
        )
      );
  }
}
