import { Injectable, signal, computed } from '@angular/core';

import { ToastMessage, MessageType } from '../../app.enum';
import { ToasterStatus } from '../../app.enum';
import { MESSAGE_DURATION_MS } from '../../app.constants';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  readonly #message = signal('');
  readonly #type = signal<MessageType>(MessageType.Success);
  readonly #status = signal<ToasterStatus>(ToasterStatus.Hidden);

  public readonly message = this.#message.asReadonly();
  public readonly type = this.#type.asReadonly();
  public readonly status = this.#status.asReadonly();

  public readonly isVisible = computed(
    () => this.#status() === ToasterStatus.Visible
  );

  #timeoutId: ReturnType<typeof setTimeout> | null = null;

  public show(message: ToastMessage, type: MessageType): void {
    if (this.#timeoutId) {
      clearTimeout(this.#timeoutId);
    }

    this.#message.set(message);
    this.#type.set(type);
    this.#status.set(ToasterStatus.Visible);
    this.#timeoutId = setTimeout(() => this.hide(), MESSAGE_DURATION_MS);
  }

  public hide(): void {
    this.#status.set(ToasterStatus.Hidden);
  }
}
