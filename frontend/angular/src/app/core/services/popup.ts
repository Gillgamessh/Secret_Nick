import {
  ApplicationRef,
  ComponentRef,
  createComponent,
  inject,
  Injectable,
  Injector,
} from '@angular/core';
import { Message } from '../../shared/components/message/message';
import { fromEvent } from 'rxjs';
import { MessageSize, PopupPosition } from '../../app.enum';
import { MessageOptions, PopupInstance } from '../../app.models';
import { MESSAGE_DURATION_MS } from '../../app.constants';

@Injectable({
  providedIn: 'root',
})
export class PopupService {
  private popups = new Map<HTMLElement, PopupInstance>();

  private appRef = inject(ApplicationRef);
  private injector = inject(Injector);

  public show(
    hostElement: HTMLElement,
    alignment: PopupPosition,
    messageOptions: MessageOptions,
    closeOnOutsideClick = false
  ): void {
    this.#clearPrevMessage(hostElement);
    this.#addRefToStorage(hostElement);
    const ref = this.popups.get(hostElement)?.ref ?? null;

    this.#addInputs(ref, messageOptions);
    this.#renderPopup(ref, alignment, hostElement);
    this.#addHidingWays(closeOnOutsideClick, hostElement);
  }

  #clearPrevMessage(popupId: HTMLElement): void {
    if (this.popups.get(popupId)) {
      this.#hide(popupId);
    }
  }

  #addRefToStorage(popupId: HTMLElement): void {
    this.popups.set(popupId, { ref: this.#createMessage() });
  }

  #createMessage(): ComponentRef<Message> {
    return createComponent(Message, {
      environmentInjector: this.appRef.injector,
      elementInjector: this.injector,
    });
  }

  #addInputs(
    popupRef: ComponentRef<Message> | null,
    messageOptions: MessageOptions
  ): void {
    if (popupRef) {
      popupRef.setInput('text', messageOptions.message);
      popupRef.setInput('type', messageOptions.type);
      popupRef.setInput('size', MessageSize.Popover);
    }
  }

  #renderPopup(
    popupRef: ComponentRef<Message> | null,
    alignment: PopupPosition,
    hostElement: HTMLElement
  ): void {
    if (popupRef) {
      const popup = popupRef.location.nativeElement;
      this.appRef.attachView(popupRef.hostView);
      popup.classList.add(
        'popover-container',
        `popover-container--${alignment}`
      );
      hostElement.prepend(popup);
    }
  }

  #addOvertimeHiding(popupId: HTMLElement): void {
    const timerId = setTimeout(() => this.#hide(popupId), MESSAGE_DURATION_MS);
    const popupData = this.popups.get(popupId);
    this.popups.set(popupId, { ...popupData, timerId });
  }

  #addClickHandler(popupId: HTMLElement): void {
    const popupData = this.popups.get(popupId);
    setTimeout(() => {
      const subscription = fromEvent(document, 'click').subscribe(
        (event: Event) => {
          if (popupData && popupData?.ref) {
            const popup = popupData.ref.location.nativeElement;
            const target = event.target;
            const clickInside = popup.contains(target);

            if (clickInside) {
              event.stopPropagation();
            } else {
              this.#hide(popupId);
            }
          }
        }
      );
      this.popups.set(popupId, { ...popupData, subscription });
    }, 0);
  }

  #addHidingWays(closeOnOutsideClick: boolean, popupId: HTMLElement): void {
    if (closeOnOutsideClick) {
      this.#addClickHandler(popupId);
    } else {
      this.#addOvertimeHiding(popupId);
    }
  }

  #hide(popupId: HTMLElement): void {
    const popupData = this.popups.get(popupId);
    if (popupData && popupData.ref) {
      this.appRef.detachView(popupData.ref.hostView);
      popupData.ref.destroy();
      popupData.ref = null;
      this.popups.delete(popupId);
      popupData.subscription?.unsubscribe();
      clearTimeout(popupData.timerId);
    }
  }

  public hide(hostElement: HTMLElement): void {
    this.#hide(hostElement);
  }
}
