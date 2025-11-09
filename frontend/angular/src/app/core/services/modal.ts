import { Injectable, signal } from '@angular/core';

import { FADE_IN_ANIMATION_DURATION_MS } from '../../app.constants';
import type {
  ModalComponentType,
  ModalEntryNullable,
  ModalInputs,
  ModalOutputs,
} from '../../app.models';

@Injectable({ providedIn: 'root' })
export class ModalService {
  readonly #isModalOpen = signal<boolean>(false);
  readonly #currentModal = signal<ModalEntryNullable>(null);

  public readonly isModalOpen = this.#isModalOpen;
  public readonly currentModal = this.#currentModal.asReadonly();

  public openWithResult(
    component: ModalComponentType,
    inputs?: ModalInputs,
    outputs?: ModalOutputs
  ): void {
    this.#currentModal.set({ component, inputs, outputs });
    this.#isModalOpen.set(true);
  }

  public close(): void {
    this.#isModalOpen.set(false);
    setTimeout(
      () => this.#currentModal.set(null),
      FADE_IN_ANIMATION_DURATION_MS
    );
  }
}
