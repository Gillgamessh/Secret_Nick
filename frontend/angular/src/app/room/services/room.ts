import { computed, inject, Injectable, signal } from '@angular/core';
import { RoomDetails } from '../../app.models';
import { ROOM_PAGE_DATA_DEFAULT } from '../../app.constants';
import { ApiService } from '../../core/services/api';
import { tap } from 'rxjs';
import { NavigationLinkSegment } from '../../app.enum';
import { UrlService } from '../../core/services/url';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  readonly #apiService = inject(ApiService);
  readonly #urlService = inject(UrlService);

  public roomData = signal<RoomDetails>(ROOM_PAGE_DATA_DEFAULT);

  public readonly invitationLink = computed(
    () =>
      this.#urlService.getNavigationLinks(
        this.roomData().invitationCode,
        NavigationLinkSegment.Join
      ).absoluteUrl
  );

  public readonly isRoomDrawn = computed(() => !!this.roomData().closedOn);

  public getRoomByUserCode(code: string) {
    this.#apiService
      .getRoomByUserCode(code)
      .pipe(
        tap((result) => {
          if (result?.body) {
            this.roomData.set(result.body);
          }
        })
      )
      .subscribe();
  }
}
